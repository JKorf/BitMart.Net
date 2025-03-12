using CryptoExchange.Net.Objects.Options;

namespace BitMart.Net.Objects.Options
{
    /// <summary>
    /// Options for the BitMartRestClient
    /// </summary>
    public class BitMartRestOptions : RestExchangeOptions<BitMartEnvironment>
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
        /// UsdFutures API options
        /// </summary>
        public RestApiOptions UsdFuturesOptions { get; private set; } = new RestApiOptions();

         /// <summary>
        /// Spot API options
        /// </summary>
        public RestApiOptions SpotOptions { get; private set; } = new RestApiOptions();

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
