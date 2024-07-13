using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Side of the book
    /// </summary>
    public enum OrderBookSide
    {
        /// <summary>
        /// Bids
        /// </summary>
        [Map("1")]
        Bids,
        /// <summary>
        /// Asks
        /// </summary>
        [Map("2")]
        Asks
    }
}
