using CryptoExchange.Net.Converters.SystemTextJson;
using System;
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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>current_fee</c>"] Current fee
        /// </summary>
        [JsonPropertyName("current_fee")]
        public decimal? CurrentFee { get; set; }
        /// <summary>
        /// ["<c>open_timestamp</c>"] Open time
        /// </summary>
        [JsonPropertyName("open_timestamp")]
        public DateTime? OpenTime { get; set; }
        /// <summary>
        /// ["<c>current_value</c>"] Current value
        /// </summary>
        [JsonPropertyName("current_value")]
        public decimal? CurrentValue { get; set; }
        /// <summary>
        /// ["<c>mark_price</c>"] Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal? MarkPrice { get; set; }
        /// <summary>
        /// ["<c>position_value</c>"] Position value
        /// </summary>
        [JsonPropertyName("position_value")]
        public decimal? PositionValue { get; set; }
        /// <summary>
        /// ["<c>position_cross</c>"] Position cross
        /// </summary>
        [JsonPropertyName("position_cross")]
        public decimal? PositionCross { get; set; }
        /// <summary>
        /// ["<c>maintenance_margin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("maintenance_margin")]
        public decimal? MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>close_vol</c>"] Close volume
        /// </summary>
        [JsonPropertyName("close_vol")]
        public decimal? CloseVolume { get; set; }
        /// <summary>
        /// ["<c>close_avg_price</c>"] Close average price
        /// </summary>
        [JsonPropertyName("close_avg_price")]
        public decimal? CloseAveragePrice { get; set; }
        /// <summary>
        /// ["<c>open_avg_price</c>"] Open average price
        /// </summary>
        [JsonPropertyName("open_avg_price")]
        public decimal? OpenAveragePrice { get; set; }
        /// <summary>
        /// ["<c>entry_price</c>"] Entry price
        /// </summary>
        [JsonPropertyName("entry_price")]
        public decimal? EntryPrice { get; set; }
        /// <summary>
        /// ["<c>current_amount</c>"] Current quantity
        /// </summary>
        [JsonPropertyName("current_amount")]
        public decimal? CurrentQuantity { get; set; }
        /// <summary>
        /// ["<c>unrealized_value</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_value")]
        public decimal? UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>realized_value</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("realized_value")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>position_type</c>"] Position side
        /// </summary>
        [JsonPropertyName("position_type")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// ["<c>position_mode</c>"] Position mode
        /// </summary>
        [JsonPropertyName("position_mode")]
        public PositionMode? PositionMode { get; set; }
    }


}
