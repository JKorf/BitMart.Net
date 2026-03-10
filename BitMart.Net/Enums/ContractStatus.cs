using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Contract status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractStatus>))]
    public enum ContractStatus
    {
        /// <summary>
        /// ["<c>Trading</c>"] Trading
        /// </summary>
        [Map("Trading")]
        Trading,
        /// <summary>
        /// ["<c>Delisted</c>"] Delisted
        /// </summary>
        [Map("Delisted")]
        Delisted
    }
}
