using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Enums;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using BitMart.Net.Objects.Models;
using BitMart.Net.Objects.Internal;
using System.Collections.Generic;
using CryptoExchange.Net.RateLimiting.Guards;
using System.Linq;

namespace BitMart.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BitMartRestClientSpotApiTrading : IBitMartRestClientSpotApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitMartRestClientSpotApi _baseClient;
        private readonly ILogger _logger;

        internal BitMartRestClientSpotApiTrading(ILogger logger, BitMartRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region Place Order

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartOrderId>> PlaceOrderAsync(
            string symbol, 
            OrderSide side, 
            OrderType type, 
            decimal? quantity = null,
            decimal? price = null,
            decimal? quoteQuantity = null, 
            string? clientOrderId = null,
            SelfTradePreventionMode? stpMode = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptionalString("size", quantity);
            parameters.AddOptionalString("price", price);
            parameters.AddOptionalString("notional", quoteQuantity);
            parameters.AddOptional("client_order_id", clientOrderId);
            parameters.AddOptionalEnum("stpMode", stpMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/spot/v2/submit_order", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartOrderId>(request, parameters, ct, additionalHeaders: new Dictionary<string, string>
            {
                { "X-BM-BROKER-ID", _baseClient._brokerId }
            }).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartOrderIds>> PlaceMultipleOrdersAsync(string symbol, IEnumerable<BitMartOrderRequest> orders, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("orderParams", orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/spot/v4/batch_orders", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartOrderIdsWrapper>(request, parameters, ct, additionalHeaders: new Dictionary<string, string>
            {
                { "X-BM-BROKER-ID", _baseClient._brokerId }
            }).ConfigureAwait(false);
            if (!result)
                return result.As<BitMartOrderIds>(default);

            if (result.Data.Code != 0)
                return result.AsError<BitMartOrderIds>(new ServerError(result.Data.Code, result.Data.Message));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult> CancelOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("order_id", orderId);
            parameters.AddOptional("client_order_id", clientOrderId);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/spot/v3/cancel_order", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartResult>(request, parameters, ct).ConfigureAwait(false);
            return result.AsDataless();
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartCancelOrdersResult>> CancelOrdersAsync(string symbol, IEnumerable<string>? orderIds = null, IEnumerable<string>? clientOrderIds = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("orderIds", orderIds?.ToArray());
            parameters.AddOptional("clientOrderIds", clientOrderIds?.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/spot/v4/cancel_orders", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartCancelOrdersResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllOrderAsync(string? symbol = null, OrderSide? side = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("side", side);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/spot/v4/cancel_all", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(1, TimeSpan.FromSeconds(3), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place Margin Order

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartOrderId>> PlaceMarginOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? price = null, decimal? quoteQuantity = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptionalString("size", quantity);
            parameters.AddOptionalString("price", price);
            parameters.AddOptionalString("notional", quoteQuantity);
            parameters.AddOptional("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/spot/v1/margin/submit_order", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(20, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartOrderId>(request, parameters, ct, additionalHeaders: new Dictionary<string, string>
            {
                { "X-BM-BROKER-ID", _baseClient._brokerId }
            }).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartOrder>> GetOrderAsync(string orderId, OrderQueryState? orderQueryState = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("orderId", orderId);
            parameters.AddOptionalEnum("queryState", orderQueryState);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/spot/v4/query/order", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order By Client Order Id

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, OrderQueryState? orderQueryState = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("clientOrderId", clientOrderId);
            parameters.AddOptionalEnum("queryState", orderQueryState);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/spot/v4/query/client-order", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartOrder[]>> GetOpenOrdersAsync(string? symbol = null, SpotMode? spotOrderMode = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("orderMode", spotOrderMode);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/spot/v4/query/open-orders", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Orders

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartOrder[]>> GetClosedOrdersAsync(string? symbol = null, SpotMode? spotOrderMode = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("orderMode", spotOrderMode);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/spot/v4/query/history-orders", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartUserTrade[]>> GetUserTradesAsync(string? symbol = null, SpotMode? spotOrderMode = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("orderMode", spotOrderMode);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/spot/v4/query/trades", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Trades

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartUserTrade[]>> GetOrderTradesAsync(string orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("orderId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "spot/v4/query/order-trades", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
