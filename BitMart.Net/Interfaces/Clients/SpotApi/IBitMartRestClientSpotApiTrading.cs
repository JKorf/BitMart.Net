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
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#new-order-v2-signed" /><br />
        /// Endpoint:<br />
        /// POST /spot/v2/submit_order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="type">["<c>type</c>"] Order type</param>
        /// <param name="quantity">["<c>size</c>"] Quantity</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="quoteQuantity">["<c>notional</c>"] Quantity in quote asset for market orders</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id</param>
        /// <param name="stpMode">["<c>stpMode</c>"] Self trade prevention mode</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrderId>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? price = null, decimal? quoteQuantity = null, string? clientOrderId = null, SelfTradePreventionMode? stpMode = null, CancellationToken ct = default);

        /// <summary>
        /// Place multiple orders in one call
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#new-batch-order-v4-signed" /><br />
        /// Endpoint:<br />
        /// POST /spot/v4/batch_orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="orders">["<c>orderParams</c>"] Order parameters</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BitMartOrderIds>> PlaceMultipleOrdersAsync(string symbol, IEnumerable<BitMartOrderRequest> orders, CancellationToken ct = default);

        /// <summary>
        /// Cancel an open order
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#cancel-order-v3-signed" /><br />
        /// Endpoint:<br />
        /// POST /spot/v3/cancel_order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">["<c>order_id</c>"] Cancel by order id. Either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Cancel by client order Id. Either this or orderId should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelOrderAsync(string symbol, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#cancel-batch-order-v4-signed" /><br />
        /// Endpoint:<br />
        /// POST /spot/v4/cancel_orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="orderIds">["<c>orderIds</c>"] Order ids to cancel. Either this or clientOrderIds should be provided</param>
        /// <param name="clientOrderIds">["<c>clientOrderIds</c>"] Client order ids to cancel. Either this or orderIds should be provided</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartCancelOrdersResult>> CancelOrdersAsync(string symbol, IEnumerable<string>? orderIds = null, IEnumerable<string>? clientOrderIds = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders matching the parameters
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#cancel-all-order-v4-signed" /><br />
        /// Endpoint:<br />
        /// POST /spot/v4/cancel_all
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="side">["<c>side</c>"] Filter by order side</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> CancelAllOrderAsync(string? symbol = null, OrderSide? side = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new margin order
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#new-margin-order-v1-signed" /><br />
        /// Endpoint:<br />
        /// POST /spot/v1/margin/submit_order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="type">["<c>type</c>"] Order type</param>
        /// <param name="quantity">["<c>size</c>"] Quantity</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="quoteQuantity">["<c>notional</c>"] Quantity in quote asset for market orders</param>
        /// <param name="clientOrderId">["<c>client_order_id</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrderId>> PlaceMarginOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? price = null, decimal? quoteQuantity = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get order details
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#query-order-by-id-v4-signed" /><br />
        /// Endpoint:<br />
        /// POST /spot/v4/query/order
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Id of the order</param>
        /// <param name="orderQueryState">["<c>queryState</c>"] Order status. If known speeds up the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrder>> GetOrderAsync(string orderId, OrderQueryState? orderQueryState = null, CancellationToken ct = default);

        /// <summary>
        /// Get order details by client order id
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#query-order-by-clientorderid-v4-signed" /><br />
        /// Endpoint:<br />
        /// POST /spot/v4/query/client-order
        /// </para>
        /// </summary>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Client order id</param>
        /// <param name="orderQueryState">["<c>queryState</c>"] Order status. If known speeds up the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, OrderQueryState? orderQueryState = null, CancellationToken ct = default);

        /// <summary>
        /// Get current open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#current-open-orders-v4-signed" /><br />
        /// Endpoint:<br />
        /// POST /spot/v4/query/open-orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="spotOrderMode">["<c>orderMode</c>"] Filter by order mode</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrder[]>> GetOpenOrdersAsync(string? symbol = null, SpotMode? spotOrderMode = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get closed orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#account-orders-v4-signed" /><br />
        /// Endpoint:<br />
        /// POST /spot/v4/query/history-orders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="spotOrderMode">["<c>orderMode</c>"] Filter spot order more</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrder[]>> GetClosedOrdersAsync(string? symbol = null, SpotMode? spotOrderMode = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of user trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#account-trade-list-v4-signed" /><br />
        /// Endpoint:<br />
        /// POST /spot/v4/query/trades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="spotOrderMode">["<c>orderMode</c>"] Filter by order mode</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartUserTrade[]>> GetUserTradesAsync(string? symbol = null, SpotMode? spotOrderMode = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get trades for a specific order
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#order-trade-list-v4-signed" /><br />
        /// Endpoint:<br />
        /// POST /spot/v4/query/order-trades
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] The order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartUserTrade[]>> GetOrderTradesAsync(string orderId, CancellationToken ct = default);

    }
}
