using CryptoExchange.Net.Converters.SystemTextJson;
using BitMart.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartSubAccountWrapper
    {
        /// <summary>
        /// Sub account list
        /// </summary>
        [JsonPropertyName("subAccountList")]
        public BitMartSubAccount[] SubAccountList { get; set; } = Array.Empty<BitMartSubAccount>();
    }

    /// <summary>
    /// Sub account info
    /// </summary>
    [SerializationModel]
    public record BitMartSubAccount
    {
        /// <summary>
        /// Account name
        /// </summary>
        [JsonPropertyName("accountName")]
        public string AccountName { get; set; } = string.Empty;
        /// <summary>
        /// Account status
        /// </summary>
        [JsonPropertyName("status")]
        public SubAccountStatus Status { get; set; }
    }


}
