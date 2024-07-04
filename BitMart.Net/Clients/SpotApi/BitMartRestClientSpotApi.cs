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
using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using BitMart.Net.Objects.Internal;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;

namespace BitMart.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IBitMartRestClientSpotApi" />
    public class BitMartRestClientSpotApi : RestApiClient, IBitMartRestClientSpotApi, ISpotClient
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Spot Api");
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IBitMartRestClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IBitMartRestClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBitMartRestClientSpotApiTrading Trading { get; }
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
        internal BitMartRestClientSpotApi(ILogger logger, HttpClient? httpClient, BitMartRestOptions options)
            : base(logger, httpClient, options.Environment.RestClientAddress, options, options.SpotOptions)
        {
            Account = new BitMartRestClientSpotApiAccount(this);
            ExchangeData = new BitMartRestClientSpotApiExchangeData(logger, this);
            Trading = new BitMartRestClientSpotApiTrading(logger, this);
        }
        #endregion

        /// <inheritdoc />
        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor();
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BitMartAuthenticationProvider(credentials);

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<BitMartResponse>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 1000)
                return result.AsDatalessError(new ServerError(result.Data.Code, result.Data.Message));

            return result.AsDataless();
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<BitMartResponse<T>>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result)
                return result.As<T>(default);

            if (result.Data.Code != 1000)
                return result.AsError<T>(new ServerError(result.Data.Code, result.Data.Message));

            return result.As(result.Data.Data);
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
