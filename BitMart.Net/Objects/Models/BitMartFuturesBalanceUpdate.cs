using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Blanace update
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesBalanceUpdate
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>available_balance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("available_balance")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>frozen_balance</c>"] Frozen balance
        /// </summary>
        [JsonPropertyName("frozen_balance")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// ["<c>position_deposit</c>"] Position Margin
        /// </summary>
        [JsonPropertyName("position_deposit")]
        public decimal PositionMargin { get; set; }
    }
}
