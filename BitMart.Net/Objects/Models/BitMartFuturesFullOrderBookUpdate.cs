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
    public record BitMartFuturesFullOrderBookUpdate
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
        /// Update type, only applicable to incremental updates
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        /// <summary>
        /// Version, only applicable to incremental updates
        /// </summary>
        [JsonPropertyName("version")]
        public long? Version { get; set; }

        /// <summary>
        /// Asks
        /// </summary>
        [JsonPropertyName("asks")]
        public IEnumerable<BitMartFuturesOrderBookEntry> Asks { get; set; } = Array.Empty<BitMartFuturesOrderBookEntry>();
        /// <summary>
        /// Bids
        /// </summary>
        [JsonPropertyName("bids")]
        public IEnumerable<BitMartFuturesOrderBookEntry> Bids { get; set; } = Array.Empty<BitMartFuturesOrderBookEntry>();
    }
}
