using BitMart.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.Logging;

namespace BitMart.Net
{
    /// <inheritdoc/>
    public class BitMartUserSpotDataTracker : UserSpotDataTracker
    {
        /// <summary>
        /// ctor
        /// </summary>
        public BitMartUserSpotDataTracker(
            ILogger<BitMartUserSpotDataTracker> logger,
            IBitMartRestClient restClient,
            IBitMartSocketClient socketClient,
            string? userIdentifier,
            SpotUserDataTrackerConfig? config) : base(
                logger,
                restClient.SpotApi.SharedApi,
                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                null,
                userIdentifier,
                config ?? new SpotUserDataTrackerConfig())
        {
        }
    }

    /// <inheritdoc/>
    public class BitMartUserUsdFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc/>
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        /// <summary>
        /// ctor
        /// </summary>
        public BitMartUserUsdFuturesDataTracker(
            ILogger<BitMartUserUsdFuturesDataTracker> logger,
            IBitMartRestClient restClient,
            IBitMartSocketClient socketClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig? config) : base(logger,
                restClient.UsdFuturesApi.SharedApi,
                restClient.UsdFuturesApi.SharedApi,
                socketClient.UsdFuturesApi.SharedApi,

                restClient.UsdFuturesApi.SharedApi,
                restClient.UsdFuturesApi.SharedApi,
                socketClient.UsdFuturesApi.SharedApi,

                restClient.UsdFuturesApi.SharedApi,
                null,

                restClient.UsdFuturesApi.SharedApi,
                null,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig())
        {
        }
    }
}
