using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Position side
    /// </summary>
    public enum PositionSide
    {
        /// <summary>
        /// Long
        /// </summary>
        [Map("1", "Long")]
        Long,
        /// <summary>
        /// Short
        /// </summary>
        [Map("2", "Short")]
        Short
    }
}
