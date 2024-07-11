using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Order mode
    /// </summary>
    public enum OrderMode
    {
        /// <summary>
        /// Good till canceled
        /// </summary>
        [Map("1")]
        GoodTilCancel,
        /// <summary>
        /// Fill entirely or cancel
        /// </summary>
        [Map("2")]
        FillOrKill,
        /// <summary>
        /// Fill at least partially or cancel
        /// </summary>
        [Map("3")]
        ImmediateOrCancel,
        /// <summary>
        /// Post only
        /// </summary>
        [Map("4")]
        PostOnly
    }
}
