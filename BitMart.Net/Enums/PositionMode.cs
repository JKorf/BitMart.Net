using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Position mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionMode>))]
    public enum PositionMode
    {
        /// <summary>
        /// Hedge mode
        /// </summary>
        [Map("hedge_mode")]
        HedgeMode,
        /// <summary>
        /// One way mode
        /// </summary>
        [Map("one_way_mode")]
        OneWayMode
    }
}
