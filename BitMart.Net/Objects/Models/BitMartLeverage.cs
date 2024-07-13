using BitMart.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Leverage info
    /// </summary>
    public record BitMartLeverage
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Open type
        /// </summary>
        [JsonPropertyName("open_type")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// Max value
        /// </summary>
        [JsonPropertyName("max_value")]
        public decimal MaxValue { get; set; }
    }


}
