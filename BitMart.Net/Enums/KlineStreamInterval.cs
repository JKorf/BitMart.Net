using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// kline interval
    /// </summary>
    public enum KlineStreamInterval
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
        [Map("1H")]
        OneHour,
        /// <summary>
        /// Two hours
        /// </summary>
        [Map("2H")]
        TwoHours,
        /// <summary>
        /// Four hours
        /// </summary>
        [Map("4H")]
        FourHours,
        /// <summary>
        /// One day
        /// </summary>
        [Map("1D")]
        OneDay,
        /// <summary>
        /// One week
        /// </summary>
        [Map("1W")]
        OneWeek,
        /// <summary>
        /// One month
        /// </summary>
        [Map("1M")]
        OneMonth
    }
}
