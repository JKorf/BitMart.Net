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
        /// Socket API address
        /// </summary>
        public string SocketClientAddress { get; }

        internal BitMartEnvironment(
            string name,
            string restAddress,
            string streamAddress) :
            base(name)
        {
            RestClientAddress = restAddress;
            SocketClientAddress = streamAddress;
        }

        /// <summary>
        /// Live environment
        /// </summary>
        public static BitMartEnvironment Live { get; }
            = new BitMartEnvironment(TradeEnvironmentNames.Live,
                                     BitMartApiAddresses.Default.RestClientAddress,
                                     BitMartApiAddresses.Default.SocketClientAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="spotRestAddress"></param>
        /// <param name="spotSocketStreamsAddress"></param>
        /// <returns></returns>
        public static BitMartEnvironment CreateCustom(
                        string name,
                        string spotRestAddress,
                        string spotSocketStreamsAddress)
            => new BitMartEnvironment(name, spotRestAddress, spotSocketStreamsAddress);
    }
}
