using BitMart.Net.Objects.Models;
using BitMart.Net.Enums;
using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BitMart.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// BitMart Spot trading endpoints, placing and managing orders.
    /// </summary>
    public interface IBitMartRestClientSpotApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#new-order-v2-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="side">Order side</param>
        /// <param name="type">Order type</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="price">Limit price</param>
        /// <param name="quoteQuantity">Quantity in quote asset for market orders</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="stpMode">Self trade prevention mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrderId>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? price = null, decimal? quoteQuantity = null, string? clientOrderId = null, SelfTradePreventionMode? stpMode = null, CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders in one call
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#new-batch-order-v4-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="orders">Order parameters</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BitMartOrderIds>> PlaceMultipleOrdersAsync(string symbol, IEnumerable<BitMartOrderRequest> orders, CancellationToken ct = default);

        /// <summary>
        /// Cancel an open order
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#cancel-order-v3-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">Cancel by order id. Either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Cancel by client order Id. Either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#cancel-batch-order-v4-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="orderIds">Order ids to cancel. Either this or clientOrderIds should be provided</param>
        /// <param name="clientOrderIds">Client order ids to cancel. Either this or orderIds should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartCancelOrdersResult>> CancelOrdersAsync(string symbol, IEnumerable<string>? orderIds = null, IEnumerable<string>? clientOrderIds = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders matching the parameters
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#cancel-all-order-v4-signed" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="side">Filter by order side</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> CancelAllOrderAsync(string? symbol = null, OrderSide? side = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new margin order
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#new-margin-order-v1-signed" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="side">Order side</param>
        /// <param name="type">Order type</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="price">Limit price</param>
        /// <param name="quoteQuantity">Quantity in quote asset for market orders</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrderId>> PlaceMarginOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? price = null, decimal? quoteQuantity = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get order details
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#query-order-by-id-v4-signed" /></para>
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="orderQueryState">Order status. If known speeds up the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrder>> GetOrderAsync(string orderId, OrderQueryState? orderQueryState = null, CancellationToken ct = default);

        /// <summary>
        /// Get order details by client order id
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#query-order-by-clientorderid-v4-signed" /></para>
        /// </summary>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="orderQueryState">Order status. If known speeds up the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, OrderQueryState? orderQueryState = null, CancellationToken ct = default);

        /// <summary>
        /// Get current open orders
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#current-open-orders-v4-signed" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="spotOrderMode">Filter by order mode</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrder[]>> GetOpenOrdersAsync(string? symbol = null, SpotMode? spotOrderMode = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get closed orders
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#account-orders-v4-signed" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="spotOrderMode">Filter spot order more</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrder[]>> GetClosedOrdersAsync(string? symbol = null, SpotMode? spotOrderMode = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of user trades
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#account-trade-list-v4-signed" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="spotOrderMode">Filter by order mode</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartUserTrade[]>> GetUserTradesAsync(string? symbol = null, SpotMode? spotOrderMode = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get trades for a specific order
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#order-trade-list-v4-signed" /></para>
        /// </summary>
        /// <param name="orderId">The order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartUserTrade[]>> GetOrderTradesAsync(string orderId, CancellationToken ct = default);

    }
}
