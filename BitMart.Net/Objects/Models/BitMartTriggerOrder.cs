using CryptoExchange.Net.Converters.SystemTextJson;
using BitMart.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Trigger order info
    /// </summary>
    [SerializationModel]
    public record BitMartTriggerOrder
    {
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>client_order_id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>executive_price</c>"] Order price
        /// </summary>
        [JsonPropertyName("executive_price")]
        public decimal? OrderPrice { get; set; }
        /// <summary>
        /// ["<c>trigger_price</c>"] Trigger price
        /// </summary>
        [JsonPropertyName("trigger_price")]
        public decimal TriggerPrice { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>state</c>"] Order status
        /// </summary>
        [JsonPropertyName("state")]
        public FuturesOrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public FuturesSide? Side { get; set; }
        /// <summary>
        /// ["<c>mode</c>"] Mode
        /// </summary>
        [JsonPropertyName("mode")]
        public OrderMode Mode { get; set; }
        /// <summary>
        /// ["<c>price_way</c>"] Price way
        /// </summary>
        [JsonPropertyName("price_way")]
        public int? PriceWay { get; set; }
        /// <summary>
        /// ["<c>price_type</c>"] Price type
        /// </summary>
        [JsonPropertyName("price_type")]
        public TriggerPriceType? PriceType { get; set; }
        /// <summary>
        /// ["<c>plan_category</c>"] Plan category
        /// </summary>
        [JsonPropertyName("plan_category")]
        public PlanCategory PlanCategory { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Trigger order type
        /// </summary>
        [JsonPropertyName("type")]
        public TriggerOrderType Type { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>open_type</c>"] Open type
        /// </summary>
        [JsonPropertyName("open_type")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// ["<c>create_time</c>"] Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// ["<c>update_time</c>"] Update time
        /// </summary>
        [JsonPropertyName("update_time")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>preset_take_profit_price_type</c>"] Pre-set TakeProfit trigger price type
        /// </summary>
        [JsonPropertyName("preset_take_profit_price_type")]
        public TriggerPriceType? PresetTakeProfitPriceType { get; set; }
        /// <summary>
        /// ["<c>preset_stop_loss_price_type</c>"] Pre-set StopLoss trigger price type
        /// </summary>
        [JsonPropertyName("preset_stop_loss_price_type")]
        public TriggerPriceType? PresetStopLossPriceType { get; set; }
        /// <summary>
        /// ["<c>preset_take_profit_price</c>"] Pre-set TakeProfit price
        /// </summary>
        [JsonPropertyName("preset_take_profit_price")]
        public decimal? PresetTakeProfitPrice { get; set; }
        /// <summary>
        /// ["<c>preset_stop_loss_price</c>"] Pre-set StopLoss price
        /// </summary>
        [JsonPropertyName("preset_stop_loss_price")]
        public decimal? PresetStopLossPrice { get; set; }
        /// <summary>
        /// ["<c>position_mode</c>"] Position mode
        /// </summary>
        [JsonPropertyName("position_mode")]
        public PositionMode? PositionMode { get; set; }
    }


}
