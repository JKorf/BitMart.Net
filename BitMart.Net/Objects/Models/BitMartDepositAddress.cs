using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Deposit address info
    /// </summary>
    [SerializationModel]
    public record BitMartDepositAddress
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>chain</c>"] Network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>address</c>"] Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>address_memo</c>"] Address memo
        /// </summary>
        [JsonPropertyName("address_memo")]
        public string? AddressMemo { get; set; }
    }


}
