using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using BitMart.Net.Enums;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Position info
    /// </summary>
    [SerializationModel]
    public record BitMartPosition
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Current fee
        /// </summary>
        [JsonPropertyName("current_fee")]
        public decimal? CurrentFee { get; set; }
        /// <summary>
        /// Open time
        /// </summary>
        [JsonPropertyName("open_timestamp")]
        public DateTime? OpenTime { get; set; }
        /// <summary>
        /// Current value
        /// </summary>
        [JsonPropertyName("current_value")]
        public decimal? CurrentValue { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal? MarkPrice { get; set; }
        /// <summary>
        /// Position value
        /// </summary>
        [JsonPropertyName("position_value")]
        public decimal? PositionValue { get; set; }
        /// <summary>
        /// Position cross
        /// </summary>
        [JsonPropertyName("position_cross")]
        public decimal? PositionCross { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("maintenance_margin")]
        public decimal? MaintenanceMargin { get; set; }
        /// <summary>
        /// Close volume
        /// </summary>
        [JsonPropertyName("close_vol")]
        public decimal? CloseVolume { get; set; }
        /// <summary>
        /// Close average price
        /// </summary>
        [JsonPropertyName("close_avg_price")]
        public decimal? CloseAveragePrice { get; set; }
        /// <summary>
        /// Open average price
        /// </summary>
        [JsonPropertyName("open_avg_price")]
        public decimal? OpenAveragePrice { get; set; }
        /// <summary>
        /// Entry price
        /// </summary>
        [JsonPropertyName("entry_price")]
        public decimal? EntryPrice { get; set; }
        /// <summary>
        /// Current quantity
        /// </summary>
        [JsonPropertyName("current_amount")]
        public decimal? CurrentQuantity { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_value")]
        public decimal? UnrealizedPnl { get; set; }
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonPropertyName("realized_value")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("position_type")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// Position mode
        /// </summary>
        [JsonPropertyName("position_mode")]
        public PositionMode? PositionMode { get; set; }
    }


}
