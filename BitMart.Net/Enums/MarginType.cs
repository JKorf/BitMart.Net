using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Margin type
    /// </summary>
    public enum MarginType
    {
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("cross", "Cross")]
        CrossMargin,
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("isolated", "Isolated")]
        IsolatedMargin
    }
}
