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
            options.UsdFuturesOptions = UsdFuturesOptions.Copy<RestApiOptions>();
            options.SpotOptions = SpotOptions.Copy<RestApiOptions>();
            return options;
        }
    }
}
