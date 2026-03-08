using CryptoExchange.Net.Converters.SystemTextJson;
using System;
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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>order_mode</c>"] Spot order mode
        /// </summary>
        [JsonPropertyName("order_mode")]
        public SpotMode SpotMode { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>order_state</c>"] Order status
        /// </summary>
        [JsonPropertyName("order_state")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>filled_size</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("filled_size")]
        public decimal? QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>notional</c>"] Quote quantity
        /// </summary>
        [JsonPropertyName("notional")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>filled_notional</c>"] Quote quantity filled
        /// </summary>
        [JsonPropertyName("filled_notional")]
        public decimal? QuoteQuantityFilled { get; set; }
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
        /// ["<c>entrust_type</c>"] Entrust type
        /// </summary>
        [JsonPropertyName("entrust_type")]
        public EntrustType EntrustType { get; set; }
        /// <summary>
        /// ["<c>dealFee</c>"] Fee
        /// </summary>
        [JsonPropertyName("dealFee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>deal_fee_coin_name</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("deal_fee_coin_name")]
        public string? FeeAsset { get; set; }


        /// <summary>
        /// ["<c>last_fill_price</c>"] Price of the last trade
        /// </summary>
        [JsonPropertyName("last_fill_price")]
        public decimal LastTradePrice { get; set; }
        /// <summary>
        /// ["<c>last_fill_count</c>"] Quantity of the last trade
        /// </summary>
        [JsonPropertyName("last_fill_count")]
        public decimal LastTradeQuantity { get; set; }
        /// <summary>
        /// ["<c>last_fill_time</c>"] Timestamp of the last trade
        /// </summary>
        [JsonPropertyName("last_fill_time")]
        public DateTime? LastTradeTime { get; set; }
        /// <summary>
        /// ["<c>exec_type</c>"] Role of the last trade
        /// </summary>
        [JsonPropertyName("exec_type")]
        public TradeRole? LastTradeRole { get; set; }
        /// <summary>
        /// ["<c>detail_id</c>"] Id of the last trade
        /// </summary>
        [JsonPropertyName("detail_id")]
        public string? LastTradeId { get; set; }
    }
}
