using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Transfer direction
    /// </summary>
    public enum TransferDirection
    {
        /// <summary>
        /// Transfer in
        /// </summary>
        [Map("in")]
        TransferIn,
        /// <summary>
        /// Transfer out
        /// </summary>
        [Map("out")]
        TransferOut
    }
}
