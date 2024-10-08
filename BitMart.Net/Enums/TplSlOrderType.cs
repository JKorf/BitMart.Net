using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// TakeProfit/StopLoss order type
    /// </summary>
    public enum TplSlOrderType
    {
        /// <summary>
        /// Take profit order
        /// </summary>
        [Map("take_profit")]
        TakeProfit,
        /// <summary>
        /// Stop loss order
        /// </summary>
        [Map("stop_loss")]
        StopLoss
    }
}
