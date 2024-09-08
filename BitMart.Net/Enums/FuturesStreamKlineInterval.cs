using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Stream kline interval
    /// </summary>
    public enum FuturesStreamKlineInterval
    {
        /// <summary>
        /// One minute
        /// </summary>
        [Map("1m")]
        OneMinute = 60,
        /// <summary>
        /// Five minutes
        /// </summary>
        [Map("5m")]
        FiveMinutes = 60 * 5,
        /// <summary>
        /// Fifteen minutes
        /// </summary>
        [Map("15m")]
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// Thirty minutes
        /// </summary>
        [Map("30m")]
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// One hour
        /// </summary>
        [Map("1h")]
        OneHour = 60 * 60,
        /// <summary>
        /// Two hours
        /// </summary>
        [Map("2h")]
        TwoHours = 60 * 60 * 2,
        /// <summary>
        /// Four hours
        /// </summary>
        [Map("4h")]
        FourHours = 60 * 60 * 4,
        /// <summary>
        /// One day
        /// </summary>
        [Map("1d")]
        OneDay = 60 * 60 * 24,
        /// <summary>
        /// One week
        /// </summary>
        [Map("1w")]
        OneWeek = 60 * 60 * 24 * 7
    }
}
