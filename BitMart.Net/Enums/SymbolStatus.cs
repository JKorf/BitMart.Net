using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Symbol status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolStatus>))]
    public enum SymbolStatus
    {
        /// <summary>
        /// Trading
        /// </summary>
        [Map("trading")]
        Trading,
        /// <summary>
        /// Pre-trade
        /// </summary>
        [Map("pre-trade")]
        PreTrade
    }
}
