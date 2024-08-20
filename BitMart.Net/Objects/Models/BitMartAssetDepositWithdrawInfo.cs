using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    internal record BitMartAssetDepositWithdrawInfoWrapper
    {
        /// <summary>
        /// Currencies
        /// </summary>
        [JsonPropertyName("currencies")]
        public IEnumerable<BitMartAssetDepositWithdrawInfo> Currencies { get; set; } = Array.Empty<BitMartAssetDepositWithdrawInfo>();
    }

    /// <summary>
    /// Deposit/withdrawal info
    /// </summary>
    public record BitMartAssetDepositWithdrawInfo
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
        /// Contract address
        /// </summary>
        [JsonPropertyName("contract_address")]
        public string? ContractAddress { get; set; }
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
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
        /// <summary>
        /// Withdraw minsize
        /// </summary>
        [JsonPropertyName("withdraw_minsize")]
        public decimal? WithdrawMinsize { get; set; }
        /// <summary>
        /// Withdraw minimal fee in USDT
        /// </summary>
        [JsonPropertyName("withdraw_minfee")]
        public decimal? WithdrawMinfee { get; set; }
    }


}
