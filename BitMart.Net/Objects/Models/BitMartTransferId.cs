using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Transfer id
    /// </summary>
    public record BitMartTransferId
    {
        /// <summary>
        /// Transfer id
        /// </summary>
        [JsonPropertyName("transfer_id")]
        public string TransferId { get; set; } = string.Empty;
    }
}
