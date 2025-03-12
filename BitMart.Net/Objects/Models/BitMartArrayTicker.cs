using BitMart.Net.Converters;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Ticker/price statistics
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<BitMartArrayTicker, BitMartSourceGenerationContext>))]
    [SerializationModel]
    public record BitMartArrayTicker
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [ArrayProperty(0)]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Last price
        /// </summary>
        [ArrayProperty(1)]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Volume over last 24h in base asset
        /// </summary>
        [ArrayProperty(2)]
        public decimal Volume24h { get; set; }
        /// <summary>
        /// Volume over last 24h in quote asset
        /// </summary>
        [ArrayProperty(3)]
        public decimal QuoteVolume24h { get; set; }
        /// <summary>
        /// Open price 24h ago
        /// </summary>
        [ArrayProperty(4)]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// High price in last 24h
        /// </summary>
        [ArrayProperty(5)]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Low price in last 24h
        /// </summary>
        [ArrayProperty(6)]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Price change factor
        /// </summary>
        [ArrayProperty(7)]
        public decimal Change { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [ArrayProperty(8)]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Best bid quantity
        /// </summary>
        [ArrayProperty(9)]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [ArrayProperty(10)]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Best ask quantity
        /// </summary>
        [ArrayProperty(11)]
        public decimal BestAskQuantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [ArrayProperty(12), JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Timestamp { get; set; }
    }


}
