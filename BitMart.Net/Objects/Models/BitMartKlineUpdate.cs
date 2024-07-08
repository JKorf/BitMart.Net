using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Kline update
    /// </summary>
    public record BitMartKlineUpdate
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Kline/candle data
        /// </summary>
        [JsonPropertyName("candle")]
        public BitMartKline Kline { get; set; } = null!;
    }
}
