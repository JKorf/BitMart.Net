using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Kline interval
    /// </summary>
    public enum KlineInterval
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
        OneMinutes,
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
        /// Fourty five minutes
        /// </summary>
        [Map("45")]
        FourtyFiveMinutes,
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
        /// Three hours
        /// </summary>
        [Map("180")]
        ThreeHours,
        /// <summary>
        /// Four hours
        /// </summary>
        [Map("240")]
        FourHours,
        /// <summary>
        /// One day
        /// </summary>
        [Map("1440")]
        OneDay,
        /// <summary>
        /// One week
        /// </summary>
        [Map("1440")]
        OneWeek,
        /// <summary>
        /// One month
        /// </summary>
        [Map("43200")]
        OneMonth
    }
}
