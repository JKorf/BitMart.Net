using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

namespace BitMart.Net.Objects.Options
{
    /// <summary>
    /// BitMart options
    /// </summary>
    public class BitMartOptions : LibraryOptions<BitMartRestOptions, BitMartSocketOptions, BitMartCredentials, BitMartEnvironment>
    {
        /// <summary>
        /// Options for Shared API usage
        /// </summary>
        public SharedApiOptions SharedApi { get; set; } = new();
    }
}
