using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Trigger plan type
    /// </summary>
    public enum TriggerPlanType
    {
        /// <summary>
        /// Plan
        /// </summary>
        [Map("plan")]
        Plan,
        /// <summary>
        /// Profit loss
        /// </summary>
        [Map("profit_loss")]
        ProfitLoss
    }
}
