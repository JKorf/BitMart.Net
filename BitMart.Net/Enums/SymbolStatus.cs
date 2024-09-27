using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Symbol status
    /// </summary>
    public enum SymbolStatus
    {
        /// <summary>
        /// Trading
        /// </summary>
        [Map("trading")]
        Trading,
        /// <summary>
        /// Pre-trade
        /// </summary>
        [Map("pre-trade")]
        PreTrade
    }
}
