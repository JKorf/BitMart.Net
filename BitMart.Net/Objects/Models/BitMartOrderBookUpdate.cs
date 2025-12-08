using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Order book
    /// </summary>
    [SerializationModel]
    public record BitMartOrderBookUpdate
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ms_t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Asks
        /// </summary>
        [JsonPropertyName("asks")]
        public BitMartOrderBookEntry[] Asks { get; set; } = Array.Empty<BitMartOrderBookEntry>();
        /// <summary>
        /// Bids
        /// </summary>
        [JsonPropertyName("bids")]
        public BitMartOrderBookEntry[] Bids { get; set; } = Array.Empty<BitMartOrderBookEntry>();
    }

    /// <summary>
    /// Incremental order book update
    /// </summary>
    [SerializationModel]
    public record BitMartOrderBookIncrementalUpdate: BitMartOrderBookUpdate
    {
        /// <summary>
        /// Update type
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Data version
        /// </summary>
        [JsonPropertyName("version")]
        public long Version { get; set; }
    }
}
