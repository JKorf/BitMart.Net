using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Order state filter
    /// </summary>
    public enum OrderQueryState
    {
        /// <summary>
        /// Active order
        /// </summary>
        [Map("open")]
        Open,
        /// <summary>
        /// Closed order
        /// </summary>
        [Map("history")]
        History
    }
}
