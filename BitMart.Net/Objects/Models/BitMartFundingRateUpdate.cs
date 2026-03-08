using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Funding rate info
    /// </summary>
    [SerializationModel]
    public record BitMartFundingRateUpdate
    {
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fundingRate</c>"] Current funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }

        /// <summary>
        /// ["<c>nextFundingRate</c>"] Expected funding rate of next period
        /// </summary>
        [JsonPropertyName("nextFundingRate")]
        public decimal ExpectedFundingRate { get; set; }
        /// <summary>
        /// ["<c>fundingTime</c>"] Timestamp of funding rate calculation
        /// </summary>
        [JsonPropertyName("fundingTime")]
        public DateTime? FundingTime { get; set; }
        /// <summary>
        /// ["<c>nextFundingTime</c>"] Forecasted funding time for the next period
        /// </summary>
        [JsonPropertyName("nextFundingTime")]
        public DateTime? NextFundingTime { get; set; }
        /// <summary>
        /// ["<c>funding_upper_limit</c>"] Upper limit of funding rate for this symbol
        /// </summary>
        [JsonPropertyName("funding_upper_limit")]
        public decimal FundingUpperLimit { get; set; }
        /// <summary>
        /// ["<c>funding_lower_limit</c>"] Lower limit of funding rate for this symbol
        /// </summary>
        [JsonPropertyName("funding_lower_limit")]
        public decimal FundingLowerLimit { get; set; }
    }


}
