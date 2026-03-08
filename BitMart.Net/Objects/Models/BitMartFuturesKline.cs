using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Kline data
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesKline
    {
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }
        /// <summary>
        /// ["<c>open_price</c>"] Open price
        /// </summary>
        [JsonPropertyName("open_price")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>close_price</c>"] Close price
        /// </summary>
        [JsonPropertyName("close_price")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// ["<c>high_price</c>"] High price
        /// </summary>
        [JsonPropertyName("high_price")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>low_price</c>"] Low price
        /// </summary>
        [JsonPropertyName("low_price")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>volume</c>"] Volume
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
    }


}
