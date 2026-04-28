using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Open interest
    /// </summary>
    [SerializationModel]
    public record BitMartOpenInterest
    {
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>open_interest</c>"] Open interest
        /// </summary>
        [JsonPropertyName("open_interest")]
        public decimal OpenInterest { get; set; }
        /// <summary>
        /// ["<c>open_interest_value</c>"] Open interest value
        /// </summary>
        [JsonPropertyName("open_interest_value")]
        public decimal OpenInterestValue { get; set; }
        /// <summary>
        /// ["<c>open_interest</c>"] Open interest 24h
        /// </summary>
        [JsonPropertyName("open_interest_24h")]
        public decimal OpenInterest24H { get; set; }
        /// <summary>
        /// ["<c>open_interest_value</c>"] Open interest value 24h
        /// </summary>
        [JsonPropertyName("open_interest_value_24h")]
        public decimal OpenInterestValue24H { get; set; }
    }


}
