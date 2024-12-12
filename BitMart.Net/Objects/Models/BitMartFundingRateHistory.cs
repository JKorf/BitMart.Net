using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    internal record BitMartFundingRateHistoryWrapper
    {
        [JsonPropertyName("list")]
        public IEnumerable<BitMartFundingRateHistory> History { get; set; } = [];
    }

    /// <summary>
    /// Funding rate history
    /// </summary>
    public record BitMartFundingRateHistory
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Funding time
        /// </summary>
        [JsonPropertyName("funding_time")]
        public DateTime FundingTime { get; set; }
    }


}
