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
        OneMinute,
        /// <summary>
        /// Five minutes
        /// </summary>
        [Map("5m")]
        FiveMinutes,
        /// <summary>
        /// Fifteen minutes
        /// </summary>
        [Map("15m")]
        FifteenMinutes,
        /// <summary>
        /// Thirty minutes
        /// </summary>
        [Map("30m")]
        ThirtyMinutes,
        /// <summary>
        /// One hour
        /// </summary>
        [Map("1h")]
        OneHour,
        /// <summary>
        /// Two hours
        /// </summary>
        [Map("2h")]
        TwoHours,
        /// <summary>
        /// Four hours
        /// </summary>
        [Map("4h")]
        FourHours,
        /// <summary>
        /// One day
        /// </summary>
        [Map("1d")]
        OneDay,
        /// <summary>
        /// One week
        /// </summary>
        [Map("1w")]
        OneWeek
    }
}
