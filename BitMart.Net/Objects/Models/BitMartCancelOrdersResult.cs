using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Cancel orders result
    /// </summary>
    [SerializationModel]
    public record BitMartCancelOrdersResult
    {
        /// <summary>
        /// Success ids
        /// </summary>
        [JsonPropertyName("successIds")]
        public string[] SuccessIds { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Fail ids
        /// </summary>
        [JsonPropertyName("failIds")]
        public string[] FailIds { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Total count
        /// </summary>
        [JsonPropertyName("totalCount")]
        public decimal TotalCount { get; set; }
        /// <summary>
        /// Success count
        /// </summary>
        [JsonPropertyName("successCount")]
        public decimal SuccessCount { get; set; }
        /// <summary>
        /// Failed count
        /// </summary>
        [JsonPropertyName("failedCount")]
        public decimal FailedCount { get; set; }
    }

}
