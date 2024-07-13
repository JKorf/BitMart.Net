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
        /// 
        /// <para><a href="XXX" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<BitMartFuturesTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="onMessage"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<BitMartFuturesTradeUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="onMessage"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IEnumerable<BitMartFuturesTradeUpdate>>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="depth"></param>
        /// <param name="onMessage"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<BitMartFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbols"></param>
        /// <param name="depth"></param>
        /// <param name="onMessage"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<BitMartFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default);

        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, FuturesStreamKlineInterval interval, Action<DataEvent<BitMartFuturesKlineUpdate>> onMessage, CancellationToken ct = default);

        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, FuturesStreamKlineInterval interval, Action<DataEvent<BitMartFuturesKlineUpdate>> onMessage, CancellationToken ct = default);

        Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<BitMartFuturesBalanceUpdate>> onMessage, CancellationToken ct = default);

        Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(Action<DataEvent<BitMartPositionUpdate>> onMessage, CancellationToken ct = default);

        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<BitMartFuturesOrderUpdate>> onMessage, CancellationToken ct = default);
    }
}
