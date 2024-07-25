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
        public string RestSpotClientAddress { get; }
        /// <summary>
        /// Rest API address
        /// </summary>
        public string RestFuturesClientAddress { get; }

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
            string restSpotAddress,
            string restFuturesAddress,
            string streamSpotAddress,
            string streamPerpetualFuturesAddress) :
            base(name)
        {
            RestSpotClientAddress = restSpotAddress;
            RestFuturesClientAddress = restFuturesAddress;
            SocketClientSpotAddress = streamSpotAddress;
            SocketClientPerpetualFuturesAddress = streamPerpetualFuturesAddress;
        }

        /// <summary>
        /// Live environment, using Futures V2 API
        /// </summary>
        public static BitMartEnvironment Live { get; }
            = new BitMartEnvironment(TradeEnvironmentNames.Live,
                                     BitMartApiAddresses.Default.RestSpotClientAddress,
                                     BitMartApiAddresses.Default.RestFuturesClientAddress,
                                     BitMartApiAddresses.Default.SocketSpotClientAddress,
                                     BitMartApiAddresses.Default.SocketPerpetualFuturesClientAddress);

        /// <summary>
        /// Live environment, using Futures V1 API
        /// </summary>
        public static BitMartEnvironment LiveFuturesV1 { get; }
            = new BitMartEnvironment(TradeEnvironmentNames.Live,
                                     BitMartApiAddresses.Default.RestSpotClientAddress,
                                     BitMartApiAddresses.FuturesV1.RestFuturesClientAddress,
                                     BitMartApiAddresses.Default.SocketSpotClientAddress,
                                     BitMartApiAddresses.FuturesV1.SocketPerpetualFuturesClientAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="restSpotAddress"></param>
        /// <param name="restFuturesAddress"></param>
        /// <param name="spotSocketStreamsAddress"></param>
        /// <param name="perpetualFuturesSocketStreamAddress"></param>
        /// <returns></returns>
        public static BitMartEnvironment CreateCustom(
                        string name,
                        string restSpotAddress,
                        string restFuturesAddress,
                        string spotSocketStreamsAddress,
                        string perpetualFuturesSocketStreamAddress)
            => new BitMartEnvironment(name, restSpotAddress, restFuturesAddress, spotSocketStreamsAddress, perpetualFuturesSocketStreamAddress);
    }
}
