using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartIsolatedMarginAccountWrapper
    {
        /// <summary>
        /// ["<c>symbols</c>"] Symbols
        /// </summary>
        [JsonPropertyName("symbols")]
        public BitMartIsolatedMarginAccount[] Symbols { get; set; } = Array.Empty<BitMartIsolatedMarginAccount>();
    }

    /// <summary>
    /// Account info
    /// </summary>
    [SerializationModel]
    public record BitMartIsolatedMarginAccount
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>risk_rate</c>"] Risk rate
        /// </summary>
        [JsonPropertyName("risk_rate")]
        public decimal? RiskRate { get; set; }
        /// <summary>
        /// ["<c>risk_level</c>"] Risk level
        /// </summary>
        [JsonPropertyName("risk_level")]
        public decimal? RiskLevel { get; set; }
        /// <summary>
        /// ["<c>buy_enabled</c>"] Buy enabled
        /// </summary>
        [JsonPropertyName("buy_enabled")]
        public bool BuyEnabled { get; set; }
        /// <summary>
        /// ["<c>sell_enabled</c>"] Sell enabled
        /// </summary>
        [JsonPropertyName("sell_enabled")]
        public bool SellEnabled { get; set; }
        /// <summary>
        /// ["<c>liquidate_price</c>"] Liquidate price
        /// </summary>
        [JsonPropertyName("liquidate_price")]
        public decimal? LiquidatePrice { get; set; }
        /// <summary>
        /// ["<c>liquidate_rate</c>"] Liquidate rate
        /// </summary>
        [JsonPropertyName("liquidate_rate")]
        public decimal? LiquidateRate { get; set; }
        /// <summary>
        /// ["<c>base</c>"] Base
        /// </summary>
        [JsonPropertyName("base")]
        public BitMartIsolatedMarginAccountAsset Base { get; set; } = null!;
        /// <summary>
        /// ["<c>quote</c>"] Quote
        /// </summary>
        [JsonPropertyName("quote")]
        public BitMartIsolatedMarginAccountAsset Quote { get; set; } = null!;
    }

    /// <summary>
    /// Account asset info
    /// </summary>
    [SerializationModel]
    public record BitMartIsolatedMarginAccountAsset
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>borrow_enabled</c>"] Borrow enabled
        /// </summary>
        [JsonPropertyName("borrow_enabled")]
        public bool BorrowEnabled { get; set; }
        /// <summary>
        /// ["<c>borrowed</c>"] Borrowed
        /// </summary>
        [JsonPropertyName("borrowed")]
        public decimal Borrowed { get; set; }
        /// <summary>
        /// ["<c>borrow_unpaid</c>"] Borrow unpaid
        /// </summary>
        [JsonPropertyName("borrow_unpaid")]
        public decimal BorrowUnpaid { get; set; }
        /// <summary>
        /// ["<c>interest_unpaid</c>"] Interest unpaid
        /// </summary>
        [JsonPropertyName("interest_unpaid")]
        public decimal InterestUnpaid { get; set; }
        /// <summary>
        /// ["<c>available</c>"] Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>frozen</c>"] Frozen
        /// </summary>
        [JsonPropertyName("frozen")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// ["<c>net_asset</c>"] Net asset
        /// </summary>
        [JsonPropertyName("net_asset")]
        public decimal NetAsset { get; set; }
        /// <summary>
        /// ["<c>net_assetBTC</c>"] Net asset BTC
        /// </summary>
        [JsonPropertyName("net_assetBTC")]
        public decimal NetAssetBTC { get; set; }
        /// <summary>
        /// ["<c>total_asset</c>"] Total asset
        /// </summary>
        [JsonPropertyName("total_asset")]
        public decimal TotalAsset { get; set; }
    }
}
