using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Open interest
    /// </summary>
    public record BitMartOpenInterest
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Open interest
        /// </summary>
        [JsonPropertyName("open_interest")]
        public decimal OpenInterest { get; set; }
        /// <summary>
        /// Open interest value
        /// </summary>
        [JsonPropertyName("open_interest_value")]
        public decimal OpenInterestValue { get; set; }
    }


}
