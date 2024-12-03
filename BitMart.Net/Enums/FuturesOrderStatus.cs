using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Futures order status
    /// </summary>
    public enum FuturesOrderStatus
    {
        /// <summary>
        /// Approval
        /// </summary>
        [Map("status_approval", "1")]
        Approval,
        /// <summary>
        /// Check
        /// </summary>
        [Map("status_check", "2")]
        Check,
        /// <summary>
        /// Finish
        /// </summary>
        [Map("status_finish", "4")]
        Finish
    }
}
