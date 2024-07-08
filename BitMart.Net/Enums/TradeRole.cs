using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Trade role
    /// </summary>
    public enum TradeRole
    {
        /// <summary>
        /// Maker
        /// </summary>
        [Map("maker")]
        Maker,
        /// <summary>
        /// Taker
        /// </summary>
        [Map("taker")]
        Taker
    }
}
