using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Flow type
    /// </summary>
    public enum FlowType
    {
        /// <summary>
        /// All
        /// </summary>
        [Map("0")]
        All,
        /// <summary>
        /// Transfer
        /// </summary>
        [Map("1")]
        Transfer,
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [Map("2")]
        RealizedPnl,
        /// <summary>
        /// Funding fee
        /// </summary>
        [Map("3")]
        FundingFee,
        /// <summary>
        /// Commission fee
        /// </summary>
        [Map("4")]
        CommissionFee,
        /// <summary>
        /// Liquidation clearance
        /// </summary>
        [Map("5")]
        LiquidationClearance
    }
}
