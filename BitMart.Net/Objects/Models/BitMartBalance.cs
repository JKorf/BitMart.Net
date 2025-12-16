using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartBalanceWrapper
    {
        /// <summary>
        /// Wallet
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
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Unavailable
        /// </summary>
        [JsonPropertyName("unAvailable")]
        public decimal Unavailable { get; set; }
        /// <summary>
        /// USD valuation
        /// </summary>
        [JsonPropertyName("available_usd_valuation")]
        public decimal? AvailableUsdValuation { get; set; }
        /// <summary>
        /// Frozen
        /// </summary>
        [JsonPropertyName("frozen")]
        public decimal Frozen { get; set; }
    }


}
