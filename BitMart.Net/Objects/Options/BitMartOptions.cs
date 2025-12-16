using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;

namespace BitMart.Net.Objects.Options
{
    /// <summary>
    /// BitMart options
    /// </summary>
    public class BitMartOptions : LibraryOptions<BitMartRestOptions, BitMartSocketOptions, ApiCredentials, BitMartEnvironment>
    {
    }
}
