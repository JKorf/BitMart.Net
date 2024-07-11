using BitMart.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using BitMart.Net.Enums;
using System.Collections.Generic;
using System.Drawing;

namespace BitMart.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// BitMart UsdFutures trading endpoints, placing and managing orders.
    /// </summary>
    public interface IBitMartRestClientUsdFuturesApiTrading
    {
        /// <summary>
        /// Get an order by id
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#get-order-detail-keyed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartFuturesOrder>> GetOrderAsync(string symbol, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Get order history
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#get-order-history-keyed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartFuturesOrder>>> GetClosedOrdersAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get open orders
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#get-all-open-orders-keyed" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="orderType">Filter by order type</param>
        /// <param name="status">Filter by order status</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartFuturesOrder>>> GetOpenOrdersAsync(string? symbol = null, FuturesOrderType? orderType = null, OrderStatusQuery? status = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get open trigger orders
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#get-all-current-plan-orders-keyed" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="type">Filter by order type</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartTriggerOrder>>> GetTriggerOrdersAsync(string? symbol = null, Type? type = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get current positions
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#get-current-position-keyed" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartPosition>>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get position risk
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#get-current-position-risk-details-keyed" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartPositionRisk>>> GetPositionRiskAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#get-order-trade-keyed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartFuturesUserTrade>>> GetUserTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#submit-order-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="side">Order side</param>
        /// <param name="type">Order type</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="price">Limit price</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="leverage">Leverage</param>
        /// <param name="openType">Margin type</param>
        /// <param name="orderMode">Order mode</param>
        /// <param name="triggerPrice">Trigger price</param>
        /// <param name="callbackRate">Trailing order callback rate</param>
        /// <param name="triggerPriceType">Trigger price type</param>
        /// <param name="presetTakeProfitPriceType">Take profit price type</param>
        /// <param name="presetStopLossPriceType">Stop loss price type</param>
        /// <param name="presetTakeProfitPrice">Take profit price</param>
        /// <param name="presetStopLossPrice">Stop loss price</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartFuturesOrderResponse>> PlaceOrderAsync(string symbol, FuturesSide side, FuturesOrderType type, decimal quantity, decimal? price = null, string? clientOrderId = null, decimal? leverage = null, MarginType? openType = null, OrderMode? orderMode = null, decimal? triggerPrice = null, decimal? callbackRate = null, TriggerPriceType? triggerPriceType = null, TriggerPriceType? presetTakeProfitPriceType = null, TriggerPriceType? presetStopLossPriceType = null, decimal? presetTakeProfitPrice = null, decimal? presetStopLossPrice = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an active order
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#cancel-order-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="orderId">The id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelOrderAsync(string symbol, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders on a symbol
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#cancel-all-orders-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Place a new trigger order
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#submit-plan-order-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="orderType">The order type</param>
        /// <param name="side">Side</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="leverage">Order leverage</param>
        /// <param name="openType">Margin type</param>
        /// <param name="triggerPrice">Trigger price</param>
        /// <param name="priceDirection">Price direction</param>
        /// <param name="triggerPriceType">Trigger price type</param>
        /// <param name="orderMode">Mode</param>
        /// <param name="orderPrice">Order price for limit order</param>
        /// <param name="planCategory">Plan category</param>
        /// <param name="takeProfitPrice">Take profit price</param>
        /// <param name="stopLossPrice">Stop loss price</param>
        /// <param name="takeProfitPriceType">Take profit price type</param>
        /// <param name="stopLossPriceType">Stop loss price type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartFuturesOrderId>> PlaceTriggerOrderAsync(string symbol, OrderType orderType, FuturesSide side, decimal quantity, decimal leverage, MarginType openType, decimal triggerPrice, PriceDirection priceDirection, TriggerPriceType triggerPriceType, OrderMode? orderMode = null, decimal? orderPrice = null, PlanCategory? planCategory = null, decimal? takeProfitPrice = null, decimal? stopLossPrice = null, TriggerPriceType? takeProfitPriceType = null, TriggerPriceType? stopLossPriceType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a trigger order
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#cancel-plan-order-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="orderId">The id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelTriggerOrderAsync(string symbol, string orderId, CancellationToken ct = default);

    }
}
