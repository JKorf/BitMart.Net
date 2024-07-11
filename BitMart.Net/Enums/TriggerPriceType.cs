using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Trigger price type
    /// </summary>
    public enum TriggerPriceType
    {
        /// <summary>
        /// Last price
        /// </summary>
        [Map("1")]
        LastPrice,
        /// <summary>
        /// Fair price
        /// </summary>
        [Map("2")]
        FairPrice
    }
}
