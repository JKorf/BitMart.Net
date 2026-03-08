using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartBalanceWrapper
    {
        /// <summary>
        /// ["<c>wallet</c>"] Wallet
        /// </summary>
        [JsonPropertyName("wallet")]
        public BitMartBalance[] Wallet { get; set; } = Array.Empty<BitMartBalance>();
    }

    /// <summary>
    /// Balance info
    /// </summary>
    [SerializationModel]
    public record BitMartBalance
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>available</c>"] Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>unAvailable</c>"] Unavailable
        /// </summary>
        [JsonPropertyName("unAvailable")]
        public decimal Unavailable { get; set; }
        /// <summary>
        /// ["<c>available_usd_valuation</c>"] USD valuation
        /// </summary>
        [JsonPropertyName("available_usd_valuation")]
        public decimal? AvailableUsdValuation { get; set; }
        /// <summary>
        /// ["<c>frozen</c>"] Frozen
        /// </summary>
        [JsonPropertyName("frozen")]
        public decimal Frozen { get; set; }
    }


}
