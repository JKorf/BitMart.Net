using CryptoExchange.Net.Objects;
using BitMart.Net.Objects;

namespace BitMart.Net
{
    /// <summary>
    /// BitMart environments
    /// </summary>
    public class BitMartEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Rest API address
        /// </summary>
        public string RestClientAddress { get; }

        /// <summary>
        /// Socket Spot API address
        /// </summary>
        public string SocketClientSpotAddress { get; }

        /// <summary>
        /// Socket Perpetual Futures API address
        /// </summary>
        public string SocketClientPerpetualFuturesAddress { get; }

        internal BitMartEnvironment(
            string name,
            string restAddress,
            string streamSpotAddress,
            string streamPerpetualFuturesAddress) :
            base(name)
        {
            RestClientAddress = restAddress;
            SocketClientSpotAddress = streamSpotAddress;
            SocketClientPerpetualFuturesAddress = streamPerpetualFuturesAddress;
        }

        /// <summary>
        /// Live environment
        /// </summary>
        public static BitMartEnvironment Live { get; }
            = new BitMartEnvironment(TradeEnvironmentNames.Live,
                                     BitMartApiAddresses.Default.RestClientAddress,
                                     BitMartApiAddresses.Default.SocketSpotClientAddress,
                                     BitMartApiAddresses.Default.SocketPerpetualFuturesClientAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="restAddress"></param>
        /// <param name="spotSocketStreamsAddress"></param>
        /// <param name="perpetualFuturesSocketStreamAddress"></param>
        /// <returns></returns>
        public static BitMartEnvironment CreateCustom(
                        string name,
                        string restAddress,
                        string spotSocketStreamsAddress,
                        string perpetualFuturesSocketStreamAddress)
            => new BitMartEnvironment(name, restAddress, spotSocketStreamsAddress, perpetualFuturesSocketStreamAddress);
    }
}
