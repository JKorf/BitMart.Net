using CryptoExchange.Net.Converters.SystemTextJson;
using BitMart.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartSymbolWrapper
    {
        /// <summary>
        /// ["<c>symbols</c>"] Symbols
        /// </summary>
        [JsonPropertyName("symbols")]
        public BitMartSymbol[] Symbols { get; set; } = Array.Empty<BitMartSymbol>();
    }

    /// <summary>
    /// Symbol info
    /// </summary>
    [SerializationModel]
    public record BitMartSymbol
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol_id</c>"] Symbol id
        /// </summary>
        [JsonPropertyName("symbol_id")]
        public long? SymbolId { get; set; }
        /// <summary>
        /// ["<c>base_currency</c>"] Base asset
        /// </summary>
        [JsonPropertyName("base_currency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quote_currency</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quote_currency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quote_increment</c>"] Quote increment
        /// </summary>
        [JsonPropertyName("quote_increment")]
        public decimal? QuoteIncrement { get; set; }
        /// <summary>
        /// ["<c>base_min_size</c>"] Base min quantity
        /// </summary>
        [JsonPropertyName("base_min_size")]
        public decimal? BaseMinQuantity { get; set; }
        /// <summary>
        /// ["<c>price_min_precision</c>"] Price min precision
        /// </summary>
        [JsonPropertyName("price_min_precision")]
        public int PriceMinPrecision { get; set; }
        /// <summary>
        /// ["<c>price_max_precision</c>"] Price max precision
        /// </summary>
        [JsonPropertyName("price_max_precision")]
        public int PriceMaxPrecision { get; set; }
        /// <summary>
        /// ["<c>expiration</c>"] Expiration
        /// </summary>
        [JsonPropertyName("expiration")]
        public string Expiration { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>min_buy_amount</c>"] Min buy quantity
        /// </summary>
        [JsonPropertyName("min_buy_amount")]
        public decimal? MinBuyQuantity { get; set; }
        /// <summary>
        /// ["<c>min_sell_amount</c>"] Min sell quantity
        /// </summary>
        [JsonPropertyName("min_sell_amount")]
        public decimal? MinSellQuantity { get; set; }
        /// <summary>
        /// ["<c>trade_status</c>"] Trade status
        /// </summary>
        [JsonPropertyName("trade_status")]
        public SymbolStatus? TradeStatus { get; set; }
        /// <summary>
        /// ["<c>planned_down_time</c>"] Expected delisting time
        /// </summary>
        [JsonPropertyName("planned_down_time")]
        public DateTime? PlannedDelistTime { get; set; }
    }


}
