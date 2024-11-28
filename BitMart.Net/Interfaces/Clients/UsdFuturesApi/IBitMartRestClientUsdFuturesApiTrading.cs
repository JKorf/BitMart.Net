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
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-order-detail-keyed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartFuturesOrder>> GetOrderAsync(string symbol, string orderId, CancellationToken ct = default);

        /// <summary>
        /// Get order history
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-order-history-keyed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartFuturesOrder>>> GetClosedOrdersAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get open orders
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-all-open-orders-keyed" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="orderType">Filter by order type</param>
        /// <param name="status">Filter by order status</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartFuturesOrder>>> GetOpenOrdersAsync(string? symbol = null, FuturesOrderType? orderType = null, OrderStatusQuery? status = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get open trigger orders
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-all-current-plan-orders-keyed" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="type">Filter by order type</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="planType">Filter by plan type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartTriggerOrder>>> GetTriggerOrdersAsync(string? symbol = null, OrderType? type = null, int? limit = null, TriggerPlanType? planType = null, CancellationToken ct = default);

        /// <summary>
        /// Get current positions
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-current-position-keyed" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartPosition>>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get position risk
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-current-position-risk-details-keyed" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartPositionRisk>>> GetPositionRiskAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-order-trade-keyed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartFuturesUserTrade>>> GetUserTradesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new order
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#submit-order-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="side">Order side</param>
        /// <param name="type">Order type</param>
        /// <param name="quantity">Quantity in number of contracts</param>
        /// <param name="price">Limit price</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="leverage">Leverage</param>
        /// <param name="marginType">Margin type</param>
        /// <param name="orderMode">Order mode</param>
        /// <param name="triggerPrice">Trigger price</param>
        /// <param name="callbackRate">Trailing order callback rate</param>
        /// <param name="triggerPriceType">Trigger price type</param>
        /// <param name="presetTakeProfitPriceType">Take profit price type</param>
        /// <param name="presetStopLossPriceType">Stop loss price type</param>
        /// <param name="presetTakeProfitPrice">Take profit price</param>
        /// <param name="presetStopLossPrice">Stop loss price</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartFuturesOrderResponse>> PlaceOrderAsync(string symbol, FuturesSide side, FuturesOrderType type, int quantity, decimal? price = null, string? clientOrderId = null, decimal? leverage = null, MarginType? marginType = null, OrderMode? orderMode = null, decimal? triggerPrice = null, decimal? callbackRate = null, TriggerPriceType? triggerPriceType = null, TriggerPriceType? presetTakeProfitPriceType = null, TriggerPriceType? presetStopLossPriceType = null, decimal? presetTakeProfitPrice = null, decimal? presetStopLossPrice = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel an active order
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#cancel-order-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">The order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">The client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelOrderAsync(string symbol, string orderId, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders on a symbol
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#cancel-all-orders-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Place a new trigger order
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#submit-plan-order-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderType">The order type</param>
        /// <param name="side">Side</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="leverage">Order leverage</param>
        /// <param name="marginType">Margin type</param>
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
        Task<WebCallResult<BitMartFuturesOrderId>> PlaceTriggerOrderAsync(string symbol, OrderType orderType, FuturesSide side, int quantity, decimal leverage, MarginType marginType, decimal triggerPrice, PriceDirection priceDirection, TriggerPriceType triggerPriceType, OrderMode? orderMode = null, decimal? orderPrice = null, PlanCategory? planCategory = null, decimal? takeProfitPrice = null, decimal? stopLossPrice = null, TriggerPriceType? takeProfitPriceType = null, TriggerPriceType? stopLossPriceType = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a trigger order
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#cancel-plan-order-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">The order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">The client order id, either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelTriggerOrderAsync(string symbol, string orderId, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new tp/sl order
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#submit-tp-or-sl-order-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="tpSlType">Take profit or stop loss</param>
        /// <param name="orderSide">Order side, either BuyCloseShort or SellCloseLong</param>
        /// <param name="triggerPrice">Trigger price</param>
        /// <param name="priceType">Trigger price type</param>
        /// <param name="planCategory">Plan category</param>
        /// <param name="executionPrice">Execution price</param>
        /// <param name="quantity">Quantity to close. Defaults to position size</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="triggerOrderType">Type of order to place when triggered</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrderId>> PlaceTpSlOrderAsync(string symbol, TplSlOrderType tpSlType, FuturesSide orderSide, decimal triggerPrice, TriggerPriceType priceType, PlanCategory planCategory, decimal? executionPrice = null, int? quantity = null, string? clientOrderId = null, OrderType? triggerOrderType = null, CancellationToken ct = default);

        /// <summary>
        /// Edit an existing tp/sl order
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#modify-tp-sl-order-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="triggerPrice">Trigger price</param>
        /// <param name="executionPrice">Execution price</param>
        /// <param name="priceType">Price type</param>
        /// <param name="planCategory">Plan category</param>
        /// <param name="orderType">Order type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrderId>> EditTpSlOrderAsync(string symbol, decimal triggerPrice, TriggerPriceType priceType, PlanCategory planCategory, OrderType orderType, string? orderId = null, string? clientOrderId = null, decimal? executionPrice = null, CancellationToken ct = default);

        /// <summary>
        /// Edit an existing plan order
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#modify-plan-order-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">Order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Client order id, either this or orderId should be provided</param>
        /// <param name="triggerPrice">Trigger price</param>
        /// <param name="executionPrice">Execution price</param>
        /// <param name="priceType">Price type</param>
        /// <param name="orderType">Order type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrderId>> EditTriggerOrderAsync(string symbol, decimal triggerPrice, TriggerPriceType priceType, OrderType orderType, string? orderId = null, string? clientOrderId = null, decimal? executionPrice = null, CancellationToken ct = default);

        /// <summary>
        /// Edit an preset plan order
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#modify-preset-plan-order-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">The order id</param>
        /// <param name="takeProfitPriceType">Take profit price type</param>
        /// <param name="stopLossPriceType">Stop loss price type</param>
        /// <param name="takeProfitPrice">Take profit price</param>
        /// <param name="stopLossPrice">Stop loss price</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrderId>> EditPresetTriggerOrderAsync(string symbol, string orderId, TriggerPriceType takeProfitPriceType, TriggerPriceType stopLossPriceType, decimal takeProfitPrice, decimal stopLossPrice, CancellationToken ct = default);

    }
}
