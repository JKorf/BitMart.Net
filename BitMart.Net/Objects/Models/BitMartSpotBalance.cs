using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartSpotBalanceWrapper
    {
        /// <summary>
        /// Wallet
        /// </summary>
        [JsonPropertyName("wallet")]
        public BitMartSpotBalance[] Wallet { get; set; } = Array.Empty<BitMartSpotBalance>();
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record BitMartSpotBalance
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("name")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Frozen
        /// </summary>
        [JsonPropertyName("frozen")]
        public decimal Frozen { get; set; }
    }


}
