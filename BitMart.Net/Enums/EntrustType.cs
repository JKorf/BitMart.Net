using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Entrust type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<EntrustType>))]
    public enum EntrustType
    {
        /// <summary>
        /// Normal (limit or market order)
        /// </summary>
        [Map("normal")]
        Normal,
        /// <summary>
        /// Limit maker
        /// </summary>
        [Map("limit_maker")]
        LimitMaker,
        /// <summary>
        /// Immediate or cancel
        /// </summary>
        [Map("ioc")]
        ImmediateOrCancel
    }
}
