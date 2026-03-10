using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderType>))]
    public enum OrderType
    {
        /// <summary>
        /// ["<c>limit</c>"] Limit order
        /// </summary>
        [Map("limit")]
        Limit,
        /// <summary>
        /// ["<c>market</c>"] Market order
        /// </summary>
        [Map("market")]
        Market,
        /// <summary>
        /// ["<c>limit_maker</c>"] Limit maker order
        /// </summary>
        [Map("limit_maker")]
        LimitMaker,
        /// <summary>
        /// ["<c>ioc</c>"] Immediate or cancel order
        /// </summary>
        [Map("ioc")]
        ImmediateOrCancel
    }
}
