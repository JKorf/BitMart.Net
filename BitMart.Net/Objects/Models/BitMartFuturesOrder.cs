using CryptoExchange.Net.Converters.SystemTextJson;
using BitMart.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesOrder
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
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
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
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public FuturesSide Side { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("type")]
        public FuturesOrderType OrderType { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Open type
        /// </summary>
        [JsonPropertyName("open_type")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// Average price
        /// </summary>
        [JsonPropertyName("deal_avg_price")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonPropertyName("deal_size")]
        public decimal QuantityFilled { get; set; }
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

        /// <summary>
        /// Trailing order trigger price
        /// </summary>
        [JsonPropertyName("activation_price")]
        public decimal? TriggerPrice { get; set; }
        /// <summary>
        /// Trailing order callback rate
        /// </summary>
        [JsonPropertyName("callback_rate")]
        public decimal? CallbackRate { get; set; }
        /// <summary>
        /// Trailing order trigger price type
        /// </summary>
        [JsonPropertyName("activation_price_type")]
        public TriggerPriceType? TriggerPriceType { get; set; }
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
        /// <summary>
        /// Order id of the executed trigger order
        /// </summary>
        [JsonPropertyName("executive_order_id")]
        public string? TriggerOrderId { get; set; }
    }


}
