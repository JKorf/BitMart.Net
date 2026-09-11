using BitMart.Net.Interfaces.Clients;
using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using BitMart.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Options;

namespace BitMart.Net.Clients
{
    /// <inheritdoc />
    public class BitMartSharedApiClient : SharedApiClientBase, IBitMartSharedApiClient
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
            IBitMartSocketClient socketClient,
            IOptions<BitMartOptions> options)
            : base(options.Value.SharedApi.PreferredTransport,
                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,
                restClient.UsdFuturesApi.SharedApi,
                socketClient.UsdFuturesApi.SharedApi)
        {
            SpotRest = restClient.SpotApi.SharedApi;
            FuturesRest = restClient.UsdFuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            FuturesSocket = socketClient.UsdFuturesApi.SharedApi;
        }
    }
}
