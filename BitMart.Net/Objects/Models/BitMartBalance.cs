using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    internal record BitMartBalanceWrapper
    {
        /// <summary>
        /// Wallet
        /// </summary>
        [JsonPropertyName("wallet")]
        public IEnumerable<BitMartBalance> Wallet { get; set; } = Array.Empty<BitMartBalance>();
    }

    /// <summary>
    /// Balance info
    /// </summary>
    public record BitMartBalance
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
