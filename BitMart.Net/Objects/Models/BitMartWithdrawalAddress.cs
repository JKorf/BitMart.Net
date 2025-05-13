using CryptoExchange.Net.Converters.SystemTextJson;
using BitMart.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartWithdrawalAddressesWrapper
    {
        [JsonPropertyName("list")]
        public BitMartWithdrawalAddress[] List { get; set; } = [];
    }

    /// <summary>
    /// Withdrawal address
    /// </summary>
    [SerializationModel]
    public record BitMartWithdrawalAddress
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Network name
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Memo
        /// </summary>
        [JsonPropertyName("memo")]
        public string? Memo { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        [JsonPropertyName("remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// Address type
        /// </summary>
        [JsonPropertyName("addressType")]
        public WithdrawalAddressType Type { get; set; }
        /// <summary>
        /// Is verified
        /// </summary>
        [JsonPropertyName("verifyStatus")]
        public bool Verified { get; set; }
    }
}
