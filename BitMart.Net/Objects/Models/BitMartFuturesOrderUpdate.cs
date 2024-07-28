﻿using BitMart.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Order update event
    /// </summary>
    public record BitMartFuturesOrderUpdateEvent
    {
        /// <summary>
        /// Update trigger
        /// </summary>
        [JsonPropertyName("action")]
        public OrderEvent Event { get; set; }
        /// <summary>
        /// Order info
        /// </summary>
        [JsonPropertyName("order")]
        public BitMartFuturesOrderUpdate Order { get; set; } = null!;
    }

    /// <summary>
    /// Order update
    /// </summary>
    public record BitMartFuturesOrderUpdate
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
        /// Order id of the executed trigger order
        /// </summary>
        [JsonPropertyName("plan_order_id")]
        public long? TriggerOrderId { get; set; }
        /// <summary>
        /// Last trade info
        /// </summary>
        [JsonPropertyName("last_trade")]
        public BitMartFuturesOrderTrade? LastTrade { get; set; }
    }

    /// <summary>
    /// Order trade info
    /// </summary>
    public record BitMartFuturesOrderTrade
    {
        /// <summary>
        /// Id of the last trade
        /// </summary>
        [JsonPropertyName("lastTradeID")]
        public long TradeId { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("fillQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("fillPrice")]
        public decimal Price { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("feeCcy")]
        public string FeeAsset { get; set; } = string.Empty;
    }
}
