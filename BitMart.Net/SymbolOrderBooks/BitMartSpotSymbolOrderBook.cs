using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.OrderBook;
using Microsoft.Extensions.Logging;
using BitMart.Net.Clients;
using BitMart.Net.Interfaces.Clients;
using BitMart.Net.Objects.Options;

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
            // XXX
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            // XXX
            throw new NotImplementedException();
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
