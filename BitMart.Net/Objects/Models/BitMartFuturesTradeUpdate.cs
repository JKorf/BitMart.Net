using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Trade update
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesTradeUpdate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>created_at</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>deal_price</c>"] Price
        /// </summary>
        [JsonPropertyName("deal_price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>deal_vol</c>"] Quantity
        /// </summary>
        [JsonPropertyName("deal_vol")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>trade_id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public long TradeId { get; set; }
        /// <summary>
        /// ["<c>m</c>"] Whether the buyer was the maker
        /// </summary>
        [JsonPropertyName("m")]
        public bool BuyerIsMaker { get; set; }
    }
}
