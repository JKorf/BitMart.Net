using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Withdrawal address type
    /// </summary>
    public enum WithdrawalAddressType
    {
        /// <summary>
        /// Standard address
        /// </summary>
        [Map("0")]
        Standard,
        /// <summary>
        /// Universal address
        /// </summary>
        [Map("1")]
        Universal,
        /// <summary>
        /// EVM address
        /// </summary>
        [Map("2")]
        Evm
    }
}
