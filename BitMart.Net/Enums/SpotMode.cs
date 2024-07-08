using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Spot mode
    /// </summary>
    public enum SpotMode
    {
        /// <summary>
        /// Spot
        /// </summary>
        [Map("spot")]
        Spot,
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("iso_margin")]
        IsolatedMargin
    }
}
