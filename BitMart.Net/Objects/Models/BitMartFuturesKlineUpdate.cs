using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Kline data
    /// </summary>
    public record BitMartFuturesKlineUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Kline items
        /// </summary>
        [JsonPropertyName("items")]
        public IEnumerable<BitMartFuturesKlineItem> Klines { get; set; } = Array.Empty<BitMartFuturesKlineItem>();
    }

    /// <summary>
    /// Kline data
    /// </summary>
    public record BitMartFuturesKlineItem
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime? Timestamp { get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// Close price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// High price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Volume
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Volume { get; set; }
    }
}
