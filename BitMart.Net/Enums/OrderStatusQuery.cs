using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Order status query filter
    /// </summary>
    public enum OrderStatusQuery
    {
        /// <summary>
        /// All orders
        /// </summary>
        [Map("all")]
        All,
        /// <summary>
        /// Partially filled
        /// </summary>
        [Map("partially_filled")]
        PartiallyFilled
    }
}
