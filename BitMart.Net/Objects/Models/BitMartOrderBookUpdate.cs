using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Order book
    /// </summary>
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
        public IEnumerable<BitMartOrderBookEntry> Asks { get; set; } = Array.Empty<BitMartOrderBookEntry>();
        /// <summary>
        /// Bids
        /// </summary>
        [JsonPropertyName("bids")]
        public IEnumerable<BitMartOrderBookEntry> Bids { get; set; } = Array.Empty<BitMartOrderBookEntry>();
    }

    /// <summary>
    /// Incremental order book update
    /// </summary>
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
