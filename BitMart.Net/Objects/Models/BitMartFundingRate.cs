using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Funding rate info
    /// </summary>
    [SerializationModel]
    public record BitMartFundingRate
    {
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>rate_value</c>"] Funding rate of previous period
        /// </summary>
        [JsonPropertyName("rate_value")]
        public decimal PreviousFundingRate { get; set; }
        /// <summary>
        /// ["<c>expected_rate</c>"] Expected funding rate of next period
        /// </summary>
        [JsonPropertyName("expected_rate")]
        public decimal ExpectedFundingRate { get; set; }
        /// <summary>
        /// ["<c>funding_time</c>"] Next funding settlement time
        /// </summary>
        [JsonPropertyName("funding_time")]
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
