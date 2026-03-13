using CryptoExchange.Net.Objects.Options;
using System;

namespace BitMart.Net.Objects.Options
{
    /// <summary>
    /// Options for the BitMartRestClient
    /// </summary>
    public class BitMartRestOptions : RestExchangeOptions<BitMartEnvironment, BitMartCredentials>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static BitMartRestOptions Default { get; set; } = new BitMartRestOptions()
        {
            Environment = BitMartEnvironment.Live,
            AutoTimestamp = true
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BitMartRestOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Set a broker id which will be send in the request headers
        /// </summary>
        public string? BrokerId { get; set; }

        /// <summary>
        /// The receive window for requests
        /// </summary>
        public TimeSpan? ReceiveWindow { get; set; }

         /// <summary>
        /// UsdFutures API options
        /// </summary>
        public RestApiOptions<BitMartCredentials> UsdFuturesOptions { get; private set; } = new RestApiOptions<BitMartCredentials>();

         /// <summary>
        /// Spot API options
        /// </summary>
        public RestApiOptions<BitMartCredentials> SpotOptions { get; private set; } = new RestApiOptions<BitMartCredentials>();

        internal BitMartRestOptions Set(BitMartRestOptions targetOptions)
        {
            targetOptions = base.Set<BitMartRestOptions>(targetOptions);
            targetOptions.BrokerId = BrokerId;
            targetOptions.UsdFuturesOptions = UsdFuturesOptions.Set(targetOptions.UsdFuturesOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            return targetOptions;
        }
    }
}
