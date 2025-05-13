using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Withdrawal address type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawalAddressType>))]
    public enum WithdrawalAddressType
    {
        /// <summary>
        /// Standard address
        /// </summary>
        [Map("0")]
        Standard,
        /// <summary>
        /// Universal address
        /// </summary>
        [Map("1")]
        Universal,
        /// <summary>
        /// EVM address
        /// </summary>
        [Map("2")]
        Evm
    }
}
