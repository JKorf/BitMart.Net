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
        /// Todays available withdraw of BTC
        /// </summary>
        [JsonPropertyName("today_available_withdraw_BTC")]
        public decimal AvailableWithdrawBtc { get; set; }
        /// <summary>
        /// Min withdraw
        /// </summary>
        [JsonPropertyName("min_withdraw")]
        public decimal MinWithdraw { get; set; }
        /// <summary>
        /// Withdraw precision
        /// </summary>
        [JsonPropertyName("withdraw_precision")]
        public decimal WithdrawPrecision { get; set; }
        /// <summary>
        /// Withdraw fee
        /// </summary>
        [JsonPropertyName("withdraw_fee")]
        public decimal WithdrawFee { get; set; }
        /// <summary>
        /// Withdraw precision step
        /// </summary>
        [JsonPropertyName("withdraw_Precision_GeTen")]
        public decimal? WithdrawPrecisionStep { get; set; }
    }


}
