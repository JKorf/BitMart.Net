using CryptoExchange.Net.Converters.SystemTextJson;
using BitMart.Net.Enums;
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
        /// ["<c>currency</c>"] Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>network</c>"] Network name
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>address</c>"] Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>memo</c>"] Memo
        /// </summary>
        [JsonPropertyName("memo")]
        public string? Memo { get; set; }
        /// <summary>
        /// ["<c>remark</c>"] Remark
        /// </summary>
        [JsonPropertyName("remark")]
        public string? Remark { get; set; }
        /// <summary>
        /// ["<c>addressType</c>"] Address type
        /// </summary>
        [JsonPropertyName("addressType")]
        public WithdrawalAddressType Type { get; set; }
        /// <summary>
        /// ["<c>verifyStatus</c>"] Is verified
        /// </summary>
        [JsonPropertyName("verifyStatus")]
        public bool Verified { get; set; }
    }
}
