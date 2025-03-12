using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Spot mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SpotMode>))]
    public enum SpotMode
    {
        /// <summary>
        /// Spot
        /// </summary>
        [Map("spot")]
        Spot,
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("iso_margin")]
        IsolatedMargin
    }
}
