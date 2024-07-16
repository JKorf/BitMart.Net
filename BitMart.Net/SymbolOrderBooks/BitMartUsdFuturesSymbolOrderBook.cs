using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Converters.SystemTextJson;
using Microsoft.Extensions.Logging;
using BitMart.Net.Clients;
using BitMart.Net.Interfaces.Clients;
using BitMart.Net.Objects.Options;
using BitMart.Net.Objects.Models;
using CryptoExchange.Net.Interfaces;
using System.Collections;
using System.Collections.Generic;
using BitMart.Net.Enums;

namespace BitMart.Net.SymbolOrderBooks
{
    /// <summary>
    /// Implementation for a synchronized order book. After calling Start the order book will sync itself and keep up to date with new data. It will automatically try to reconnect and resync in case of a lost/interrupted connection.
    /// Make sure to check the State property to see if the order book is synced.
    /// </summary>
    public class BitMartUsdFuturesSymbolOrderBook : SymbolOrderBook
    {
        private readonly bool _clientOwner;
        private readonly IBitMartRestClient _restClient;
        private readonly IBitMartSocketClient _socketClient;
        private readonly TimeSpan _initialDataTimeout;

        private IEnumerable<ISymbolOrderBookEntry>? _depthBuffer;
        private OrderBookSide? _bufferSide;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BitMartUsdFuturesSymbolOrderBook(string symbol, Action<BitMartOrderBookOptions>? optionsDelegate = null)
            : this(symbol, optionsDelegate, null, null, null)
        {
            _clientOwner = true;
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="logger">Logger</param>
        /// <param name="restClient">Rest client instance</param>
        /// <param name="socketClient">Socket client instance</param>
        public BitMartUsdFuturesSymbolOrderBook(
            string symbol,
            Action<BitMartOrderBookOptions>? optionsDelegate,
            ILoggerFactory? logger,
            IBitMartRestClient? restClient,
            IBitMartSocketClient? socketClient) : base(logger, "BitMart", "UsdFutures", symbol)
        {
            var options = BitMartOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _strictLevels = false;
            _sequencesAreConsecutive = options?.Limit == null;

            Levels = options?.Limit;
            _initialDataTimeout = options?.InitialDataTimeout ?? TimeSpan.FromSeconds(30);
            _clientOwner = socketClient == null;
            _socketClient = socketClient ?? new BitMartSocketClient();
            _restClient = restClient ?? new BitMartRestClient();
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            var subResult = await _socketClient.UsdFuturesApi.SubscribeToOrderBookUpdatesAsync(Symbol, Levels ?? 20, HandleUpdate).ConfigureAwait(false);
            if (!subResult)
                return new CallResult<UpdateSubscription>(subResult.Error!);

            if (ct.IsCancellationRequested)
            {
                await subResult.Data.CloseAsync().ConfigureAwait(false);
                return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
            }

            Status = OrderBookStatus.Syncing;
            var setResult = await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
            return setResult ? subResult : new CallResult<UpdateSubscription>(setResult.Error!);
        }

        private void HandleUpdate(DataEvent<BitMartFuturesOrderBookUpdate> data)
        {
            if (!_bookSet)
            {
                if (_depthBuffer == null || _bufferSide == data.Data.Side)
                {
                    _depthBuffer = data.Data.Depths;
                    _bufferSide = data.Data.Side;
                }
                else if(_bufferSide != data.Data.Side && _depthBuffer != null)
                {
                    SetInitialOrderBook(DateTimeConverter.ConvertToMilliseconds(data.Data.Timestamp).Value, _bufferSide == OrderBookSide.Bids ? _depthBuffer : data.Data.Depths, _bufferSide == OrderBookSide.Asks ? _depthBuffer : data.Data.Depths);
                    _depthBuffer = null;
                    _bufferSide = null;
                }
            }
            else
            {
                UpdateOrderBook(DateTimeConverter.ConvertToMilliseconds(data.Data.Timestamp).Value, data.Data.Side == Enums.OrderBookSide.Bids ? data.Data.Depths : Array.Empty<ISymbolOrderBookEntry>(), data.Data.Side == Enums.OrderBookSide.Asks ? data.Data.Depths : Array.Empty<ISymbolOrderBookEntry>());
            }
        }


        /// <inheritdoc />
        protected override void DoReset()
        {
            _depthBuffer = null;
            _bufferSide = null;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            return await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (_clientOwner)
            {
                _restClient?.Dispose();
                _socketClient?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
