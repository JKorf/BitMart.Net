using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;

namespace BitMart.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the BitMart websocket API
    /// </summary>
    public interface IBitMartSocketClient : ISocketClient<BitMartCredentials>
    {
        /// <summary>
        /// UsdFutures API endpoints
        /// </summary>
        /// <see cref="IBitMartSocketClientUsdFuturesApi"/>
        public IBitMartSocketClientUsdFuturesApi UsdFuturesApi { get; }

        /// <summary>
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IBitMartSocketClientSpotApi"/>
        public IBitMartSocketClientSpotApi SpotApi { get; }

    }
}
