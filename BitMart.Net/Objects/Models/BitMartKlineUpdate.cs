using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Kline update
    /// </summary>
    [SerializationModel]
    public record BitMartKlineUpdate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>candle</c>"] Kline/candle data
        /// </summary>
        [JsonPropertyName("candle")]
        public BitMartKline Kline { get; set; } = null!;
    }
}
