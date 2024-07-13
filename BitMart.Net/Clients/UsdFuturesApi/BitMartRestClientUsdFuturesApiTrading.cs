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
using System.Drawing;
using System.IO.Pipes;

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
        public async Task<WebCallResult<BitMartFuturesOrder>> GetOrderAsync(string symbol, string orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("", symbol);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/private/order", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<BitMartFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartFuturesOrder>>> GetClosedOrdersAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptionalMilliseconds("start_time", startTime);
            parameters.AddOptionalMilliseconds("end_time", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/private/order-history", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<IEnumerable<BitMartFuturesOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data ?? Array.Empty<BitMartFuturesOrder>());
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartFuturesOrder>>> GetOpenOrdersAsync(string? symbol = null, FuturesOrderType? orderType = null, OrderStatusQuery? status = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("type", orderType);
            parameters.AddOptionalEnum("order_state", status);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/private/get-open-orders", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<IEnumerable<BitMartFuturesOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data ?? Array.Empty<BitMartFuturesOrder>());
        }

        #endregion

        #region Get Trigger Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartTriggerOrder>>> GetTriggerOrdersAsync(string? symbol = null, Type? type = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("type", type);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/private/current-plan-order", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<IEnumerable<BitMartTriggerOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Positions

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartPosition>>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/private/position", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<IEnumerable<BitMartPosition>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position Risk

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartPositionRisk>>> GetPositionRiskAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/private/position-risk", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<IEnumerable<BitMartPositionRisk>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartFuturesUserTrade>>> GetUserTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptionalMilliseconds("start_time", startTime);
            parameters.AddOptionalMilliseconds("end_time", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/private/trades", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<IEnumerable<BitMartFuturesUserTrade>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Order

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartFuturesOrderResponse>> PlaceOrderAsync(string symbol, FuturesSide side, FuturesOrderType type, decimal quantity, decimal? price = null, string? clientOrderId = null, decimal? leverage = null, MarginType? marginType = null, OrderMode? orderMode = null, decimal? triggerPrice = null, decimal? callbackRate = null, TriggerPriceType? triggerPriceType = null, TriggerPriceType? presetTakeProfitPriceType = null, TriggerPriceType? presetStopLossPriceType = null, decimal? presetTakeProfitPrice = null, decimal? presetStopLossPrice = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddString("size", quantity);
            parameters.AddOptionalString("price", price);
            parameters.AddOptional("client_order_id", clientOrderId);
            parameters.AddOptionalString("leverage", leverage);
            parameters.AddOptionalEnum("open_type", marginType);
            parameters.AddOptionalEnum("mode", orderMode);
            parameters.AddOptionalString("activation_price", triggerPrice);
            parameters.AddOptionalString("callback_rate", callbackRate);
            parameters.AddOptionalEnum("activation_price_type", triggerPriceType);
            parameters.AddOptionalEnum("preset_take_profit_price_type", presetTakeProfitPriceType);
            parameters.AddOptionalEnum("preset_stop_loss_price_type", presetStopLossPriceType);
            parameters.AddOptionalString("preset_take_profit_price", presetTakeProfitPrice);
            parameters.AddOptionalString("preset_stop_loss_price", presetStopLossPrice);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/contract/private/submit-order", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<BitMartFuturesOrderResponse>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult> CancelOrderAsync(string symbol, string orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("", symbol);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/contract/private/cancel-order", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/contract/private/cancel-orders", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Trigger Order

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartFuturesOrderId>> PlaceTriggerOrderAsync(string symbol, OrderType orderType, FuturesSide side, decimal quantity, decimal leverage, MarginType marginType, decimal triggerPrice, PriceDirection priceDirection, TriggerPriceType triggerPriceType, OrderMode? orderMode = null, decimal? orderPrice = null, PlanCategory? planCategory = null, decimal? takeProfitPrice = null, decimal? stopLossPrice = null, TriggerPriceType? takeProfitPriceType = null, TriggerPriceType? stopLossPriceType = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("type", orderType);
            parameters.AddEnum("side", side);
            parameters.AddString("size", quantity);
            parameters.AddString("leverage", leverage);
            parameters.AddEnum("open_type", marginType);
            parameters.AddString("trigger_price", triggerPrice);
            parameters.AddEnum("price_way", priceDirection);
            parameters.AddEnum("price_type", triggerPriceType);
            parameters.AddOptionalEnum("mode", orderMode);
            parameters.AddOptionalString("executive_price", orderPrice);
            parameters.AddOptionalEnum("plan_category", planCategory);
            parameters.AddOptionalString("preset_take_profit_price", takeProfitPrice);
            parameters.AddOptionalString("preset_stop_loss_price", stopLossPrice);
            parameters.AddOptionalEnum("preset_take_profit_price_type", takeProfitPriceType);
            parameters.AddOptionalEnum("preset_stop_loss_price_type", stopLossPriceType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/contract/private/submit-plan-order", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<BitMartFuturesOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Trigger Order

        /// <inheritdoc />
        public async Task<WebCallResult> CancelTriggerOrderAsync(string symbol, string orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("order_id", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/contract/private/cancel-plan-order", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
