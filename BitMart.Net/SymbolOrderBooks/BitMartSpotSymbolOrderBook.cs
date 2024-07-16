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

namespace BitMart.Net.SymbolOrderBooks
{
    /// <summary>
    /// Implementation for a synchronized order book. After calling Start the order book will sync itself and keep up to date with new data. It will automatically try to reconnect and resync in case of a lost/interrupted connection.
    /// Make sure to check the State property to see if the order book is synced.
    /// </summary>
    public class BitMartSpotSymbolOrderBook : SymbolOrderBook
    {
        private readonly bool _clientOwner;
        private readonly IBitMartRestClient _restClient;
        private readonly IBitMartSocketClient _socketClient;
        private readonly TimeSpan _initialDataTimeout;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BitMartSpotSymbolOrderBook(string symbol, Action<BitMartOrderBookOptions>? optionsDelegate = null)
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
        public BitMartSpotSymbolOrderBook(
            string symbol,
            Action<BitMartOrderBookOptions>? optionsDelegate,
            ILoggerFactory? logger,
            IBitMartRestClient? restClient,
            IBitMartSocketClient? socketClient) : base(logger, "BitMart", "Spot", symbol)
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
            CallResult<UpdateSubscription> subResult;
            if (Levels == null)
                subResult = await _socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync(Symbol, HandleUpdate).ConfigureAwait(false);
            else
                subResult = await _socketClient.SpotApi.SubscribeToPartialOrderBookUpdatesAsync(Symbol, Levels.Value, HandleUpdate).ConfigureAwait(false);

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

        private void HandleUpdate(DataEvent<BitMartOrderBookIncrementalUpdate> data)
        {
            if (data.UpdateType == SocketUpdateType.Snapshot)
                SetInitialOrderBook(DateTimeConverter.ConvertToMilliseconds(data.Data.Timestamp).Value, data.Data.Bids, data.Data.Asks);
            else
                UpdateOrderBook(DateTimeConverter.ConvertToMilliseconds(data.Data.Timestamp).Value, data.Data.Bids, data.Data.Asks);
        }

        private void HandleUpdate(DataEvent<BitMartOrderBookUpdate> data)
        {
            SetInitialOrderBook(DateTimeConverter.ConvertToMilliseconds(data.Data.Timestamp).Value, data.Data.Bids, data.Data.Asks);
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
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
