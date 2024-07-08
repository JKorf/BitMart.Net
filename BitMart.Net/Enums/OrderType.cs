using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// Limit order
        /// </summary>
        [Map("limit")]
        Limit,
        /// <summary>
        /// Market order
        /// </summary>
        [Map("market")]
        Market,
        /// <summary>
        /// Limit maker order
        /// </summary>
        [Map("limit_maker")]
        LimitMaker,
        /// <summary>
        /// Immediate or cancel order
        /// </summary>
        [Map("ioc")]
        ImmediateOrCancel
    }
}
