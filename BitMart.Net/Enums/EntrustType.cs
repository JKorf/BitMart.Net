using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Entrust type
    /// </summary>
    public enum EntrustType
    {
        /// <summary>
        /// Normal (limit or market order)
        /// </summary>
        [Map("normal")]
        Normal,
        /// <summary>
        /// Limit maker
        /// </summary>
        [Map("limit_maker")]
        LimitMaker,
        /// <summary>
        /// Immediate or cancel
        /// </summary>
        [Map("ioc")]
        ImmediateOrCancel
    }
}
