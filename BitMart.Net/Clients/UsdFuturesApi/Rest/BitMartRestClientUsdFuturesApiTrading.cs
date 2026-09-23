using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using BitMart.Net.Objects.Models;
using BitMart.Net.Enums;
using System;
using System.Collections.Generic;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc />
    internal class BitMartRestClientUsdFuturesApiTrading : IBitMartRestClientUsdFuturesApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitMartRestClientUsdFuturesApi _baseClient;
        private readonly ILogger _logger;

        internal BitMartRestClientUsdFuturesApiTrading(ILogger logger, BitMartRestClientUsdFuturesApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<BitMartFuturesOrder>> GetOrderAsync(string symbol, string orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/private/order", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Orders

        /// <inheritdoc />
        public async Task<HttpResult<BitMartFuturesOrder[]>> GetClosedOrdersAsync(string symbol, string? orderId = null, string? clientOrderId = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("order_id", orderId);
            parameters.Add("client_order_id", clientOrderId);
            parameters.Add("start_time", startTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("end_time", endTime, DateTimeSerialization.SecondsNumber);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/private/order-history", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(6, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartFuturesOrder[]>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartFuturesOrder[]>(result);

            return HttpResult.Ok(result, result.Data ?? Array.Empty<BitMartFuturesOrder>());
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<BitMartFuturesOrder[]>> GetOpenOrdersAsync(string? symbol = null, FuturesOrderType? orderType = null, OrderStatusQuery? status = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("type", orderType);
            parameters.Add("order_state", status);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/private/get-open-orders", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartFuturesOrder[]>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartFuturesOrder[]>(result);

            return HttpResult.Ok(result, result.Data ?? Array.Empty<BitMartFuturesOrder>());
        }

        #endregion

        #region Get Trigger Orders

        /// <inheritdoc />
        public async Task<HttpResult<BitMartTriggerOrder[]>> GetTriggerOrdersAsync(string? symbol = null, OrderType? type = null, int? limit = null, TriggerPlanType? planType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("type", type);
            parameters.Add("limit", limit);
            parameters.Add("plan_type", planType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/private/current-plan-order", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(50, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartTriggerOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Positions

        /// <inheritdoc />
        public async Task<HttpResult<BitMartPosition[]>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/private/position", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(6, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position Risk

        /// <inheritdoc />
        public async Task<HttpResult<BitMartPositionRisk[]>> GetPositionRiskAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/private/position-risk", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(24, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartPositionRisk[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<BitMartFuturesUserTrade[]>> GetUserTradesAsync(
            string? symbol = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            long? orderId = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("start_time", startTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("end_time", endTime, DateTimeSerialization.SecondsNumber);
            parameters.Add("order_id", orderId);
            parameters.Add("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/private/trades", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(6, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartFuturesUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Order

        /// <inheritdoc />
        public async Task<HttpResult<BitMartFuturesOrderResponse>> PlaceOrderAsync(string symbol, FuturesSide side, FuturesOrderType type, int quantity, decimal? price = null, string? clientOrderId = null, decimal? leverage = null, MarginType? marginType = null, OrderMode? orderMode = null, TriggerPriceType? presetTakeProfitPriceType = null, TriggerPriceType? presetStopLossPriceType = null, decimal? presetTakeProfitPrice = null, decimal? presetStopLossPrice = null, StpMode? stpMode = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", side, EnumSerialization.Number);
            parameters.Add("type", type);
            parameters.Add("size", quantity);
            parameters.Add("price", price);
            parameters.Add("client_order_id", clientOrderId);
            parameters.Add("leverage", leverage);
            parameters.Add("open_type", marginType);
            parameters.Add("mode", orderMode, EnumSerialization.Number);
            parameters.Add("preset_take_profit_price_type", presetTakeProfitPriceType);
            parameters.Add("preset_stop_loss_price_type", presetStopLossPriceType);
            parameters.Add("preset_take_profit_price", presetTakeProfitPrice);
            parameters.Add("preset_stop_loss_price", presetStopLossPrice);
            parameters.Add("stp_mode", stpMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/contract/private/submit-order", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(24, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartFuturesOrderResponse>(request, parameters, ct, additionalHeaders:new Dictionary<string, string>
            {
                { "X-BM-BROKER-ID", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
            }).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Trailing Order

        /// <inheritdoc />
        public async Task<HttpResult<BitMartOrderId>> PlaceTrailingOrderAsync(
            string symbol,
            FuturesSide side,
            int quantity,
            decimal leverage,
            MarginType marginType,
            decimal triggerPrice,
            decimal callbackRate,
            TriggerPriceType triggerPriceType,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", side, EnumSerialization.Number);
            parameters.Add("leverage", leverage);
            parameters.Add("open_type", marginType);
            parameters.Add("size", quantity);
            parameters.Add("activation_price", triggerPrice);
            parameters.Add("callback_rate", callbackRate);
            parameters.Add("activation_price_type", triggerPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/contract/private/submit-trail-order", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(24, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartOrderId>(request, parameters, ct, additionalHeaders: new Dictionary<string, string>
            {
                { "X-BM-BROKER-ID", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
            }).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Trailing Order

        /// <inheritdoc />
        public async Task<HttpResult> CancelTrailingOrderAsync(string symbol, string orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/contract/private/cancel-trail-order", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(40, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult> CancelOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("order_id", orderId);
            parameters.Add("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/contract/private/cancel-order", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(40, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<HttpResult> CancelOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/contract/private/cancel-orders", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult<BitMartFuturesOrderId>> PlaceTriggerOrderAsync(string symbol, OrderType orderType, FuturesSide side, int quantity, decimal leverage, MarginType marginType, decimal triggerPrice, PriceDirection priceDirection, TriggerPriceType triggerPriceType, OrderMode? orderMode = null, decimal? orderPrice = null, PlanCategory? planCategory = null, decimal? takeProfitPrice = null, decimal? stopLossPrice = null, TriggerPriceType? takeProfitPriceType = null, TriggerPriceType? stopLossPriceType = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("type", orderType);
            parameters.Add("side", side, EnumSerialization.Number);
            parameters.Add("size", quantity);
            parameters.Add("leverage", leverage);
            parameters.Add("open_type", marginType);
            parameters.Add("trigger_price", triggerPrice);
            parameters.Add("price_way", priceDirection == PriceDirection.ShortDirection ? 2 : 1);
            parameters.Add("price_type", triggerPriceType, EnumSerialization.Number);
            parameters.Add("mode", orderMode, EnumSerialization.Number);
            parameters.Add("executive_price", orderPrice);
            parameters.Add("plan_category", planCategory, EnumSerialization.Number);
            parameters.Add("preset_take_profit_price", takeProfitPrice);
            parameters.Add("preset_stop_loss_price", stopLossPrice);
            parameters.Add("preset_take_profit_price_type", takeProfitPriceType, EnumSerialization.Number);
            parameters.Add("preset_stop_loss_price_type", stopLossPriceType, EnumSerialization.Number);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/contract/private/submit-plan-order", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(24, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartFuturesOrderId>(request, parameters, ct, additionalHeaders: new Dictionary<string, string>
            {
                { "X-BM-BROKER-ID", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
            }).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult> CancelTriggerOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("order_id", orderId);
            parameters.Add("client_order_id", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/contract/private/cancel-plan-order", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(40, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Tp Sl Order

        /// <inheritdoc />
        public async Task<HttpResult<BitMartOrderId>> PlaceTpSlOrderAsync(string symbol, TplSlOrderType tpSlType, FuturesSide orderSide, decimal triggerPrice, TriggerPriceType priceType, PlanCategory planCategory, decimal? executionPrice = null, int? quantity = null, string? clientOrderId = null, OrderType? triggerOrderType = null, CancellationToken ct = default)
        {
            if (orderSide != FuturesSide.BuyCloseShort && orderSide != FuturesSide.SellCloseLong)
                throw new ArgumentException("Order side should be either BuyCloseShort or SellCloseLong");

            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("type", tpSlType);
            parameters.Add("side", orderSide, EnumSerialization.Number);
            parameters.Add("trigger_price", triggerPrice);
            parameters.Add("price_type", priceType, EnumSerialization.Number);
            parameters.Add("plan_category", planCategory, EnumSerialization.Number);
            parameters.Add("executive_price", executionPrice);
            parameters.Add("size", quantity);
            parameters.Add("client_order_id", clientOrderId);
            parameters.Add("category", triggerOrderType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/contract/private/submit-tp-sl-order", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<BitMartOrderId>(request, parameters, ct, additionalHeaders: new Dictionary<string, string>
            {
                { "X-BM-BROKER-ID", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange) }
            }).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<HttpResult<BitMartOrderId>> EditOrderAsync(string symbol, long? orderId = null, string? clientOrderId = null, decimal? price = null, decimal? quantity = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            parameters.Add("client_order_id", clientOrderId);
            parameters.Add("price", price);
            parameters.Add("size", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/contract/private/modify-limit-order", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<BitMartOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Tp Sl Order

        /// <inheritdoc />
        public async Task<HttpResult<BitMartOrderId>> EditTpSlOrderAsync(string symbol, decimal triggerPrice, TriggerPriceType priceType, PlanCategory planCategory, OrderType orderType, string? orderId = null, decimal? executionPrice = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("trigger_price", triggerPrice);
            parameters.Add("price_type", priceType, EnumSerialization.Number);
            parameters.Add("plan_category", planCategory, EnumSerialization.Number);
            parameters.Add("category", orderType);
            parameters.Add("orderId", orderId);
            parameters.Add("executive_price", executionPrice);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/contract/private/modify-tp-sl-order", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<BitMartOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult<BitMartOrderId>> EditTriggerOrderAsync(string symbol, decimal triggerPrice, TriggerPriceType priceType, OrderType orderType, string? orderId = null, string? clientOrderId = null, decimal? executionPrice = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("trigger_price", triggerPrice);
            parameters.Add("price_type", priceType, EnumSerialization.Number);
            parameters.Add("type", orderType);
            parameters.Add("orderId", orderId);
            parameters.Add("client_order_id", clientOrderId);
            parameters.Add("executive_price", executionPrice);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/contract/private/modify-plan-order", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<BitMartOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Preset Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult<BitMartOrderId>> EditPresetTriggerOrderAsync(string symbol, string orderId, TriggerPriceType takeProfitPriceType, TriggerPriceType stopLossPriceType, decimal takeProfitPrice, decimal stopLossPrice, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("order_id", orderId);
            parameters.Add("preset_take_profit_price_type", takeProfitPriceType, EnumSerialization.Number);
            parameters.Add("preset_stop_loss_price_type", stopLossPriceType, EnumSerialization.Number);
            parameters.Add("preset_take_profit_price", takeProfitPrice);
            parameters.Add("preset_stop_loss_price", stopLossPrice);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/contract/private/modify-preset-plan-order", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<BitMartOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All After

        /// <inheritdoc />
        public async Task<HttpResult<BitMartCancelAfter>> CancelAllAfterAsync(string symbol, TimeSpan timespan, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("timeout", (int)timespan.TotalSeconds);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/contract/private/cancel-all-after", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(4, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartCancelAfter>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
