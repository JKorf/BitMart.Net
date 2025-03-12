using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Kline interval
    /// </summary>
    [JsonConverter(typeof(EnumConverter<KlineInterval>))]
    public enum KlineInterval
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
        ThreeMinutes = 180,
        /// <summary>
        /// Five minutes
        /// </summary>
        [Map("5")]
        FiveMinutes = 300,
        /// <summary>
        /// Fifteen minutes
        /// </summary>
        [Map("15")]
        FifteenMinutes = 900,
        /// <summary>
        /// Thirty minutes
        /// </summary>
        [Map("30")]
        ThirtyMinutes = 1800,
        /// <summary>
        /// Fourty five minutes
        /// </summary>
        [Map("45")]
        FourtyFiveMinutes = 2700,
        /// <summary>
        /// One hour
        /// </summary>
        [Map("60")]
        OneHour = 3600,
        /// <summary>
        /// Two hours
        /// </summary>
        [Map("120")]
        TwoHours = 7200,
        /// <summary>
        /// Three hours
        /// </summary>
        [Map("180")]
        ThreeHours = 10800,
        /// <summary>
        /// Four hours
        /// </summary>
        [Map("240")]
        FourHours = 14400,
        /// <summary>
        /// One day
        /// </summary>
        [Map("1440")]
        OneDay = 86400,
        /// <summary>
        /// One week
        /// </summary>
        [Map("10080")]
        OneWeek = 604800,
        /// <summary>
        /// One month
        /// </summary>
        [Map("43200")]
        OneMonth = 2592000
    }
}
