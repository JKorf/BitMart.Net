using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record RepayRecordWrapper
    {
        /// <summary>
        /// Records
        /// </summary>
        [JsonPropertyName("records")]
        public RepayRecord[] Records { get; set; } = Array.Empty<RepayRecord>();
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record RepayRecord
    {
        /// <summary>
        /// Repay id
        /// </summary>
        [JsonPropertyName("repay_id")]
        public string RepayId { get; set; } = string.Empty;
        /// <summary>
        /// Repay time
        /// </summary>
        [JsonPropertyName("repay_time")]
        public DateTime? RepayTime { get; set; }
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
        /// Repaid quantity
        /// </summary>
        [JsonPropertyName("repaid_amount")]
        public decimal RepaidQuantity { get; set; }
        /// <summary>
        /// Repaid principal
        /// </summary>
        [JsonPropertyName("repaid_principal")]
        public decimal RepaidPrincipal { get; set; }
        /// <summary>
        /// Repaid interest
        /// </summary>
        [JsonPropertyName("repaid_interest")]
        public decimal RepaidInterest { get; set; }
    }


}
