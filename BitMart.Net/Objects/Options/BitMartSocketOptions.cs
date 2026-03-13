using CryptoExchange.Net.Objects.Options;

namespace BitMart.Net.Objects.Options
{
    /// <summary>
    /// Options for the BitMartSocketClient
    /// </summary>
    public class BitMartSocketOptions : SocketExchangeOptions<BitMartEnvironment, BitMartCredentials>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static BitMartSocketOptions Default { get; set; } = new BitMartSocketOptions()
        {
            Environment = BitMartEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10,
            MaxSocketConnections = 20
        };

        /// <summary>
        /// ctor
        /// </summary>
        public BitMartSocketOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// UsdFutures API options
        /// </summary>
        public SocketApiOptions<BitMartCredentials> UsdFuturesOptions { get; private set; } = new SocketApiOptions<BitMartCredentials>();

         /// <summary>
        /// Spot API options
        /// </summary>
        public SocketApiOptions<BitMartCredentials> SpotOptions { get; private set; } = new SocketApiOptions<BitMartCredentials>();

        internal BitMartSocketOptions Set(BitMartSocketOptions targetOptions)
        {
            targetOptions = base.Set<BitMartSocketOptions>(targetOptions);
            targetOptions.UsdFuturesOptions = UsdFuturesOptions.Set(targetOptions.UsdFuturesOptions);
            targetOptions.SpotOptions = UsdFuturesOptions.Set(targetOptions.UsdFuturesOptions);
            return targetOptions;
        }
    }
}
