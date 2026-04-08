using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Book ticker update
    /// </summary>
    public record BitMartBookTickerUpdate
    {
        /// <summary>
        /// ["<c>bid_px</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bid_px")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bid_sz</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("bid_sz")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>ask_px</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("ask_px")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>ask_sz</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("ask_sz")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>ms_t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ms_t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }
}
