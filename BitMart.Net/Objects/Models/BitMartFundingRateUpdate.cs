using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Funding rate info
    /// </summary>
    public record BitMartFundingRateUpdate
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Current funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }

        /// <summary>
        /// Expected funding rate of next period
        /// </summary>
        [JsonPropertyName("nextFundingRate")]
        public decimal ExpectedFundingRate { get; set; }
        /// <summary>
        /// Timestamp of funding rate calculation
        /// </summary>
        [JsonPropertyName("fundingTime")]
        public DateTime? FundingTime { get; set; }
        /// <summary>
        /// Forecasted funding time for the next period
        /// </summary>
        [JsonPropertyName("nextFundingTime")]
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
