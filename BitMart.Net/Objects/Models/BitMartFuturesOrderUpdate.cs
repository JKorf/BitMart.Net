using CryptoExchange.Net.Converters.SystemTextJson;
using BitMart.Net.Enums;
using System;
using System.Text.Json.Serialization;
using BitMart.Net.Converters;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Order update event
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesOrderUpdateEvent
    {
        /// <summary>
        /// ["<c>action</c>"] Update trigger
        /// </summary>
        [JsonPropertyName("action")]
        public OrderEvent Event { get; set; }
        /// <summary>
        /// ["<c>order</c>"] Order info
        /// </summary>
        [JsonPropertyName("order")]
        public BitMartFuturesOrderUpdate Order { get; set; } = null!;
    }

    /// <summary>
    /// Order update
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesOrderUpdate
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
        public MarginType? MarginType { get; set; }
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
        /// ["<c>plan_order_id</c>"] Order id of the executed trigger order
        /// </summary>
        [JsonPropertyName("plan_order_id")]
        public long? TriggerOrderId { get; set; }
        /// <summary>
        /// ["<c>last_trade</c>"] Last trade info
        /// </summary>
        [JsonPropertyName("last_trade")]
        public BitMartFuturesOrderTrade? LastTrade { get; set; }
        /// <summary>
        /// ["<c>trigger_price</c>"] Trigger price
        /// </summary>
        [JsonPropertyName("trigger_price")]
        public decimal? TriggerPrice { get; set; }
        /// <summary>
        /// ["<c>trigger_price_type</c>"] Trigger price type
        /// </summary>
        [JsonPropertyName("trigger_price_type")]
        public TriggerPriceType? TriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>execution_price</c>"] Execution price of trigger order
        /// </summary>
        [JsonPropertyName("execution_price")]
        [JsonConverter(typeof(MarketPriceConverter))]
        public decimal? ExecutionPrice { get; set; }
        /// <summary>
        /// ["<c>activation_price</c>"] Activation price
        /// </summary>
        [JsonPropertyName("activation_price")]
        public decimal? ActivationPrice { get; set; }
        /// <summary>
        /// ["<c>activation_price_type</c>"] Activation price type
        /// </summary>
        [JsonPropertyName("activation_price_type")]
        public TriggerPriceType? ActivationPriceType { get; set; }
        /// <summary>
        /// ["<c>callback_rate</c>"] Callback rate
        /// </summary>
        [JsonPropertyName("callback_rate")]
        public decimal? CallbackRate { get; set; }
        /// <summary>
        /// ["<c>position_mode</c>"] Position mode
        /// </summary>
        [JsonPropertyName("position_mode")]
        public PositionMode? PositionMode { get; set; }
    }

    /// <summary>
    /// Order trade info
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesOrderTrade
    {
        /// <summary>
        /// ["<c>lastTradeID</c>"] Id of the last trade
        /// </summary>
        [JsonPropertyName("lastTradeID")]
        public long TradeId { get; set; }
        /// <summary>
        /// ["<c>fillQty</c>"] Quantity
        /// </summary>
        [JsonPropertyName("fillQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>fillPrice</c>"] Price
        /// </summary>
        [JsonPropertyName("fillPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>feeCcy</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("feeCcy")]
        public string FeeAsset { get; set; } = string.Empty;
    }
}
