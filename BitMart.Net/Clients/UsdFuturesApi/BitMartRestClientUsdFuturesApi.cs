using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces.CommonClients;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using BitMart.Net.Objects.Options;
using CryptoExchange.Net.Clients;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc cref="IBitMartRestClientUsdFuturesApi" />
    public class BitMartRestClientUsdFuturesApi : RestApiClient, IBitMartRestClientUsdFuturesApi, ISpotClient
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("UsdFutures Api");
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IBitMartRestClientUsdFuturesApiAccount Account { get; }
        /// <inheritdoc />
        public IBitMartRestClientUsdFuturesApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBitMartRestClientUsdFuturesApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "BitMart";
        #endregion

        /// <summary>
        /// Event triggered when an order is placed via this client. Only available for Spot orders
        /// </summary>
        public event Action<OrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is canceled via this client. Note that this does not trigger when using CancelAllOrdersAsync. Only available for Spot orders
        /// </summary>
        public event Action<OrderId>? OnOrderCanceled;

        #region constructor/destructor
        internal BitMartRestClientUsdFuturesApi(ILogger logger, HttpClient? httpClient, BitMartRestOptions options)
            : base(logger, httpClient, options.Environment.RestClientAddress, options, options.UsdFuturesOptions)
        {
            Account = new BitMartRestClientUsdFuturesApiAccount(this);
            ExchangeData = new BitMartRestClientUsdFuturesApiExchangeData(logger, this);
            Trading = new BitMartRestClientUsdFuturesApiTrading(logger, this);
        }
        #endregion

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BitMartAuthenticationProvider(credentials);

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);

            // Optional response checking

            return result;
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);

            // Optional response checking

            return result;
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp, ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval, _timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => _timeSyncState.TimeOffset;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => throw new NotImplementedException();

        /// <inheritdoc />
        public ISpotClient CommonSpotClient => this;

        /// <inheritdoc />
        public string GetSymbolName(string baseAsset, string quoteAsset) =>
            throw new NotImplementedException();

        internal void InvokeOrderPlaced(OrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(OrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }

        async Task<WebCallResult<OrderId>> ISpotClient.PlaceOrderAsync(string symbol, CommonOrderSide side, CommonOrderType type, decimal quantity, decimal? price, string? accountId, string? clientOrderId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        async Task<WebCallResult<Order>> IBaseRestClient.GetOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        async Task<WebCallResult<IEnumerable<UserTrade>>> IBaseRestClient.GetOrderTradesAsync(string orderId, string? symbol, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetOpenOrdersAsync(string? symbol, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetClosedOrdersAsync(string? symbol, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        async Task<WebCallResult<OrderId>> IBaseRestClient.CancelOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        async Task<WebCallResult<IEnumerable<Symbol>>> IBaseRestClient.GetSymbolsAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        async Task<WebCallResult<Ticker>> IBaseRestClient.GetTickerAsync(string symbol, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        async Task<WebCallResult<IEnumerable<Ticker>>> IBaseRestClient.GetTickersAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        async Task<WebCallResult<IEnumerable<Kline>>> IBaseRestClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime, DateTime? endTime, int? limit, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        async Task<WebCallResult<OrderBook>> IBaseRestClient.GetOrderBookAsync(string symbol, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        async Task<WebCallResult<IEnumerable<Trade>>> IBaseRestClient.GetRecentTradesAsync(string symbol, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        async Task<WebCallResult<IEnumerable<Balance>>> IBaseRestClient.GetBalancesAsync(string? accountId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
