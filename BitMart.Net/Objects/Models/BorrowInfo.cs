using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    internal record BorrowInfoWrapper
    {
        /// <summary>
        /// Symbols
        /// </summary>
        [JsonPropertyName("symbols")]
        public IEnumerable<BorrowInfo> Symbols { get; set; } = Array.Empty<BorrowInfo>();
    }

    /// <summary>
    /// Borrow rate and quantity info
    /// </summary>
    public record BorrowInfo
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("max_leverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Symbol enabled
        /// </summary>
        [JsonPropertyName("symbol_enabled")]
        public bool SymbolEnabled { get; set; }
        /// <summary>
        /// Base
        /// </summary>
        [JsonPropertyName("base")]
        public BorrowInfoAsset Base { get; set; } = null!;
        /// <summary>
        /// Quote
        /// </summary>
        [JsonPropertyName("quote")]
        public BorrowInfoAsset Quote { get; set; } = null!;
    }

    /// <summary>
    /// Asset info
    /// </summary>
    public record BorrowInfoAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Daily interest
        /// </summary>
        [JsonPropertyName("daily_interest")]
        public decimal DailyInterest { get; set; }
        /// <summary>
        /// Hourly interest
        /// </summary>
        [JsonPropertyName("hourly_interest")]
        public decimal HourlyInterest { get; set; }
        /// <summary>
        /// Max borrow quantity
        /// </summary>
        [JsonPropertyName("max_borrow_amount")]
        public decimal MaxBorrowQuantity { get; set; }
        /// <summary>
        /// Min borrow quantity
        /// </summary>
        [JsonPropertyName("min_borrow_amount")]
        public decimal MinBorrowQuantity { get; set; }
        /// <summary>
        /// Borrowable quantity
        /// </summary>
        [JsonPropertyName("borrowable_amount")]
        public decimal BorrowableQuantity { get; set; }
    }
}
