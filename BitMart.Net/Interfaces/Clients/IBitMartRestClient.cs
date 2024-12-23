using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Options;

namespace BitMart.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the BitMart Rest API. 
    /// </summary>
    public interface IBitMartRestClient : IRestClient
    {
        /// <summary>
        /// UsdFutures API endpoints
        /// </summary>
        public IBitMartRestClientUsdFuturesApi UsdFuturesApi { get; }

        /// <summary>
        /// Spot API endpoints
        /// </summary>
        public IBitMartRestClientSpotApi SpotApi { get; }

        /// <summary>
        /// Update specific options
        /// </summary>
        /// <param name="options">Options to update. Only specific options are changable after the client has been created</param>
        void SetOptions(UpdateOptions options);

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
