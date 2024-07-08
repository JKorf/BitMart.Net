using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
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
