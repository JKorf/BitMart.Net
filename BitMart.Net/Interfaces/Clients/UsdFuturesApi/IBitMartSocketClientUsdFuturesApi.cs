using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects.Sockets;
using BitMart.Net.Objects.Models;
using System.Collections.Generic;
using BitMart.Net.Enums;
using CryptoExchange.Net.Interfaces.Clients;

namespace BitMart.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// BitMart UsdFutures streams
    /// </summary>
    public interface IBitMartSocketClientUsdFuturesApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Get the shared socket subscription client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IBitMartSocketClientUsdFuturesApiShared SharedClient { get; }

        /// <summary>
        /// Subscribe to ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-ticker-channel" /><br />
        /// Endpoint:<br />
        /// futures/ticker:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<BitMartFuturesTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-ticker-channel" /><br />
        /// Endpoint:<br />
        /// futures/ticker:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols to subscribe</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitMartFuturesTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-ticker-channel" /><br />
        /// Endpoint:<br />
        /// futures/ticker
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<BitMartFuturesTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-trade-channel" /><br />
        /// Endpoint:<br />
        /// futures/trade:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<BitMartFuturesTradeUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates for multiple symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-trade-channel" /><br />
        /// Endpoint:<br />
        /// futures/trade:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitMartFuturesTradeUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding rate updates for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-funding-rate-channel" /><br />
        /// Endpoint:<br />
        /// futures/fundingRate:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<BitMartFundingRateUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding rate updates for multiple symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-funding-rate-channel" /><br />
        /// Endpoint:<br />
        /// futures/fundingRate:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitMartFundingRateUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-depth-channel" /><br />
        /// Endpoint:<br />
        /// futures/depth{depth}:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="depth">Depth level, 5, 20 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<BitMartFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates for multiple symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-depth-channel" /><br />
        /// Endpoint:<br />
        /// futures/depth{depth}:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols, for example `ETHUSDT`</param>
        /// <param name="depth">Depth level, 5, 20 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<BitMartFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates for a symbol. Pushes the full order book depth with each update
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-depth-all-channel" /><br />
        /// Endpoint:<br />
        /// futures/depthAll{depth}:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="depth">Depth level, 5, 20 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookSnapshotUpdatesAsync(string symbol, int depth, Action<DataEvent<BitMartFuturesFullOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates for multiple symbols. Pushes the full order book depth with each update
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-depth-all-channel" /><br />
        /// Endpoint:<br />
        /// futures/depthAll{depth}:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols, for example `ETHUSDT`</param>
        /// <param name="depth">Depth level, 5, 20 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookSnapshotUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<BitMartFuturesFullOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates for a symbol. First update is the snapshot, after that updates to the snapshot will be pushed
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-depth-all-channel" /><br />
        /// Endpoint:<br />
        /// futures/depthIncrease{depth}:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="depth">Depth level, 5, 20 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookIncrementalUpdatesAsync(string symbol, int depth, Action<DataEvent<BitMartFuturesFullOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates for multiple symbols. First update is the snapshot, after that updates to the snapshot will be pushed
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-depth-all-channel" /><br />
        /// Endpoint:<br />
        /// futures/depthIncrease{depth}:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols, for example `ETHUSDT`</param>
        /// <param name="depth">Depth level, 5, 20 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookIncrementalUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<BitMartFuturesFullOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates for the best ask/bid price for a symbol
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<BitMartBookTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to updates for the best ask/bid price for a symbol
        /// </summary>
        /// <param name="symbols">Symbols, for example `ETHUSDT`</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitMartBookTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-klinebin-channel" /><br />
        /// Endpoint:<br />
        /// futures/klineBin{interval}:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, FuturesStreamKlineInterval interval, Action<DataEvent<BitMartFuturesKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates for multiple symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-klinebin-channel" /><br />
        /// Endpoint:<br />
        /// futures/klineBin{interval}:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols, for example `ETHUSDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, FuturesStreamKlineInterval interval, Action<DataEvent<BitMartFuturesKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe mark price kline/candlestick updates for multiple symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-markprice-klinebin-channel" /><br />
        /// Endpoint:<br />
        /// futures/markPriceKlineBin{interval}:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkKlineUpdatesAsync(string symbol, FuturesStreamKlineInterval interval, Action<DataEvent<BitMartFuturesKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe mark price kline/candlestick updates for multiple symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#public-markprice-klinebin-channel" /><br />
        /// Endpoint:<br />
        /// futures/markPriceKlineBin{interval}:{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols, for example `ETHUSDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkKlineUpdatesAsync(IEnumerable<string> symbols, FuturesStreamKlineInterval interval, Action<DataEvent<BitMartFuturesKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to balance updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#private-assets-channel" /><br />
        /// Endpoint:<br />
        /// futures/asset:{asset}
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<BitMartFuturesBalanceUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to position updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#private-position-channel" /><br />
        /// Endpoint:<br />
        /// futures/position
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(Action<DataEvent<BitMartPositionUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/futuresv2/#private-order-channel" /><br />
        /// Endpoint:<br />
        /// futures/order
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<BitMartFuturesOrderUpdateEvent[]>> onMessage, CancellationToken ct = default);
    }
}
