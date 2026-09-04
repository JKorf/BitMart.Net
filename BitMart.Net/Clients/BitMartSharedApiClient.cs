using BitMart.Net.Interfaces.Clients;
using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;

namespace BitMart.Net.Clients
{
    /// <inheritdoc />
    public class BitMartSharedApiClient : IBitMartSharedApiClient
    {
        /// <inheritdoc />
        public IBitMartRestClientSpotSharedApi SpotRest { get; }
        /// <inheritdoc />
        public IBitMartRestClientUsdFuturesSharedApi FuturesRest { get; }
        /// <inheritdoc />
        public IBitMartSocketClientSpotSharedApi SpotSocket { get; }
        /// <inheritdoc />
        public IBitMartSocketClientUsdFuturesSharedApi FuturesSocket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public BitMartSharedApiClient(
            IBitMartRestClient restClient,
            IBitMartSocketClient socketClient)
        {
            SpotRest = restClient.SpotApi.SharedApi;
            FuturesRest = restClient.UsdFuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            FuturesSocket = socketClient.UsdFuturesApi.SharedApi;
        }
    }
}
