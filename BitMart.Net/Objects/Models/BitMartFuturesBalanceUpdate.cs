using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
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
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("available_balance")]
        public decimal Available { get; set; }
        /// <summary>
        /// Frozen balance
        /// </summary>
        [JsonPropertyName("frozen_balance")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// Position Margin
        /// </summary>
        [JsonPropertyName("position_deposit")]
        public decimal PositionMargin { get; set; }
    }
}
