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
        /// ["<c>0</c>"] Standard address
        /// </summary>
        [Map("0")]
        Standard,
        /// <summary>
        /// ["<c>1</c>"] Universal address
        /// </summary>
        [Map("1")]
        Universal,
        /// <summary>
        /// ["<c>2</c>"] EVM address
        /// </summary>
        [Map("2")]
        Evm
    }
}
