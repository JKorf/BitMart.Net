using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// 
    /// </summary>
    internal record BitMartAssetWrapper
    {
        /// <summary>
        /// Currencies
        /// </summary>
        [JsonPropertyName("currencies")]
        public IEnumerable<BitMartAsset> Currencies { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public record BitMartAsset
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Withdraw enabled
        /// </summary>
        [JsonPropertyName("withdraw_enabled")]
        public bool WithdrawEnabled { get; set; }
        /// <summary>
        /// Deposit enabled
        /// </summary>
        [JsonPropertyName("deposit_enabled")]
        public bool DepositEnabled { get; set; }
    }


}
