using CryptoExchange.Net.Converters.SystemTextJson;
using BitMart.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Balance update
    /// </summary>
    [SerializationModel]
    public record BitMartBalanceUpdate
    {
        /// <summary>
        /// ["<c>event_type</c>"] Event type
        /// </summary>
        [JsonPropertyName("event_type")]
        public BalanceUpdateType EventType { get; set; }

        /// <summary>
        /// ["<c>account_type</c>"] Account type
        /// </summary>
        [JsonPropertyName("account_type")]
        public AccountType AccountType { get; set; }

        /// <summary>
        /// ["<c>event_time</c>"] Event time
        /// </summary>
        [JsonPropertyName("event_time")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ["<c>balance_details</c>"] Updated balances
        /// </summary>
        [JsonPropertyName("balance_details")]
        public BitMartBalanceUpdateDetails[] Balances { get; set; } = Array.Empty<BitMartBalanceUpdateDetails>();
    }

    /// <summary>
    /// Asset info
    /// </summary>
    [SerializationModel]
    public record BitMartBalanceUpdateDetails
    {
        /// <summary>
        /// ["<c>ccy</c>"] Asset
        /// </summary>
        [JsonPropertyName("ccy")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>av_bal</c>"] Available balance
        /// </summary>
        [JsonPropertyName("av_bal")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>fz_bal</c>"] Frozen balance
        /// </summary>
        [JsonPropertyName("fz_bal")]
        public decimal Frozen { get; set; }
    }
}
