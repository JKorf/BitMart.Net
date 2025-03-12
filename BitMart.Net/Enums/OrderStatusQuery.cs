using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Order status query filter
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatusQuery>))]
    public enum OrderStatusQuery
    {
        /// <summary>
        /// All orders
        /// </summary>
        [Map("all")]
        All,
        /// <summary>
        /// Partially filled
        /// </summary>
        [Map("partially_filled")]
        PartiallyFilled
    }
}
