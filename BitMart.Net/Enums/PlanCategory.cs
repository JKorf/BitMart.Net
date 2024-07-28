using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Trigger order category
    /// </summary>
    public enum PlanCategory
    {
        /// <summary>
        /// Take profit / Stop loss
        /// </summary>
        [Map("1")]
        TpSl,
        /// <summary>
        /// Position Take profit / Stop loss
        /// </summary>
        [Map("2")]
        PositionTpSl
    }
}
