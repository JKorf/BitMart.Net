using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Deposit/Withdrawal status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DepositWithdrawalStatus>))]
    public enum DepositWithdrawalStatus
    {
        /// <summary>
        /// Created
        /// </summary>
        [Map("0")]
        Created,
        /// <summary>
        /// Submitted
        /// </summary>
        [Map("1")]
        Submitted,
        /// <summary>
        /// Processing
        /// </summary>
        [Map("2")]
        Processing,
        /// <summary>
        /// Completed
        /// </summary>
        [Map("3")]
        Completed,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("4")]
        Canceled,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("5")]
        Failed
    }
}
