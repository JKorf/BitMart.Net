using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Funding rate info
    /// </summary>
    public record BitMartFundingRate
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Funding rate of previous period
        /// </summary>
        [JsonPropertyName("rate_value")]
        public decimal PreviousFundingRate { get; set; }
        /// <summary>
        /// Expected funding rate of next period
        /// </summary>
        [JsonPropertyName("expected_rate")]
        public decimal ExpectedFundingRate { get; set; }
        /// <summary>
        /// Next funding settlement time
        /// </summary>
        [JsonPropertyName("funding_time")]
        public DateTime? NextFundingTime { get; set; }
        /// <summary>
        /// Upper limit of funding rate for this symbol
        /// </summary>
        [JsonPropertyName("funding_upper_limit")]
        public decimal FundingUpperLimit { get; set; }
        /// <summary>
        /// Lower limit of funding rate for this symbol
        /// </summary>
        [JsonPropertyName("funding_lower_limit")]
        public decimal FundingLowerLimit { get; set; }
    }


}
