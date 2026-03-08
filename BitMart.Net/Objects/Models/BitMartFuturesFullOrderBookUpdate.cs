using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Order book
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesFullOrderBookUpdate
    {
        /// <summary>
        /// ["<c>ms_t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ms_t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>type</c>"] Update type, only applicable to incremental updates
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        /// <summary>
        /// ["<c>version</c>"] Version, only applicable to incremental updates
        /// </summary>
        [JsonPropertyName("version")]
        public long? Version { get; set; }

        /// <summary>
        /// ["<c>asks</c>"] Asks
        /// </summary>
        [JsonPropertyName("asks")]
        public BitMartFuturesOrderBookEntry[] Asks { get; set; } = Array.Empty<BitMartFuturesOrderBookEntry>();
        /// <summary>
        /// ["<c>bids</c>"] Bids
        /// </summary>
        [JsonPropertyName("bids")]
        public BitMartFuturesOrderBookEntry[] Bids { get; set; } = Array.Empty<BitMartFuturesOrderBookEntry>();
    }
}
