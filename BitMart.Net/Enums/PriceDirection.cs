using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Price direction
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PriceDirection>))]
    public enum PriceDirection
    {
        /// <summary>
        /// ["<c>price_way_long</c>"] Long direction
        /// </summary>
        [Map("price_way_long")]
        LongDirection,
        /// <summary>
        /// ["<c>price_way_short</c>"] Short direction
        /// </summary>
        [Map("price_way_short")]
        ShortDirection
    }
}
