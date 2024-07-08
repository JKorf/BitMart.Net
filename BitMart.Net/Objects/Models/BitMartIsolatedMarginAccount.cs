using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    internal record BitMartIsolatedMarginAccountWrapper
    {
        /// <summary>
        /// Symbols
        /// </summary>
        [JsonPropertyName("symbols")]
        public IEnumerable<BitMartIsolatedMarginAccount> Symbols { get; set; } = Array.Empty<BitMartIsolatedMarginAccount>();
    }

    /// <summary>
    /// Account info
    /// </summary>
    public record BitMartIsolatedMarginAccount
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Risk rate
        /// </summary>
        [JsonPropertyName("risk_rate")]
        public decimal? RiskRate { get; set; }
        /// <summary>
        /// Risk level
        /// </summary>
        [JsonPropertyName("risk_level")]
        public decimal? RiskLevel { get; set; }
        /// <summary>
        /// Buy enabled
        /// </summary>
        [JsonPropertyName("buy_enabled")]
        public bool BuyEnabled { get; set; }
        /// <summary>
        /// Sell enabled
        /// </summary>
        [JsonPropertyName("sell_enabled")]
        public bool SellEnabled { get; set; }
        /// <summary>
        /// Liquidate price
        /// </summary>
        [JsonPropertyName("liquidate_price")]
        public decimal? LiquidatePrice { get; set; }
        /// <summary>
        /// Liquidate rate
        /// </summary>
        [JsonPropertyName("liquidate_rate")]
        public decimal? LiquidateRate { get; set; }
        /// <summary>
        /// Base
        /// </summary>
        [JsonPropertyName("base")]
        public BitMartIsolatedMarginAccountAsset Base { get; set; } = null!;
        /// <summary>
        /// Quote
        /// </summary>
        [JsonPropertyName("quote")]
        public BitMartIsolatedMarginAccountAsset Quote { get; set; } = null!;
    }

    /// <summary>
    /// Account asset info
    /// </summary>
    public record BitMartIsolatedMarginAccountAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Borrow enabled
        /// </summary>
        [JsonPropertyName("borrow_enabled")]
        public bool BorrowEnabled { get; set; }
        /// <summary>
        /// Borrowed
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal Borrowed { get; set; }
        /// <summary>
        /// Borrow unpaid
        /// </summary>
        [JsonPropertyName("borrow_unpaid")]
        public decimal BorrowUnpaid { get; set; }
        /// <summary>
        /// Interest unpaid
        /// </summary>
        [JsonPropertyName("interest_unpaid")]
        public decimal InterestUnpaid { get; set; }
        /// <summary>
        /// Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Frozen
        /// </summary>
        [JsonPropertyName("frozen")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// Net asset
        /// </summary>
        [JsonPropertyName("net_asset")]
        public decimal NetAsset { get; set; }
        /// <summary>
        /// Net asset BTC
        /// </summary>
        [JsonPropertyName("net_assetBTC")]
        public decimal NetAssetBTC { get; set; }
        /// <summary>
        /// Total asset
        /// </summary>
        [JsonPropertyName("total_asset")]
        public decimal TotalAsset { get; set; }
    }
}
