using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    internal record BorrowRecordWrapper
    {
        /// <summary>
        /// Records
        /// </summary>
        [JsonPropertyName("records")]
        public IEnumerable<BorrowRecord> Records { get; set; } = Array.Empty<BorrowRecord>();
    }

    /// <summary>
    /// Borrow details
    /// </summary>
    public record BorrowRecord
    {
        /// <summary>
        /// Borrow id
        /// </summary>
        [JsonPropertyName("borrow_id")]
        public string BorrowId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Borrow quantity
        /// </summary>
        [JsonPropertyName("borrow_amount")]
        public decimal BorrowQuantity { get; set; }
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
        /// Interest quantity
        /// </summary>
        [JsonPropertyName("interest_amount")]
        public decimal InterestQuantity { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
    }


}
