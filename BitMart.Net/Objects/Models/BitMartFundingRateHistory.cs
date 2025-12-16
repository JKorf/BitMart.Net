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
