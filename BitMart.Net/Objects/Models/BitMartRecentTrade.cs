using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Trade info
    /// </summary>
    public record BitMartRecentTrade
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>qty</c>"] Quantity
        /// </summary>
        [JsonPropertyName("qty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>quote_qty</c>"] Quote quantity
        /// </summary>
        [JsonPropertyName("quote_qty")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>is_buyer_maker</c>"] Is buyer maker
        /// </summary>
        [JsonPropertyName("is_buyer_maker")]
        public bool IsBuyerMaker { get; set; }
    }


}
