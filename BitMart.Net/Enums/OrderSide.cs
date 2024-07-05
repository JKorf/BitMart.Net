using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    public enum OrderSide
    {
        /// <summary>
        /// Buy
        /// </summary>
        [Map("buy")]
        Buy,
        /// <summary>
        /// Sell
        /// </summary>
        [Map("sell")]
        Sell
    }
}
