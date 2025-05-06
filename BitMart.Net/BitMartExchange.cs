using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.RateLimiting;
using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.SharedApis;
using System.Text.Json.Serialization;
using BitMart.Net.Converters;
using CryptoExchange.Net.Converters;

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
        /// Display exchange name
        /// </summary>
        public static string DisplayName => "BitMart";

        /// <summary>
        /// Url to exchange image
        /// </summary>
        public static string ImageUrl { get; } = "https://raw.githubusercontent.com/JKorf/BitMart.Net/master/BitMart.Net/Icon/icon.png";

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
        /// Type of exchange
        /// </summary>
        public static ExchangeType Type { get; } = ExchangeType.CEX;

        internal static JsonSerializerContext _serializerContext = JsonSerializerContextCache.GetOrCreate<BitMartSourceGenerationContext>();

        /// <summary>
        /// Format a base and quote asset to a BitMart recognized symbol 
        /// </summary>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="tradingMode">Trading mode</param>
        /// <param name="deliverTime">Delivery time for delivery futures</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        {
            if (tradingMode == TradingMode.Spot)
                return baseAsset.ToUpperInvariant() + "_" + quoteAsset.ToUpperInvariant();

            return baseAsset.ToUpperInvariant() + quoteAsset.ToUpperInvariant();
        }

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

        /// <summary>
        /// Event when the rate limit is updated. Note that it's only updated when a request is send, so there are no specific updates when the current usage is decaying.
        /// </summary>
        public event Action<RateLimitUpdateEvent> RateLimitUpdated;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal BitMartRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            BitMart = new RateLimitGate("BitMart IP");
            SocketLimits = new RateLimitGate("Socket limits")
                .AddGuard(new RateLimitGuard(RateLimitGuard.PerHost, new LimitItemTypeFilter(RateLimitItemType.Connection), 100, TimeSpan.FromSeconds(60), RateLimitWindowType.Sliding, connectionWeight: 1));
            BitMart.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            BitMart.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
            SocketLimits.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
            SocketLimits.RateLimitUpdated += (x) => RateLimitUpdated?.Invoke(x);
        }


        internal IRateLimitGate BitMart { get; private set; }
        internal IRateLimitGate SocketLimits { get; private set; }

    }
}
