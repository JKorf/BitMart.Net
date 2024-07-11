using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Ticker update
    /// </summary>
    public record BitMartFuturesTickerUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Volume in past 24 hours
        /// </summary>
        [JsonPropertyName("volume_24")]
        public decimal Volume24h { get; set; }

        /// <summary>
        /// Fair price
        /// </summary>
        [JsonPropertyName("fair_price")]
        public decimal FairPrice { get; set; }

        /// <summary>
        /// Last trade price
        /// </summary>
        [JsonPropertyName("last_price")]
        public decimal LastPrice { get; set; }

        /// <summary>
        /// Price range
        /// </summary>
        [JsonPropertyName("range")]
        public decimal PriceRange { get; set; }

        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("ask_price")]
        public decimal BestAskPrice { get; set; }

        /// <summary>
        /// Best ask quantity
        /// </summary>
        [JsonPropertyName("ask_vol")]
        public decimal BestAskQuantity { get; set; }

        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("bid_price")]
        public decimal BestBidPrice { get; set; }

        /// <summary>
        /// Best bid quantity
        /// </summary>
        [JsonPropertyName("bid_vol")]
        public decimal BestBidQuantity { get; set; }
    }
}
