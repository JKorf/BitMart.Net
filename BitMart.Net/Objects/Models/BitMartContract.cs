using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using BitMart.Net.Enums;

namespace BitMart.Net.Objects.Models
{
    internal record BitMartContractWrapper
    {
        /// <summary>
        /// Symbols
        /// </summary>
        [JsonPropertyName("symbols")]
        public IEnumerable<BitMartContract> Symbols { get; set; } = Array.Empty<BitMartContract>();
    }

    /// <summary>
    /// Contract info
    /// </summary>
    public record BitMartContract
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Product type
        /// </summary>
        [JsonPropertyName("product_type")]
        public ContractType ProductType { get; set; }
        /// <summary>
        /// Open time
        /// </summary>
        [JsonPropertyName("open_timestamp")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Expire time
        /// </summary>
        [JsonPropertyName("expire_timestamp")]
        public DateTime? ExpireTime { get; set; }
        /// <summary>
        /// Settle time
        /// </summary>
        [JsonPropertyName("settle_timestamp")]
        public DateTime? SettleTime { get; set; }
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
        /// Last price
        /// </summary>
        [JsonPropertyName("last_price")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// Volume24h
        /// </summary>
        [JsonPropertyName("volume_24h")]
        public decimal Volume24h { get; set; }
        /// <summary>
        /// Turnover24h
        /// </summary>
        [JsonPropertyName("turnover_24h")]
        public decimal Turnover24h { get; set; }
        /// <summary>
        /// Index price
        /// </summary>
        [JsonPropertyName("index_price")]
        public decimal? IndexPrice { get; set; }
        /// <summary>
        /// Index name
        /// </summary>
        [JsonPropertyName("index_name")]
        public string IndexName { get; set; } = string.Empty;
        /// <summary>
        /// Contract quantity
        /// </summary>
        [JsonPropertyName("contract_size")]
        public decimal ContractQuantity { get; set; }
        /// <summary>
        /// Min leverage
        /// </summary>
        [JsonPropertyName("min_leverage")]
        public decimal MinLeverage { get; set; }
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("max_leverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Price precision
        /// </summary>
        [JsonPropertyName("price_precision")]
        public decimal PricePrecision { get; set; }
        /// <summary>
        /// Quantity precision
        /// </summary>
        [JsonPropertyName("vol_precision")]
        public decimal QuantityPrecision { get; set; }
        /// <summary>
        /// Max quantity
        /// </summary>
        [JsonPropertyName("max_volume")]
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// Min quantity
        /// </summary>
        [JsonPropertyName("min_volume")]
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Expected funding rate
        /// </summary>
        [JsonPropertyName("expected_funding_rate")]
        public decimal? ExpectedFundingRate { get; set; }
        /// <summary>
        /// Open interest
        /// </summary>
        [JsonPropertyName("open_interest")]
        public decimal OpenInterest { get; set; }
        /// <summary>
        /// Open interest value
        /// </summary>
        [JsonPropertyName("open_interest_value")]
        public decimal OpenInterestValue { get; set; }
        /// <summary>
        /// High price last 24h
        /// </summary>
        [JsonPropertyName("high_24h")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// Low price last 24h
        /// </summary>
        [JsonPropertyName("low_24h")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// Change in the last 24h
        /// </summary>
        [JsonPropertyName("change_24h")]
        public decimal Change24h { get; set; }
    }


}
