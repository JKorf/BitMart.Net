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
        /// ["<c>0</c>"] Created
        /// </summary>
        [Map("0")]
        Created,
        /// <summary>
        /// ["<c>1</c>"] Submitted
        /// </summary>
        [Map("1")]
        Submitted,
        /// <summary>
        /// ["<c>2</c>"] Processing
        /// </summary>
        [Map("2")]
        Processing,
        /// <summary>
        /// ["<c>3</c>"] Completed
        /// </summary>
        [Map("3")]
        Completed,
        /// <summary>
        /// ["<c>4</c>"] Canceled
        /// </summary>
        [Map("4")]
        Canceled,
        /// <summary>
        /// ["<c>5</c>"] Failed
        /// </summary>
        [Map("5")]
        Failed
    }
}
