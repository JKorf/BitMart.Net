using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Order state filter
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderQueryState>))]
    public enum OrderQueryState
    {
        /// <summary>
        /// ["<c>open</c>"] Active order
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// ["<c>history</c>"] Closed order
        /// </summary>
        [Map("history")]
        History
    }
}
