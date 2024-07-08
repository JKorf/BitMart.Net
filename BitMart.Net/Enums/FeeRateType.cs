using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Fee rate type
    /// </summary>
    public enum FeeRateType
    {
        /// <summary>
        /// Normal user
        /// </summary>
        [Map("0")]
        Normal,
        /// <summary>
        /// VIP user
        /// </summary>
        [Map("1")]
        Vip,
        /// <summary>
        /// Special VIP user
        /// </summary>
        [Map("2")]
        SpecialVip
    }
}
