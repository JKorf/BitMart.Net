﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    internal record BitMartSymbolWrapper
    {
        /// <summary>
        /// Symbols
        /// </summary>
        [JsonPropertyName("symbols")]
        public IEnumerable<BitMartSymbol> Symbols { get; set; } = Array.Empty<BitMartSymbol>();
    }

    /// <summary>
    /// Symbol info
    /// </summary>
    public record BitMartSymbol
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Symbol id
        /// </summary>
        [JsonPropertyName("symbol_id")]
        public long? SymbolId { get; set; }
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("base_currency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("quote_currency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote increment
        /// </summary>
        [JsonPropertyName("quote_increment")]
        public decimal? QuoteIncrement { get; set; }
        /// <summary>
        /// Base min quantity
        /// </summary>
        [JsonPropertyName("base_min_size")]
        public decimal? BaseMinQuantity { get; set; }
        /// <summary>
        /// Price min precision
        /// </summary>
        [JsonPropertyName("price_min_precision")]
        public decimal PriceMinPrecision { get; set; }
        /// <summary>
        /// Price max precision
        /// </summary>
        [JsonPropertyName("price_max_precision")]
        public decimal PriceMaxPrecision { get; set; }
        /// <summary>
        /// Expiration
        /// </summary>
        [JsonPropertyName("expiration")]
        public string Expiration { get; set; } = string.Empty;
        /// <summary>
        /// Min buy quantity
        /// </summary>
        [JsonPropertyName("min_buy_amount")]
        public decimal? MinBuyQuantity { get; set; }
        /// <summary>
        /// Min sell quantity
        /// </summary>
        [JsonPropertyName("min_sell_amount")]
        public decimal? MinSellQuantity { get; set; }
        /// <summary>
        /// Trade status
        /// </summary>
        [JsonPropertyName("trade_status")]
        public string? TradeStatus { get; set; }
    }


}
