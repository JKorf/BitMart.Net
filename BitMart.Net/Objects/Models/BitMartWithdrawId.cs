using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// ["<c>withdraw_id</c>"] Withdraw id
        /// </summary>
        [JsonPropertyName("withdraw_id")]
        public string WithdrawId { get; set; } = string.Empty;
    }
}
