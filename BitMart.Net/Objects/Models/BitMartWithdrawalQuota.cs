using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Withdrawal quota
    /// </summary>
    [SerializationModel]
    public record BitMartWithdrawalQuota
    {
        /// <summary>
        /// ["<c>today_available_withdraw_BTC</c>"] Todays available withdraw of BTC
        /// </summary>
        [JsonPropertyName("today_available_withdraw_BTC")]
        public decimal AvailableWithdrawBtc { get; set; }
        /// <summary>
        /// ["<c>min_withdraw</c>"] Min withdraw
        /// </summary>
        [JsonPropertyName("min_withdraw")]
        public decimal MinWithdraw { get; set; }
        /// <summary>
        /// ["<c>withdraw_precision</c>"] Withdraw precision
        /// </summary>
        [JsonPropertyName("withdraw_precision")]
        public decimal WithdrawPrecision { get; set; }
        /// <summary>
        /// ["<c>withdraw_fee</c>"] Withdraw fee
        /// </summary>
        [JsonPropertyName("withdraw_fee")]
        public decimal WithdrawFee { get; set; }
        /// <summary>
        /// ["<c>withdraw_Precision_GeTen</c>"] Withdraw precision step
        /// </summary>
        [JsonPropertyName("withdraw_Precision_GeTen")]
        public decimal? WithdrawPrecisionStep { get; set; }
    }


}
