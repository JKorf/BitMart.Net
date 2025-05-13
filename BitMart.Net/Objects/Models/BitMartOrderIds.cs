using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartOrderIdsWrapper
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msd")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("data")]
        public BitMartOrderIds Data { get; set; } = null!;
    }

    /// <summary>
    /// Order ids
    /// </summary>
    [SerializationModel]
    public record BitMartOrderIds
    {
        /// <summary>
        /// Order ids of the placed orders
        /// </summary>
        [JsonPropertyName("orderIds")]
        public string[] OrderIds { get; set; } = Array.Empty<string>();
    }
}
