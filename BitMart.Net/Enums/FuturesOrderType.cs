using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Futures order type
    /// </summary>
    public enum FuturesOrderType
    {
        /// <summary>
        /// Limit order
        /// </summary>
        [Map("limit")]
        Limit,
        /// <summary>
        /// Market order
        /// </summary>
        [Map("market")]
        Market,
        /// <summary>
        /// Liquidation
        /// </summary>
        [Map("liquidate")]
        Liquidation,
        /// <summary>
        /// Bankruptcy
        /// </summary>
        [Map("bankruptcy")]
        Bankruptcy,
        /// <summary>
        /// Auto deleverage
        /// </summary>
        [Map("adl")]
        AutoDeleverage,
        /// <summary>
        /// Trailing
        /// </summary>
        [Map("trailing")]
        Trailing
    }
}
