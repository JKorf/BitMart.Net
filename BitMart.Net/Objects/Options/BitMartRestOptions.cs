using CryptoExchange.Net.Objects.Options;

namespace BitMart.Net.Objects.Options
{
    /// <summary>
    /// Options for the BitMartRestClient
    /// </summary>
    public class BitMartRestOptions : RestExchangeOptions<BitMartEnvironment, BitMartApiCredentials>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static BitMartRestOptions Default { get; set; } = new BitMartRestOptions()
        {
            Environment = BitMartEnvironment.Live,
            AutoTimestamp = true
        };
        
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


        internal BitMartRestOptions Copy()
        {
            var options = Copy<BitMartRestOptions>();
            options.BrokerId = BrokerId;
            options.UsdFuturesOptions = UsdFuturesOptions.Copy<RestApiOptions>();
            options.SpotOptions = SpotOptions.Copy<RestApiOptions>();
            return options;
        }
    }
}
