using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    internal record BitMartSubAccountBalanceWrapper
    {
        /// <summary>
        /// Wallet
        /// </summary>
        [JsonPropertyName("wallet")]
        public IEnumerable<BitMartSubAccountBalance> Wallet { get; set; } = Array.Empty<BitMartSubAccountBalance>();
    }

    /// <summary>
    /// Sub account balance
    /// </summary>
    public record BitMartSubAccountBalance
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Frozen
        /// </summary>
        [JsonPropertyName("frozen")]
        public decimal Frozen { get; set; }
    }


}
