using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Price direction
    /// </summary>
    public enum PriceDirection
    {
        /// <summary>
        /// Long direction
        /// </summary>
        [Map("price_way_long")]
        LongDirection,
        /// <summary>
        /// Short direction
        /// </summary>
        [Map("price_way_short")]
        ShortDirection
    }
}
