using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Transfer status
    /// </summary>
    public enum FuturesTransferStatus
    {
        /// <summary>
        /// Processing
        /// </summary>
        [Map("PROCESSING")]
        Processing,
        /// <summary>
        /// Finished
        /// </summary>
        [Map("FINISHED")]
        Finished,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("FAILED")]
        Failed
    }
}
