using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Ticker/price statistics
    /// </summary>
    [SerializationModel]
    public record BitMartTicker
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Last price
        /// </summary>
        [JsonPropertyName("last")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Volume over last 24h in base asset
        /// </summary>
        [JsonPropertyName("v_24h")]
        public decimal Volume24h { get; set; }
        /// <summary>
        /// Volume over last 24h in quote asset
        /// </summary>
        [JsonPropertyName("qv_24h")]
        public decimal QuoteVolume24h { get; set; }
        /// <summary>
        /// Open price 24h ago
        /// </summary>
        [JsonPropertyName("open_24h")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// High price in last 24h
        /// </summary>
        [JsonPropertyName("high_24h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Low price in last 24h
        /// </summary>
        [JsonPropertyName("low_24h")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Price change factor
        /// </summary>
        [JsonPropertyName("fluctuation")]
        public decimal Change { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("bid_px")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Best bid quantity
        /// </summary>
        [JsonPropertyName("bid_sz")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("ask_px")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Best ask quantity
        /// </summary>
        [JsonPropertyName("ask_sz")]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime? Timestamp { get; set; }
    }


}
