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
        OneMinute = 60,
        /// <summary>
        /// Three minutes
        /// </summary>
        [Map("3")]
        ThreeMinutes = 60 * 3,
        /// <summary>
        /// Five minutes
        /// </summary>
        [Map("5")]
        FiveMinutes = 60 * 5,
        /// <summary>
        /// Fifteen minutes
        /// </summary>
        [Map("15")]
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// Thirty minutes
        /// </summary>
        [Map("30")]
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// One hour
        /// </summary>
        [Map("60")]
        OneHour = 60 * 60,
        /// <summary>
        /// Two hours
        /// </summary>
        [Map("120")]
        TwoHours = 60 * 60 * 2,
        /// <summary>
        /// Four hours
        /// </summary>
        [Map("240")]
        FourHours = 60 * 60 * 4,
        /// <summary>
        /// Six hours
        /// </summary>
        [Map("360")]
        SixHours = 60 * 60 * 6,
        /// <summary>
        /// Twelve hours
        /// </summary>
        [Map("720")]
        TwelveHours = 60 * 60 * 12,
        /// <summary>
        /// One day
        /// </summary>
        [Map("1440")]
        OneDay = 60 * 60 * 24,
        /// <summary>
        /// Three days
        /// </summary>
        [Map("4320")]
        ThreeDays = 60 * 60 * 24 * 3,
        /// <summary>
        /// One week
        /// </summary>
        [Map("10080")]
        OneWeek = 60 * 60 * 24 * 7 
    }
}
