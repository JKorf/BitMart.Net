using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Kline data
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesKlineUpdate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>items</c>"] Kline items
        /// </summary>
        [JsonPropertyName("items")]
        public BitMartFuturesKlineItem[] Klines { get; set; } = Array.Empty<BitMartFuturesKlineItem>();
    }

    /// <summary>
    /// Kline data
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesKlineItem
    {
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime? Timestamp { get; set; }
        /// <summary>
        /// ["<c>o</c>"] Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Close price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] High price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Volume
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
    }
}
