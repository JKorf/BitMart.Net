using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Sub account status
    /// </summary>
    public enum SubAccountStatus
    {
        /// <summary>
        /// Disabled in background
        /// </summary>
        [Map("0")]
        Disabled,
        /// <summary>
        /// Normal
        /// </summary>
        [Map("1")]
        Normal,
        /// <summary>
        /// Frozen by main account
        /// </summary>
        [Map("2")]
        FrozenByMainAccount
    }
}
