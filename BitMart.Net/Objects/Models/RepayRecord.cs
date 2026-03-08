using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record RepayRecordWrapper
    {
        /// <summary>
        /// ["<c>records</c>"] Records
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
        /// ["<c>repay_id</c>"] Repay id
        /// </summary>
        [JsonPropertyName("repay_id")]
        public string RepayId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>repay_time</c>"] Repay time
        /// </summary>
        [JsonPropertyName("repay_time")]
        public DateTime? RepayTime { get; set; }
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
        /// ["<c>repaid_amount</c>"] Repaid quantity
        /// </summary>
        [JsonPropertyName("repaid_amount")]
        public decimal RepaidQuantity { get; set; }
        /// <summary>
        /// ["<c>repaid_principal</c>"] Repaid principal
        /// </summary>
        [JsonPropertyName("repaid_principal")]
        public decimal RepaidPrincipal { get; set; }
        /// <summary>
        /// ["<c>repaid_interest</c>"] Repaid interest
        /// </summary>
        [JsonPropertyName("repaid_interest")]
        public decimal RepaidInterest { get; set; }
    }


}
