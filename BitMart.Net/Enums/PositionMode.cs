using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionMode>))]
    public enum PositionMode
    {
        /// <summary>
        /// ["<c>hedge_mode</c>"] Hedge mode
        /// </summary>
        [Map("hedge_mode")]
        HedgeMode,
        /// <summary>
        /// ["<c>one_way_mode</c>"] One way mode
        /// </summary>
        [Map("one_way_mode")]
        OneWayMode
    }
}
