using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Self trade prevention mode
    /// </summary>
    public enum StpMode
    {
        /// <summary>
        /// Cancel maker order
        /// </summary>
        [Map("1")]
        CancelMaker,
        /// <summary>
        /// Cancel taker order
        /// </summary>
        [Map("2")]
        CancelTaker,
        /// <summary>
        /// Cancel both orders
        /// </summary>
        [Map("3")]
        CancelBoth
    }
}
