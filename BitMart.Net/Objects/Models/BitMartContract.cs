using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using BitMart.Net.Enums;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartContractWrapper
    {
        /// <summary>
        /// ["<c>symbols</c>"] Symbols
        /// </summary>
        [JsonPropertyName("symbols")]
        public BitMartContract[] Symbols { get; set; } = Array.Empty<BitMartContract>();
    }

    /// <summary>
    /// Contract info
    /// </summary>
    [SerializationModel]
    public record BitMartContract
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>product_type</c>"] Product type
        /// </summary>
        [JsonPropertyName("product_type")]
        public ContractType ProductType { get; set; }
        /// <summary>
        /// ["<c>open_timestamp</c>"] Open time
        /// </summary>
        [JsonPropertyName("open_timestamp")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>expire_timestamp</c>"] Expire time
        /// </summary>
        [JsonPropertyName("expire_timestamp")]
        public DateTime? ExpireTime { get; set; }
        /// <summary>
        /// ["<c>settle_timestamp</c>"] Settle time
        /// </summary>
        [JsonPropertyName("settle_timestamp")]
        public DateTime? SettleTime { get; set; }
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
        /// ["<c>last_price</c>"] Last price
        /// </summary>
        [JsonPropertyName("last_price")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// ["<c>volume_24h</c>"] Volume24h
        /// </summary>
        [JsonPropertyName("volume_24h")]
        public decimal Volume24h { get; set; }
        /// <summary>
        /// ["<c>turnover_24h</c>"] Turnover24h
        /// </summary>
        [JsonPropertyName("turnover_24h")]
        public decimal Turnover24h { get; set; }
        /// <summary>
        /// ["<c>index_price</c>"] Index price
        /// </summary>
        [JsonPropertyName("index_price")]
        public decimal? IndexPrice { get; set; }
        /// <summary>
        /// ["<c>index_name</c>"] Index name
        /// </summary>
        [JsonPropertyName("index_name")]
        public string IndexName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_size</c>"] Contract quantity
        /// </summary>
        [JsonPropertyName("contract_size")]
        public decimal ContractQuantity { get; set; }
        /// <summary>
        /// ["<c>min_leverage</c>"] Min leverage
        /// </summary>
        [JsonPropertyName("min_leverage")]
        public decimal MinLeverage { get; set; }
        /// <summary>
        /// ["<c>max_leverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("max_leverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>price_precision</c>"] Price precision
        /// </summary>
        [JsonPropertyName("price_precision")]
        public decimal PricePrecision { get; set; }
        /// <summary>
        /// ["<c>vol_precision</c>"] Quantity precision
        /// </summary>
        [JsonPropertyName("vol_precision")]
        public decimal QuantityPrecision { get; set; }
        /// <summary>
        /// ["<c>max_volume</c>"] Max quantity
        /// </summary>
        [JsonPropertyName("max_volume")]
        public decimal MaxQuantity { get; set; }
        /// <summary>
        /// ["<c>min_volume</c>"] Min quantity
        /// </summary>
        [JsonPropertyName("min_volume")]
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// ["<c>funding_rate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>expected_funding_rate</c>"] Expected funding rate
        /// </summary>
        [JsonPropertyName("expected_funding_rate")]
        public decimal? ExpectedFundingRate { get; set; }
        /// <summary>
        /// ["<c>open_interest</c>"] Open interest
        /// </summary>
        [JsonPropertyName("open_interest")]
        public decimal OpenInterest { get; set; }
        /// <summary>
        /// ["<c>open_interest_value</c>"] Open interest value
        /// </summary>
        [JsonPropertyName("open_interest_value")]
        public decimal OpenInterestValue { get; set; }
        /// <summary>
        /// ["<c>high_24h</c>"] High price last 24h
        /// </summary>
        [JsonPropertyName("high_24h")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// ["<c>low_24h</c>"] Low price last 24h
        /// </summary>
        [JsonPropertyName("low_24h")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// ["<c>change_24h</c>"] Change in the last 24h
        /// </summary>
        [JsonPropertyName("change_24h")]
        public decimal Change24h { get; set; }
        /// <summary>
        /// ["<c>funding_interval_hours</c>"] Interval of funding in hours
        /// </summary>
        [JsonPropertyName("funding_interval_hours")]
        public int? FundingIntervalHours { get; set; }
        /// <summary>
        /// ["<c>market_max_volume</c>"] Maximum market order quantity
        /// </summary>
        [JsonPropertyName("market_max_volume")]
        public decimal MaxMarketOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public ContractStatus Status { get; set; }
        /// <summary>
        /// ["<c>delist_time</c>"] Delist time
        /// </summary>
        [JsonPropertyName("delist_time")]
        public DateTime? DelistTime { get; set; }
    }


}
