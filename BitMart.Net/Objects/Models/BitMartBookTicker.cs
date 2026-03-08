using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Book ticker
    /// </summary>
    [SerializationModel]
    public record BitMartBookTicker
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>best_bid_price</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("best_bid_price")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>best_bid_vol</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("best_bid_vol")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>best_ask_price</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("best_ask_price")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>best_ask_vol</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("best_ask_vol")]
        public decimal BestAskQuantity { get; set; }

        /// <summary>
        /// ["<c>ms_t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ms_t")]
        public DateTime Timestamp { get; set; }
    }
}
