using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    internal record BitMartAssetWrapper
    {
        /// <summary>
        /// ["<c>currencies</c>"] Currencies
        /// </summary>
        [JsonPropertyName("currencies")]
        public BitMartAsset[] Currencies { get; set; } = Array.Empty<BitMartAsset>();
    }

    /// <summary>
    /// Asset information
    /// </summary>
    [SerializationModel]
    public record BitMartAsset
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>withdraw_enabled</c>"] Withdraw enabled
        /// </summary>
        [JsonPropertyName("withdraw_enabled")]
        public bool WithdrawEnabled { get; set; }
        /// <summary>
        /// ["<c>deposit_enabled</c>"] Deposit enabled
        /// </summary>
        [JsonPropertyName("deposit_enabled")]
        public bool DepositEnabled { get; set; }
    }


}
