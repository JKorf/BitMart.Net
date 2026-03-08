using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BorrowInfoWrapper
    {
        /// <summary>
        /// ["<c>symbols</c>"] Symbols
        /// </summary>
        [JsonPropertyName("symbols")]
        public BorrowInfo[] Symbols { get; set; } = Array.Empty<BorrowInfo>();
    }

    /// <summary>
    /// Borrow rate and quantity info
    /// </summary>
    [SerializationModel]
    public record BorrowInfo
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>max_leverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("max_leverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>symbol_enabled</c>"] Symbol enabled
        /// </summary>
        [JsonPropertyName("symbol_enabled")]
        public bool SymbolEnabled { get; set; }
        /// <summary>
        /// ["<c>base</c>"] Base
        /// </summary>
        [JsonPropertyName("base")]
        public BorrowInfoAsset Base { get; set; } = null!;
        /// <summary>
        /// ["<c>quote</c>"] Quote
        /// </summary>
        [JsonPropertyName("quote")]
        public BorrowInfoAsset Quote { get; set; } = null!;
    }

    /// <summary>
    /// Asset info
    /// </summary>
    [SerializationModel]
    public record BorrowInfoAsset
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>daily_interest</c>"] Daily interest
        /// </summary>
        [JsonPropertyName("daily_interest")]
        public decimal DailyInterest { get; set; }
        /// <summary>
        /// ["<c>hourly_interest</c>"] Hourly interest
        /// </summary>
        [JsonPropertyName("hourly_interest")]
        public decimal HourlyInterest { get; set; }
        /// <summary>
        /// ["<c>max_borrow_amount</c>"] Max borrow quantity
        /// </summary>
        [JsonPropertyName("max_borrow_amount")]
        public decimal MaxBorrowQuantity { get; set; }
        /// <summary>
        /// ["<c>min_borrow_amount</c>"] Min borrow quantity
        /// </summary>
        [JsonPropertyName("min_borrow_amount")]
        public decimal MinBorrowQuantity { get; set; }
        /// <summary>
        /// ["<c>borrowable_amount</c>"] Borrowable quantity
        /// </summary>
        [JsonPropertyName("borrowable_amount")]
        public decimal BorrowableQuantity { get; set; }
    }
}
