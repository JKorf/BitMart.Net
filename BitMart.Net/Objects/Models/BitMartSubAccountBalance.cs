using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartSubAccountBalanceWrapper
    {
        /// <summary>
        /// ["<c>wallet</c>"] Wallet
        /// </summary>
        [JsonPropertyName("wallet")]
        public BitMartSubAccountBalance[] Wallet { get; set; } = Array.Empty<BitMartSubAccountBalance>();
    }

    /// <summary>
    /// Sub account balance
    /// </summary>
    [SerializationModel]
    public record BitMartSubAccountBalance
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>available</c>"] Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// ["<c>frozen</c>"] Frozen
        /// </summary>
        [JsonPropertyName("frozen")]
        public decimal Frozen { get; set; }
    }


}
