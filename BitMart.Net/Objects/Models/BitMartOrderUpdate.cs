using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using BitMart.Net.Enums;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Order update
    /// </summary>
    [SerializationModel]
    public record BitMartOrderUpdate
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
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Spot order mode
        /// </summary>
        [JsonPropertyName("order_mode")]
        public SpotMode SpotMode { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("order_state")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonPropertyName("filled_size")]
        public decimal? QuantityFilled { get; set; }
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonPropertyName("notional")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// Quote quantity filled
        /// </summary>
        [JsonPropertyName("filled_notional")]
        public decimal? QuoteQuantityFilled { get; set; }
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
        /// Entrust type
        /// </summary>
        [JsonPropertyName("entrust_type")]
        public EntrustType EntrustType { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("dealFee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("deal_fee_coin_name")]
        public string? FeeAsset { get; set; }


        /// <summary>
        /// Price of the last trade
        /// </summary>
        [JsonPropertyName("last_fill_price")]
        public decimal LastTradePrice { get; set; }
        /// <summary>
        /// Quantity of the last trade
        /// </summary>
        [JsonPropertyName("last_fill_count")]
        public decimal LastTradeQuantity { get; set; }
        /// <summary>
        /// Timestamp of the last trade
        /// </summary>
        [JsonPropertyName("last_fill_time")]
        public DateTime? LastTradeTime { get; set; }
        /// <summary>
        /// Role of the last trade
        /// </summary>
        [JsonPropertyName("exec_type")]
        public TradeRole? LastTradeRole { get; set; }
        /// <summary>
        /// Id of the last trade
        /// </summary>
        [JsonPropertyName("detail_id")]
        public string? LastTradeId { get; set; }
    }
}
