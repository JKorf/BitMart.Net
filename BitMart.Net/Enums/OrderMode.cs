using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Order mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderMode>))]
    public enum OrderMode
    {
        /// <summary>
        /// Good till canceled
        /// </summary>
        [Map("1")]
        GoodTilCancel,
        /// <summary>
        /// Fill entirely or cancel
        /// </summary>
        [Map("2")]
        FillOrKill,
        /// <summary>
        /// Fill at least partially or cancel
        /// </summary>
        [Map("3")]
        ImmediateOrCancel,
        /// <summary>
        /// Post only
        /// </summary>
        [Map("4")]
        PostOnly
    }
}
