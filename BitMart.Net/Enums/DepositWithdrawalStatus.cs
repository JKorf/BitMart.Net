using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Deposit/Withdrawal status
    /// </summary>
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
        /// Processsing
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
