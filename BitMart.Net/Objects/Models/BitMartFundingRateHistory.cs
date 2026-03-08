using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartFundingRateHistoryWrapper
    {
        [JsonPropertyName("list")]
        public BitMartFundingRateHistory[] History { get; set; } = [];
    }

    /// <summary>
    /// Funding rate history
    /// </summary>
    [SerializationModel]
    public record BitMartFundingRateHistory
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>funding_rate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>funding_time</c>"] Funding time
        /// </summary>
        [JsonPropertyName("funding_time")]
        public DateTime FundingTime { get; set; }
    }


}
