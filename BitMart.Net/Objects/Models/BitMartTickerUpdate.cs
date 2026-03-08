using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Ticker update
    /// </summary>
    [SerializationModel]
    public record BitMartTickerUpdate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>last_price</c>"] Last price
        /// </summary>
        [JsonPropertyName("last_price")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>base_volume_24h</c>"] Volume over last 24h in base asset
        /// </summary>
        [JsonPropertyName("base_volume_24h")]
        public decimal Volume24h { get; set; }
        /// <summary>
        /// ["<c>quote_volume_24h</c>"] Volume over last 24h in quote asset
        /// </summary>
        [JsonPropertyName("quote_volume_24h")]
        public decimal QuoteVolume24h { get; set; }
        /// <summary>
        /// ["<c>open_24h</c>"] Open price 24h ago
        /// </summary>
        [JsonPropertyName("open_24h")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>high_24h</c>"] High price in last 24h
        /// </summary>
        [JsonPropertyName("high_24h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>low_24h</c>"] Low price in last 24h
        /// </summary>
        [JsonPropertyName("low_24h")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>fluctuation</c>"] Price change factor
        /// </summary>
        [JsonPropertyName("fluctuation")]
        public decimal Change { get; set; }
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
        public DateTime? Timestamp { get; set; }
    }
}
