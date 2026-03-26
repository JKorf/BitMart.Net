using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartSpotBalanceWrapper
    {
        /// <summary>
        /// ["<c>wallet</c>"] Wallet
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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>available</c>"] Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>name</c>"] Asset
        /// </summary>
        [JsonPropertyName("name")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>frozen</c>"] Frozen
        /// </summary>
        [JsonPropertyName("frozen")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// ["<c>total</c>"] Total
        /// </summary>
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
    }


}
