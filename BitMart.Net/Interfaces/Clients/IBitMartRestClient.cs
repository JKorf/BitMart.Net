using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;

namespace BitMart.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the BitMart Rest API. 
    /// </summary>
    public interface IBitMartRestClient : IRestClient<BitMartCredentials>
    {
        /// <summary>
        /// UsdFutures API endpoints
        /// </summary>
        /// <see cref="IBitMartRestClientUsdFuturesApi"/>
        public IBitMartRestClientUsdFuturesApi UsdFuturesApi { get; }

        /// <summary>
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IBitMartRestClientSpotApi"/>
        public IBitMartRestClientSpotApi SpotApi { get; }

    }
}
