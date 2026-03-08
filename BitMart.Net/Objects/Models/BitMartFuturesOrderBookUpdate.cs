using BitMart.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Order book
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesOrderBookUpdate
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
        /// ["<c>way</c>"] Side of the book
        /// </summary>
        [JsonPropertyName("way")]
        public OrderBookSide Side { get; set; }
        /// <summary>
        /// ["<c>depths</c>"] Depths, can either be bids or asks, check Side to see which
        /// </summary>
        [JsonPropertyName("depths")]
        public BitMartFuturesOrderBookEntry[] Depths { get; set; } = Array.Empty<BitMartFuturesOrderBookEntry>();
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesOrderBookEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>vol</c>"] Quantity
        /// </summary>
        [JsonPropertyName("vol")]
        public decimal Quantity { get; set; }
    }
}
