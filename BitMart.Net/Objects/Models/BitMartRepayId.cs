using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Repayment id
    /// </summary>
    [SerializationModel]
    public record BitMartRepayId
    {
        /// <summary>
        /// ["<c>repay_id</c>"] Repay id
        /// </summary>
        [JsonPropertyName("repay_id")]
        public string RepayId { get; set; } = string.Empty;
    }


}
