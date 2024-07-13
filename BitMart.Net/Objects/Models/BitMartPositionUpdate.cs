using BitMart.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Position update
    /// </summary>
    public record BitMartPositionUpdate
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Position size
        /// </summary>
        [JsonPropertyName("hold_volume")]
        public decimal PositionSize { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("position_type")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Margin type
        /// </summary>
        [JsonPropertyName("open_type")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// Quantity frozen
        /// </summary>
        [JsonPropertyName("frozen_volume")]
        public decimal QuantityFrozen { get; set; }
        /// <summary>
        /// Quantity close
        /// </summary>
        [JsonPropertyName("close_volume")]
        public decimal QuantityClose { get; set; }
        /// <summary>
        /// Average position price
        /// </summary>
        [JsonPropertyName("hold_avg_price")]
        public decimal? AverageHoldPrice { get; set; }
        /// <summary>
        /// Average close price
        /// </summary>
        [JsonPropertyName("close_avg_price")]
        public decimal? AverageClosePrice { get; set; }
        /// <summary>
        /// Average open price
        /// </summary>
        [JsonPropertyName("open_avg_price")]
        public decimal? AverageOpenPrice { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("liquidate_price")]
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
    }
}
