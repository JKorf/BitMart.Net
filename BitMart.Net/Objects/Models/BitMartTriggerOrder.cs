using BitMart.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Trigger order info
    /// </summary>
    public record BitMartTriggerOrder
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("executive_price")]
        public decimal? OrderPrice { get; set; }
        /// <summary>
        /// Trigger price
        /// </summary>
        [JsonPropertyName("trigger_price")]
        public decimal TriggerPrice { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("state")]
        public FuturesOrderStatus Status { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("side")]
        public FuturesSide? Side { get; set; }
        /// <summary>
        /// Mode
        /// </summary>
        [JsonPropertyName("mode")]
        public OrderMode Mode { get; set; }
        /// <summary>
        /// Price way
        /// </summary>
        [JsonPropertyName("price_way")]
        public int? PriceWay { get; set; }
        /// <summary>
        /// Price type
        /// </summary>
        [JsonPropertyName("price_type")]
        public TriggerPriceType? PriceType { get; set; }
        /// <summary>
        /// Plan category
        /// </summary>
        [JsonPropertyName("plan_category")]
        public PlanCategory PlanCategory { get; set; }
        /// <summary>
        /// Trigger order type
        /// </summary>
        [JsonPropertyName("type")]
        public TriggerOrderType Type { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Open type
        /// </summary>
        [JsonPropertyName("open_type")]
        public MarginType OpenType { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Pre-set TakeProfit trigger price type
        /// </summary>
        [JsonPropertyName("preset_take_profit_price_type")]
        public TriggerPriceType? PresetTakeProfitPriceType { get; set; }
        /// <summary>
        /// Pre-set StopLoss trigger price type
        /// </summary>
        [JsonPropertyName("preset_stop_loss_price_type")]
        public TriggerPriceType? PresetStopLossPriceType { get; set; }
        /// <summary>
        /// Pre-set TakeProfit price
        /// </summary>
        [JsonPropertyName("preset_take_profit_price")]
        public decimal? PresetTakeProfitPrice { get; set; }
        /// <summary>
        /// Pre-set StopLoss price
        /// </summary>
        [JsonPropertyName("preset_stop_loss_price")]
        public decimal? PresetStopLossPrice { get; set; }
    }


}
