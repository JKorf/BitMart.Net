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
using BitMart.Net.Objects;
using CryptoExchange.Net.Converters.MessageParsing;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using BitMart.Net.Enums;

namespace BitMart.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IBitMartRestClientSpotApi" />
    internal class BitMartRestClientSpotApi : RestApiClient, IBitMartRestClientSpotApi, ISpotClient
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Spot Api");
        internal readonly string _brokerId;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IBitMartRestClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IBitMartRestClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBitMartRestClientSpotApiMargin Margin { get; }
        /// <inheritdoc />
        public IBitMartRestClientSpotApiSubAccount SubAccount { get; }
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
            : base(logger, httpClient, options.Environment.RestSpotClientAddress, options, options.SpotOptions)
        {
            Account = new BitMartRestClientSpotApiAccount(this);
            ExchangeData = new BitMartRestClientSpotApiExchangeData(logger, this);
            Margin = new BitMartRestClientSpotApiMargin(this);
            SubAccount = new BitMartRestClientSpotApiSubAccount(this);
            Trading = new BitMartRestClientSpotApiTrading(logger, this);

            _brokerId = !string.IsNullOrEmpty(options.BrokerId) ? options.BrokerId! : "EASYTRADING0001";
        }
        #endregion

        /// <inheritdoc />
        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor();
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BitMartAuthenticationProvider((BitMartApiCredentials)credentials);

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? additionalHeaders = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight, additionalHeaders);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? additionalHeaders = null)
        {
            var result = await base.SendAsync<BitMartResponse>(baseAddress, definition, parameters, cancellationToken, additionalHeaders, weight).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 1000)
                return result.AsDatalessError(new ServerError(result.Data.Code, result.Data.Message));

            return result.AsDataless();
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? additionalHeaders = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight, additionalHeaders);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? additionalHeaders = null) where T : class
        {
            var result = await base.SendAsync<BitMartResponse<T>>(baseAddress, definition, parameters, cancellationToken, additionalHeaders, weight).ConfigureAwait(false);
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
        public override string FormatSymbol(string baseAsset, string quoteAsset) => baseAsset + "_" + quoteAsset;

        /// <inheritdoc />
        protected override Error ParseErrorResponse(int httpStatusCode, IEnumerable<KeyValuePair<string, IEnumerable<string>>> responseHeaders, IMessageAccessor accessor)
        {
            if (!accessor.IsJson)
                return new ServerError(accessor.GetOriginalString());

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("message"));
            if (msg == null)
                return new ServerError(accessor.GetOriginalString());

            if (code == null)
                return new ServerError(msg);

            return new ServerError(code.Value, msg);
        }

        protected override ServerRateLimitError ParseRateLimitResponse(int httpStatusCode, IEnumerable<KeyValuePair<string, IEnumerable<string>>> responseHeaders, IMessageAccessor accessor)
        {
            var error = base.ParseRateLimitResponse(httpStatusCode, responseHeaders, accessor);
            var retryAfterHeader = responseHeaders.SingleOrDefault(r => r.Key.Equals("X-BM-RateLimit-Reset", StringComparison.InvariantCultureIgnoreCase));
            if (retryAfterHeader.Value?.Any() != true)
                return error;

            var value = retryAfterHeader.Value.First();
            if (!int.TryParse(value, out var seconds))
                return error;

            error.RetryAfter = DateTime.UtcNow.AddSeconds(seconds);
            return error;
        }

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
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for BitMart " + nameof(ISpotClient.PlaceOrderAsync), nameof(symbol));

            var result = await Trading.PlaceOrderAsync(
                symbol,
                side == CommonOrderSide.Buy ? Enums.OrderSide.Buy : Enums.OrderSide.Sell,
                type == CommonOrderType.Limit ? Enums.OrderType.Limit : Enums.OrderType.Market,
                quantity,
                price,
                clientOrderId: clientOrderId).ConfigureAwait(false);

            if (!result)
                return result.As<OrderId>(default);

            return result.As(new OrderId
            {
                Id = result.Data.OrderId,
                SourceObject = result.Data
            });
        }

        async Task<WebCallResult<Order>> IBaseRestClient.GetOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(orderId))
                throw new ArgumentException(nameof(orderId) + " required for BitMart " + nameof(ISpotClient.GetOrderAsync), nameof(orderId));

            var result = await Trading.GetOrderAsync(orderId).ConfigureAwait(false);
            if (!result)
                return result.As<Order>(default);

            return result.As(new Order
            {
                Id = result.Data.OrderId,
                Price = result.Data.Price,
                Quantity = result.Data.Quantity,
                QuantityFilled = result.Data.QuantityFilled,
                Side = result.Data.Side == Enums.OrderSide.Sell ? CommonOrderSide.Sell : CommonOrderSide.Buy,
                Status = result.Data.Status == Enums.OrderStatus.Filled ? CommonOrderStatus.Filled : (result.Data.Status == Enums.OrderStatus.PartiallyFilled || result.Data.Status == Enums.OrderStatus.New) ? CommonOrderStatus.Active: CommonOrderStatus.Canceled,
                Symbol = result.Data.Symbol,
                Timestamp = result.Data.CreateTime,
                Type = result.Data.OrderType == Enums.OrderType.Market ? CommonOrderType.Market : result.Data.OrderType == Enums.OrderType.Limit ? CommonOrderType.Limit : CommonOrderType.Other,
                SourceObject = result.Data
            });
        }

        async Task<WebCallResult<IEnumerable<UserTrade>>> IBaseRestClient.GetOrderTradesAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(orderId))
                throw new ArgumentException(nameof(orderId) + " required for BitMart " + nameof(ISpotClient.GetOrderTradesAsync), nameof(orderId));

            var result = await Trading.GetOrderTradesAsync(orderId).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<UserTrade>>(default);

            return result.As(result.Data.Select(x => new UserTrade
            {
                Id = x.TradeId,
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                OrderId = orderId,
                Price = x.Price,
                Quantity = x.Quantity,
                Symbol = x.Symbol,
                Timestamp = x.CreateTime,
                SourceObject = x
            }));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetOpenOrdersAsync(string? symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for BitMart " + nameof(ISpotClient.GetOpenOrdersAsync), nameof(symbol));

            var result = await Trading.GetOpenOrdersAsync(symbol).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<Order>>(default);

            return result.As(result.Data.Select(x => new Order
            {
                Id = x.OrderId,
                Price = x.Price,
                Quantity = x.Quantity,
                Symbol = x.Symbol,
                Timestamp = x.CreateTime,
                QuantityFilled = x.QuantityFilled,
                Side = x.Side == Enums.OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
                Status = x.Status == Enums.OrderStatus.Filled ? CommonOrderStatus.Filled : (x.Status == Enums.OrderStatus.PartiallyFilled || x.Status == Enums.OrderStatus.New) ? CommonOrderStatus.Active: CommonOrderStatus.Canceled,
                Type = x.OrderType == Enums.OrderType.Market ? CommonOrderType.Market : x.OrderType == Enums.OrderType.Limit ? CommonOrderType.Limit : CommonOrderType.Other,
                SourceObject = x
            }));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetClosedOrdersAsync(string? symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for BitMart " + nameof(ISpotClient.GetClosedOrdersAsync), nameof(symbol));

            var result = await Trading.GetClosedOrdersAsync(symbol).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<Order>>(default);

            return result.As(result.Data.Select(x => new Order
            {
                Id = x.OrderId,
                Price = x.Price,
                Quantity = x.Quantity,
                Symbol = x.Symbol,
                Timestamp = x.CreateTime,
                QuantityFilled = x.QuantityFilled,
                Side = x.Side == Enums.OrderSide.Buy ? CommonOrderSide.Buy : CommonOrderSide.Sell,
                Status = x.Status == Enums.OrderStatus.Filled ? CommonOrderStatus.Filled : (x.Status == Enums.OrderStatus.PartiallyFilled || x.Status == Enums.OrderStatus.New) ? CommonOrderStatus.Active : CommonOrderStatus.Canceled,
                Type = x.OrderType == Enums.OrderType.Market ? CommonOrderType.Market : x.OrderType == Enums.OrderType.Limit ? CommonOrderType.Limit : CommonOrderType.Other,
                SourceObject = x
            }));
        }

        async Task<WebCallResult<OrderId>> IBaseRestClient.CancelOrderAsync(string orderId, string? symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for BitMart " + nameof(ISpotClient.CancelOrderAsync), nameof(symbol));

            var result = await Trading.CancelOrderAsync(symbol!, orderId).ConfigureAwait(false);

            if (!result)
                return result.As<OrderId>(default);

            return result.As(new OrderId
            {
                Id = orderId,
                SourceObject = null!
            });
        }

        async Task<WebCallResult<IEnumerable<Symbol>>> IBaseRestClient.GetSymbolsAsync(CancellationToken ct)
        {
            var result = await ExchangeData.GetSymbolsAsync().ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<Symbol>>(default);

            return result.As(result.Data.Select(x => new Symbol
            {
                Name = x.Symbol,
                PriceStep = x.QuoteIncrement,
                MinTradeQuantity = x.MinBuyQuantity == null && x.MinSellQuantity == null ? null : Math.Min(x.MinBuyQuantity ?? decimal.MaxValue, x.MinSellQuantity ?? decimal.MaxValue),
                PriceDecimals = x.PriceMaxPrecision,
                SourceObject = x
            }));
        }

        async Task<WebCallResult<Ticker>> IBaseRestClient.GetTickerAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for BitMart " + nameof(ISpotClient.GetTickerAsync), nameof(symbol));

            var result = await ExchangeData.GetTickerAsync(symbol).ConfigureAwait(false);
            if (!result)
                return result.As<Ticker>(default);

            return result.As(new Ticker
            {
                HighPrice = result.Data.HighPrice,
                LastPrice = result.Data.LastPrice,
                LowPrice = result.Data.LowPrice,
                Price24H = result.Data.OpenPrice,
                Symbol = result.Data.Symbol,
                Volume = result.Data.Volume24h,
                SourceObject = result.Data
            });
        }

        async Task<WebCallResult<IEnumerable<Ticker>>> IBaseRestClient.GetTickersAsync(CancellationToken ct)
        {
            var result = await ExchangeData.GetTickersAsync().ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<Ticker>>(default);

            return result.As(result.Data.Select(x => new Ticker
            {
                HighPrice = x.HighPrice,
                LastPrice = x.LastPrice,
                LowPrice = x.LowPrice,
                Price24H = x.OpenPrice,
                Symbol = x.Symbol,
                Volume = x.Volume24h,
                SourceObject = x
            }));
        }

        async Task<WebCallResult<IEnumerable<Kline>>> IBaseRestClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime, DateTime? endTime, int? limit, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for BitMart " + nameof(ISpotClient.GetKlinesAsync), nameof(symbol));

            var intervalSec = (int)timespan.TotalSeconds;
            if (!Enum.IsDefined(typeof(KlineInterval), intervalSec))
                throw new ArgumentException("Unsupported timespan for BitMart Klines, check supported intervals using BitMart.Net.Enums.KlineInterval");

            var result = await ExchangeData.GetKlinesAsync(symbol, (KlineInterval)intervalSec, startTime, endTime, limit).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<Kline>>(default);

            return result.As(result.Data.Select(x => new Kline
            {
                HighPrice = x.HighPrice,
                ClosePrice = x.ClosePrice,
                LowPrice = x.LowPrice,
                OpenPrice = x.OpenPrice,
                OpenTime = x.OpenTime,
                Volume = x.Volume,
                SourceObject = x
            }));
        }

        async Task<WebCallResult<OrderBook>> IBaseRestClient.GetOrderBookAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for BitMart " + nameof(ISpotClient.GetOrderBookAsync), nameof(symbol));

            var result = await ExchangeData.GetOrderBookAsync(symbol).ConfigureAwait(false);
            if (!result)
                return result.As<OrderBook>(default);

            return result.As(new OrderBook
            {
                Asks = result.Data.Asks.Select(x => new OrderBookEntry { Price = x.Price, Quantity = x.Quantity }),
                Bids = result.Data.Bids.Select(x => new OrderBookEntry { Price = x.Price, Quantity = x.Quantity }),
                SourceObject = result.Data
            });
        }

        async Task<WebCallResult<IEnumerable<Trade>>> IBaseRestClient.GetRecentTradesAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol) + " required for BitMart " + nameof(ISpotClient.GetRecentTradesAsync), nameof(symbol));

            var result = await ExchangeData.GetTradesAsync(symbol).ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<Trade>>(default);

            return result.As(result.Data.Select(x => new Trade
            {
                Quantity = x.Quantity,
                Price = x.Price,
                Timestamp = x.Timestamp,
                Symbol = x.Symbol,
                SourceObject = x
            }));
        }

        async Task<WebCallResult<IEnumerable<Balance>>> IBaseRestClient.GetBalancesAsync(string? accountId, CancellationToken ct)
        {
            var result = await Account.GetSpotBalancesAsync().ConfigureAwait(false);
            if (!result)
                return result.As<IEnumerable<Balance>>(default);

            return result.As(result.Data.Select(x => new Balance
            {
                Asset = x.Asset,
                Available = x.Available,
                Total = x.Available + x.Frozen,
                SourceObject = x
            }));
        }
    }
}
