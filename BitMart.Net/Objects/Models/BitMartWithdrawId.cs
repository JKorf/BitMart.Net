using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Withdrawal id
    /// </summary>
    [SerializationModel]
    public record BitMartWithdrawId
    {
        /// <summary>
        /// Withdraw id
        /// </summary>
        [JsonPropertyName("withdraw_id")]
        public string WithdrawId { get; set; } = string.Empty;
    }
}
