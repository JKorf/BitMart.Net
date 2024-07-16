using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.RateLimiting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net
{
    /// <summary>
    /// BitMart exchange information and configuration
    /// </summary>
    public static class BitMartExchange
    {
        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "BitMart";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.bitmart.com";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://developer-pro.bitmart.com/#introduction"
            };

        /// <summary>
        /// Rate limiter configuration for the BitMart API
        /// </summary>
        public static BitMartRateLimiters RateLimiter { get; } = new BitMartRateLimiters();
    }

    /// <summary>
    /// Rate limiter configuration for the BitMart API
    /// </summary>
    public class BitMartRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal BitMartRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            BitMart = new RateLimitGate("BitMart IP");
            BitMart.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
        }


        internal IRateLimitGate BitMart { get; private set; }

    }
}
