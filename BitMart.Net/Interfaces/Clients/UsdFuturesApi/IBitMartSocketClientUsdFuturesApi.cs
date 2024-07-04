using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Sockets;
using BitMart.Net.Objects.Models;

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
        Task<CallResult<UpdateSubscription>> SubscribeToXXXUpdatesAsync(Action<DataEvent<BitMartModel>> onMessage, CancellationToken ct = default);
    }
}
