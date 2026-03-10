using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// kline interval
    /// </summary>
    [JsonConverter(typeof(EnumConverter<KlineStreamInterval>))]
    public enum KlineStreamInterval
    {
        /// <summary>
        /// ["<c>1m</c>"] One minute
        /// </summary>
        [Map("1m")]
        OneMinute = 60,
        /// <summary>
        /// ["<c>5m</c>"] Five minutes
        /// </summary>
        [Map("5m")]
        FiveMinutes = 60 * 5,
        /// <summary>
        /// ["<c>15m</c>"] Fifteen minutes
        /// </summary>
        [Map("15m")]
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// ["<c>30m</c>"] Thirty minutes
        /// </summary>
        [Map("30m")]
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// ["<c>1H</c>"] One hour
        /// </summary>
        [Map("1H")]
        OneHour = 60 * 60,
        /// <summary>
        /// ["<c>2H</c>"] Two hours
        /// </summary>
        [Map("2H")]
        TwoHours = 60 * 60 * 2,
        /// <summary>
        /// ["<c>4H</c>"] Four hours
        /// </summary>
        [Map("4H")]
        FourHours = 60 * 60 * 4,
        /// <summary>
        /// ["<c>1D</c>"] One day
        /// </summary>
        [Map("1D")]
        OneDay = 60 * 60 * 24,
        /// <summary>
        /// ["<c>1W</c>"] One week
        /// </summary>
        [Map("1W")]
        OneWeek = 60 * 60 * 24 * 7,
        /// <summary>
        /// ["<c>1M</c>"] One month
        /// </summary>
        [Map("1M")]
        OneMonth = 60 * 60 * 24 * 30
    }
}
