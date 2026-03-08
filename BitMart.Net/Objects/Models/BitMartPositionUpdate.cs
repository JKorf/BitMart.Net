using CryptoExchange.Net.Converters.SystemTextJson;
using BitMart.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Position update
    /// </summary>
    [SerializationModel]
    public record BitMartPositionUpdate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>hold_volume</c>"] Position size
        /// </summary>
        [JsonPropertyName("hold_volume")]
        public decimal PositionSize { get; set; }
        /// <summary>
        /// ["<c>position_type</c>"] Position side
        /// </summary>
        [JsonPropertyName("position_type")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>open_type</c>"] Margin type
        /// </summary>
        [JsonPropertyName("open_type")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// ["<c>frozen_volume</c>"] Quantity frozen
        /// </summary>
        [JsonPropertyName("frozen_volume")]
        public decimal QuantityFrozen { get; set; }
        /// <summary>
        /// ["<c>close_volume</c>"] Quantity close
        /// </summary>
        [JsonPropertyName("close_volume")]
        public decimal QuantityClose { get; set; }
        /// <summary>
        /// ["<c>hold_avg_price</c>"] Average position price
        /// </summary>
        [JsonPropertyName("hold_avg_price")]
        public decimal? AverageHoldPrice { get; set; }
        /// <summary>
        /// ["<c>close_avg_price</c>"] Average close price
        /// </summary>
        [JsonPropertyName("close_avg_price")]
        public decimal? AverageClosePrice { get; set; }
        /// <summary>
        /// ["<c>open_avg_price</c>"] Average open price
        /// </summary>
        [JsonPropertyName("open_avg_price")]
        public decimal? AverageOpenPrice { get; set; }
        /// <summary>
        /// ["<c>liquidate_price</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("liquidate_price")]
        public decimal? LiquidationPrice { get; set; }
        /// <summary>
        /// ["<c>create_time</c>"] Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>update_time</c>"] Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>position_mode</c>"] Position mode
        /// </summary>
        [JsonPropertyName("position_mode")]
        public PositionMode? PositionMode { get; set; }
    }
}
