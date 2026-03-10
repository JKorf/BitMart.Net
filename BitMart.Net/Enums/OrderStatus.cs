using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatus>))]
    public enum OrderStatus
    {
        /// <summary>
        /// ["<c>new</c>"] New
        /// </summary>
        [Map("new")]
        New,
        /// <summary>
        /// ["<c>partially_filled</c>"] Partially filled
        /// </summary>
        [Map("partially_filled")]
        PartiallyFilled,
        /// <summary>
        /// ["<c>filled</c>"] Filled
        /// </summary>
        [Map("filled")]
        Filled,
        /// <summary>
        /// ["<c>canceled</c>"] Canceled
        /// </summary>
        [Map("canceled")]
        Canceled,
        /// <summary>
        /// ["<c>partially_canceled</c>"] Partially canceled
        /// </summary>
        [Map("partially_canceled")]
        PartiallyCanceled,
        /// <summary>
        /// ["<c>failed</c>"] Order failed
        /// </summary>
        [Map("failed")]
        Failed

    }
}
