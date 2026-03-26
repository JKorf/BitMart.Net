using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartAssetDepositWithdrawInfoWrapper
    {
        /// <summary>
        /// ["<c>currencies</c>"] Currencies
        /// </summary>
        [JsonPropertyName("currencies")]
        public BitMartAssetDepositWithdrawInfo[] Currencies { get; set; } = Array.Empty<BitMartAssetDepositWithdrawInfo>();
    }

    /// <summary>
    /// Deposit/withdrawal info
    /// </summary>
    [SerializationModel]
    public record BitMartAssetDepositWithdrawInfo
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
        /// ["<c>contract_address</c>"] Contract address
        /// </summary>
        [JsonPropertyName("contract_address")]
        public string? ContractAddress { get; set; }
        /// <summary>
        /// ["<c>network</c>"] Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
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
        /// <summary>
        /// ["<c>withdraw_minsize</c>"] Withdraw min size
        /// </summary>
        [JsonPropertyName("withdraw_minsize")]
        public decimal? WithdrawMinsize { get; set; }
        /// <summary>
        /// ["<c>recharge_minsize</c>"] Recharge min size
        /// </summary>
        [JsonPropertyName("recharge_minsize")]
        public decimal? RechargeMinSize { get; set; }
        /// <summary>
        /// ["<c>withdraw_minfee</c>"] Withdraw minimal fee in USDT
        /// </summary>
        [JsonPropertyName("withdraw_minfee")]
        public decimal? WithdrawMinfee { get; set; }
        /// <summary>
        /// ["<c>withdraw_fee_estimate</c>"] Withdraw fee estimate
        /// </summary>
        [JsonPropertyName("withdraw_fee_estimate")]
        public decimal? WithdrawFeeEstimate { get; set; }
        /// <summary>
        /// ["<c>withdraw_fee</c>"] Withdraw fee
        /// </summary>
        [JsonPropertyName("withdraw_fee")]
        public decimal? WithdrawFee { get; set; }
    }


}
