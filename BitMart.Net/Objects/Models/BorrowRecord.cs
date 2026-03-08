using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BorrowRecordWrapper
    {
        /// <summary>
        /// ["<c>records</c>"] Records
        /// </summary>
        [JsonPropertyName("records")]
        public BorrowRecord[] Records { get; set; } = Array.Empty<BorrowRecord>();
    }

    /// <summary>
    /// Borrow details
    /// </summary>
    [SerializationModel]
    public record BorrowRecord
    {
        /// <summary>
        /// ["<c>borrow_id</c>"] Borrow id
        /// </summary>
        [JsonPropertyName("borrow_id")]
        public string BorrowId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>borrow_amount</c>"] Borrow quantity
        /// </summary>
        [JsonPropertyName("borrow_amount")]
        public decimal BorrowQuantity { get; set; }
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
        /// ["<c>interest_amount</c>"] Interest quantity
        /// </summary>
        [JsonPropertyName("interest_amount")]
        public decimal InterestQuantity { get; set; }
        /// <summary>
        /// ["<c>create_time</c>"] Create time
        /// </summary>
        [JsonPropertyName("create_time")]
        public DateTime CreateTime { get; set; }
    }


}
