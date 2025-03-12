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
        /// Long direction
        /// </summary>
        [Map("price_way_long")]
        LongDirection,
        /// <summary>
        /// Short direction
        /// </summary>
        [Map("price_way_short")]
        ShortDirection
    }
}
