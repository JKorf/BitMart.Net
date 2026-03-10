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
        /// ["<c>1</c>"] One minute
        /// </summary>
        [Map("1")]
        OneMinute = 60,
        /// <summary>
        /// ["<c>3</c>"] Three minutes
        /// </summary>
        [Map("3")]
        ThreeMinutes = 180,
        /// <summary>
        /// ["<c>5</c>"] Five minutes
        /// </summary>
        [Map("5")]
        FiveMinutes = 300,
        /// <summary>
        /// ["<c>15</c>"] Fifteen minutes
        /// </summary>
        [Map("15")]
        FifteenMinutes = 900,
        /// <summary>
        /// ["<c>30</c>"] Thirty minutes
        /// </summary>
        [Map("30")]
        ThirtyMinutes = 1800,
        /// <summary>
        /// ["<c>45</c>"] Fourty five minutes
        /// </summary>
        [Map("45")]
        FourtyFiveMinutes = 2700,
        /// <summary>
        /// ["<c>60</c>"] One hour
        /// </summary>
        [Map("60")]
        OneHour = 3600,
        /// <summary>
        /// ["<c>120</c>"] Two hours
        /// </summary>
        [Map("120")]
        TwoHours = 7200,
        /// <summary>
        /// ["<c>180</c>"] Three hours
        /// </summary>
        [Map("180")]
        ThreeHours = 10800,
        /// <summary>
        /// ["<c>240</c>"] Four hours
        /// </summary>
        [Map("240")]
        FourHours = 14400,
        /// <summary>
        /// ["<c>1440</c>"] One day
        /// </summary>
        [Map("1440")]
        OneDay = 86400,
        /// <summary>
        /// ["<c>10080</c>"] One week
        /// </summary>
        [Map("10080")]
        OneWeek = 604800,
        /// <summary>
        /// ["<c>43200</c>"] One month
        /// </summary>
        [Map("43200")]
        OneMonth = 2592000
    }
}
