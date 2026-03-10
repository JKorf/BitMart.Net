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
        /// ["<c>trading</c>"] Trading
        /// </summary>
        [Map("trading")]
        Trading,
        /// <summary>
        /// ["<c>pre-trade</c>"] Pre-trade
        /// </summary>
        [Map("pre-trade")]
        PreTrade
    }
}
