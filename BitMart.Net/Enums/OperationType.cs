using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Operation type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OperationType>))]
    public enum OperationType
    {
        /// <summary>
        /// ["<c>deposit</c>"] Deposit
        /// </summary>
        [Map("deposit")]
        Deposit,
        /// <summary>
        /// ["<c>withdraw</c>"] Withdrawal
        /// </summary>
        [Map("withdraw")]
        Withdrawal
    }
}
