using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Balance update type
    /// </summary>
    public enum BalanceUpdateType
    {
        /// <summary>
        /// Trade
        /// </summary>
        [Map("TRANSACTION_COMPLETED")]
        Trade,
        /// <summary>
        /// Deposit
        /// </summary>
        [Map("ACCOUNT_RECHARGE")]
        Deposit,
        /// <summary>
        /// Withdrawal
        /// </summary>
        [Map("ACCOUNT_WITHDRAWAL")]
        Withdrawal,
        /// <summary>
        /// Transfer
        /// </summary>
        [Map("ACCOUNT_TRANSFER")]
        Transfer,
        /// <summary>
        /// BMX handling fee deduction
        /// </summary>
        [Map("BMX_DEDUCTION")]
        BmxDeduction,
    }
}
