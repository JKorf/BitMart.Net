using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Sockets;
using BitMart.Net.Objects.Models;
using System.Collections.Generic;
using BitMart.Net.Enums;

namespace BitMart.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// BitMart UsdFutures streams
    /// </summary>
    public interface IBitMartSocketClientUsdFuturesApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exhanges to allow for a common implementation for different exchanges.
        /// </summary>
        IBitMartSocketClientUsdFuturesApiShared SharedClient { get; }

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#public-ticker-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<BitMartFuturesTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates for a symbol
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#public-trade-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<BitMartFuturesTradeUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates for multiple symbols
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#public-trade-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IEnumerable<BitMartFuturesTradeUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates for a symbol
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#public-depth-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="depth">Depth level, 5, 20 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<BitMartFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates for multiple symbols
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#public-depth-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols, for example `ETHUSDT`</param>
        /// <param name="depth">Depth level, 5, 20 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<BitMartFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates for a symbol
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#public-klinebin-channel" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, FuturesStreamKlineInterval interval, Action<DataEvent<BitMartFuturesKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates for multiple symbols
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#public-klinebin-channel" /></para>
        /// </summary>
        /// <param name="symbols">Symbols, for example `ETHUSDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, FuturesStreamKlineInterval interval, Action<DataEvent<BitMartFuturesKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to balance updates
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#private-assets-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<BitMartFuturesBalanceUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to position updates
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#private-position-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(Action<DataEvent<IEnumerable<BitMartPositionUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order updates
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#private-order-channel" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<IEnumerable<BitMartFuturesOrderUpdateEvent>>> onMessage, CancellationToken ct = default);
    }
}
