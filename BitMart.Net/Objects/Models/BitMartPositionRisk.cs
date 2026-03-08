using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using BitMart.Net.Enums;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Position risk
    /// </summary>
    [SerializationModel]
    public record BitMartPositionRisk
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>position_amt</c>"] Position quantity
        /// </summary>
        [JsonPropertyName("position_amt")]
        public decimal PositionQuantity { get; set; }
        /// <summary>
        /// ["<c>mark_price</c>"] Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>unrealized_profit</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_profit")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>liquidation_price</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("liquidation_price")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>max_notional_value</c>"] Max notional value
        /// </summary>
        [JsonPropertyName("max_notional_value")]
        public decimal MaxNotionalValue { get; set; }
        /// <summary>
        /// ["<c>margin_type</c>"] Margin type
        /// </summary>
        [JsonPropertyName("margin_type")]
        public MarginType? MarginType { get; set; }
        /// <summary>
        /// ["<c>isolated_margin</c>"] Margin for isolated position
        /// </summary>
        [JsonPropertyName("isolated_margin")]
        public decimal? IsolatedMargin { get; set; }
        /// <summary>
        /// ["<c>position_side</c>"] Position side
        /// </summary>
        [JsonPropertyName("position_side")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// ["<c>notional</c>"] Notional
        /// </summary>
        [JsonPropertyName("notional")]
        public decimal Notional { get; set; }
        /// <summary>
        /// ["<c>update_time</c>"] Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
    }


}
