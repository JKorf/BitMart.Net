using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Balance update type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BalanceUpdateType>))]
    public enum BalanceUpdateType
    {
        /// <summary>
        /// ["<c>TRANSACTION_COMPLETED</c>"] Trade
        /// </summary>
        [Map("TRANSACTION_COMPLETED")]
        Trade,
        /// <summary>
        /// ["<c>ACCOUNT_RECHARGE</c>"] Deposit
        /// </summary>
        [Map("ACCOUNT_RECHARGE")]
        Deposit,
        /// <summary>
        /// ["<c>ACCOUNT_WITHDRAWAL</c>"] Withdrawal
        /// </summary>
        [Map("ACCOUNT_WITHDRAWAL")]
        Withdrawal,
        /// <summary>
        /// ["<c>ACCOUNT_TRANSFER</c>"] Transfer
        /// </summary>
        [Map("ACCOUNT_TRANSFER")]
        Transfer,
        /// <summary>
        /// ["<c>BMX_DEDUCTION</c>"] BMX handling fee deduction
        /// </summary>
        [Map("BMX_DEDUCTION")]
        BmxDeduction,
    }
}
