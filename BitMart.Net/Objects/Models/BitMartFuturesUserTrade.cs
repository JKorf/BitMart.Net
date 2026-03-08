using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using BitMart.Net.Enums;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// User trade info
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesUserTrade
    {
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>trade_id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public FuturesSide Side { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>vol</c>"] Quantity
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>exec_type</c>"] Role
        /// </summary>
        [JsonPropertyName("exec_type")]
        public TradeRole? Role { get; set; }
        /// <summary>
        /// ["<c>profit</c>"] Profit
        /// </summary>
        [JsonPropertyName("profit")]
        public bool Profit { get; set; }
        /// <summary>
        /// ["<c>realised_profit</c>"] Realised profit and loss
        /// </summary>
        [JsonPropertyName("realised_profit")]
        public decimal RealisedPnl { get; set; }
        /// <summary>
        /// ["<c>paid_fees</c>"] Fee paid
        /// </summary>
        [JsonPropertyName("paid_fees")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>create_time</c>"] Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
    }


}
