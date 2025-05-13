using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Margin type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginType>))]
    public enum MarginType
    {
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("cross", "Cross")]
        CrossMargin,
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("isolated", "Isolated")]
        IsolatedMargin
    }
}
