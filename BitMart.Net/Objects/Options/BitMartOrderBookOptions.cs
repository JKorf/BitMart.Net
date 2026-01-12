using CryptoExchange.Net.Objects.Options;
using System;

namespace BitMart.Net.Objects.Options
{
    /// <summary>
    /// Options for the BitMart SymbolOrderBook
    /// </summary>
    public class BitMartOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static BitMartOrderBookOptions Default { get; set; } = new BitMartOrderBookOptions();

        /// <summary>
        /// The top amount of results to keep in sync, 5, 20 or 50. If for example limit=10 is used, the order book will contain the 10 best bids and 10 best asks. Leaving this null will sync the full order book
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// After how much time we should consider the connection dropped if no data is received for this time after the initial subscriptions
        /// </summary>
        public TimeSpan? InitialDataTimeout { get; set; }

        internal BitMartOrderBookOptions Copy()
        {
            var result = Copy<BitMartOrderBookOptions>();
            result.Limit = Limit;
            result.InitialDataTimeout = InitialDataTimeout;
            return result;
        }
    }
}
