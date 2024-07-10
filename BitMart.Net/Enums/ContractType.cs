using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Contract type
    /// </summary>
    public enum ContractType
    {
        /// <summary>
        /// Perpetual contract
        /// </summary>
        [Map("1")]
        Perpetual,
        /// <summary>
        /// Futures contract
        /// </summary>
        [Map("2")]
        Futures
    }
}
