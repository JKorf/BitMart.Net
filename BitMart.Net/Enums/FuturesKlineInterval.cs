using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Kline interval
    /// </summary>
    public enum FuturesKlineInterval
    {
        /// <summary>
        /// One minute
        /// </summary>
        [Map("1")]
        OneMinute,
        /// <summary>
        /// Three minutes
        /// </summary>
        [Map("3")]
        ThreeMinutes,
        /// <summary>
        /// Five minutes
        /// </summary>
        [Map("5")]
        FiveMinutes,
        /// <summary>
        /// Fifteen minutes
        /// </summary>
        [Map("15")]
        FifteenMinutes,
        /// <summary>
        /// Thirty minutes
        /// </summary>
        [Map("30")]
        ThirtyMinutes,
        /// <summary>
        /// One hour
        /// </summary>
        [Map("60")]
        OneHour,
        /// <summary>
        /// Two hours
        /// </summary>
        [Map("120")]
        TwoHours,
        /// <summary>
        /// Four hours
        /// </summary>
        [Map("240")]
        FourHours,
        /// <summary>
        /// Six hours
        /// </summary>
        [Map("360")]
        SixHours,
        /// <summary>
        /// Twelve hours
        /// </summary>
        [Map("720")]
        TwelveHours,
        /// <summary>
        /// One day
        /// </summary>
        [Map("1440")]
        OneDay,
        /// <summary>
        /// Three days
        /// </summary>
        [Map("4320")]
        ThreeDays,
        /// <summary>
        /// One week
        /// </summary>
        [Map("10080")]
        OneWeek
    }
}
