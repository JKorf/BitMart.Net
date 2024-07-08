using CryptoExchange.Net.Objects.Options;

namespace BitMart.Net.Objects.Options
{
    /// <summary>
    /// Options for the BitMartSocketClient
    /// </summary>
    public class BitMartSocketOptions : SocketExchangeOptions<BitMartEnvironment, BitMartApiCredentials>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static BitMartSocketOptions Default { get; set; } = new BitMartSocketOptions()
        {
            Environment = BitMartEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10,
            MaxSocketConnections = 20
        };

         /// <summary>
        /// UsdFutures API options
        /// </summary>
        public SocketApiOptions UsdFuturesOptions { get; private set; } = new SocketApiOptions();

         /// <summary>
        /// Spot API options
        /// </summary>
        public SocketApiOptions SpotOptions { get; private set; } = new SocketApiOptions();

        internal BitMartSocketOptions Copy()
        {
            var options = Copy<BitMartSocketOptions>();
            options.UsdFuturesOptions = UsdFuturesOptions.Copy<SocketApiOptions>();
            options.SpotOptions = SpotOptions.Copy<SocketApiOptions>();
            return options;
        }
    }
}
