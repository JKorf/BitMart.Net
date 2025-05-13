using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Contract type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractType>))]
    public enum ContractType
    {
        /// <summary>
        /// Perpetual contract
        /// </summary>
        [Map("1")]
        Perpetual,
        /// <summary>
        /// Futures contract
        /// </summary>
        [Map("2")]
        Futures
    }
}
