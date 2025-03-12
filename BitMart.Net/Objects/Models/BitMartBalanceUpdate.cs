using CryptoExchange.Net.Converters.SystemTextJson;
using BitMart.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
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
        /// Event type
        /// </summary>
        [JsonPropertyName("event_type")]
        public BalanceUpdateType EventType { get; set; }

        /// <summary>
        /// Event time
        /// </summary>
        [JsonPropertyName("event_time")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Updated balances
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
        /// Asset
        /// </summary>
        [JsonPropertyName("ccy")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("av_bal")]
        public decimal Available { get; set; }
        /// <summary>
        /// Frozen balance
        /// </summary>
        [JsonPropertyName("fz_bal")]
        public decimal Frozen { get; set; }
    }
}
