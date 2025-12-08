using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Transfer id
    /// </summary>
    [SerializationModel]
    public record BitMartTransferId
    {
        /// <summary>
        /// Transfer id
        /// </summary>
        [JsonPropertyName("transfer_id")]
        public string TransferId { get; set; } = string.Empty;
    }
}
