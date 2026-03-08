using CryptoExchange.Net.Converters.SystemTextJson;
using BitMart.Net.Enums;
using System;
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
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
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
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public FuturesSide Side { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type")]
        public FuturesOrderType OrderType { get; set; }
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
        /// ["<c>deal_avg_price</c>"] Average price
        /// </summary>
        [JsonPropertyName("deal_avg_price")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// ["<c>deal_size</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("deal_size")]
        public decimal QuantityFilled { get; set; }
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
        /// ["<c>activation_price</c>"] Trailing order trigger price
        /// </summary>
        [JsonPropertyName("activation_price")]
        public decimal? TriggerPrice { get; set; }
        /// <summary>
        /// ["<c>callback_rate</c>"] Trailing order callback rate
        /// </summary>
        [JsonPropertyName("callback_rate")]
        public decimal? CallbackRate { get; set; }
        /// <summary>
        /// ["<c>activation_price_type</c>"] Trailing order trigger price type
        /// </summary>
        [JsonPropertyName("activation_price_type")]
        public TriggerPriceType? TriggerPriceType { get; set; }
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
        /// ["<c>executive_order_id</c>"] Order id of the executed trigger order
        /// </summary>
        [JsonPropertyName("executive_order_id")]
        public string? TriggerOrderId { get; set; }
        /// <summary>
        /// ["<c>position_mode</c>"] Position mode
        /// </summary>
        [JsonPropertyName("position_mode")]
        public PositionMode? PositionMode { get; set; }
    }


}
