using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Futures order side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesSide>))]
    public enum FuturesSide
    {
        /// <summary>
        /// Buy open long
        /// </summary>
        [Map("1")]
        BuyOpenLong,
        /// <summary>
        /// Buy close short
        /// </summary>
        [Map("2")]
        BuyCloseShort,
        /// <summary>
        /// Sell close long
        /// </summary>
        [Map("3")]
        SellCloseLong,
        /// <summary>
        /// Sell oen short
        /// </summary>
        [Map("4")]
        SellOpenShort
    }
}
