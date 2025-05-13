using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Options;

namespace BitMart.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the BitMart websocket API
    /// </summary>
    public interface IBitMartSocketClient : ISocketClient
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

        /// <summary>
        /// Update specific options
        /// </summary>
        /// <param name="options">Options to update. Only specific options are changeable after the client has been created</param>
        void SetOptions(UpdateOptions options);

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
