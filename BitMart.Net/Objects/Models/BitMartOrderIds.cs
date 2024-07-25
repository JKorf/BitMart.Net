using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
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
    public record BitMartOrderIds
    {
        /// <summary>
        /// Order ids of the placed orders
        /// </summary>
        [JsonPropertyName("orderIds")]
        public IEnumerable<string> OrderIds { get; set; } = Array.Empty<string>();
    }
}
