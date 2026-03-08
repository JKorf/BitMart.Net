using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Ticker update
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesTickerUpdate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>volume_24</c>"] Volume in past 24 hours
        /// </summary>
        [JsonPropertyName("volume_24")]
        public decimal Volume24h { get; set; }

        /// <summary>
        /// ["<c>fair_price</c>"] Fair price
        /// </summary>
        [JsonPropertyName("fair_price")]
        public decimal FairPrice { get; set; }

        /// <summary>
        /// ["<c>last_price</c>"] Last trade price
        /// </summary>
        [JsonPropertyName("last_price")]
        public decimal LastPrice { get; set; }

        /// <summary>
        /// ["<c>range</c>"] Price range
        /// </summary>
        [JsonPropertyName("range")]
        public decimal PriceRange { get; set; }

        /// <summary>
        /// ["<c>ask_price</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("ask_price")]
        public decimal BestAskPrice { get; set; }

        /// <summary>
        /// ["<c>ask_vol</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("ask_vol")]
        public decimal BestAskQuantity { get; set; }

        /// <summary>
        /// ["<c>bid_price</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bid_price")]
        public decimal BestBidPrice { get; set; }

        /// <summary>
        /// ["<c>bid_vol</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("bid_vol")]
        public decimal BestBidQuantity { get; set; }
    }
}
