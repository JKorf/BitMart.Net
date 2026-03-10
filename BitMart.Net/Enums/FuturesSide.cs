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
        /// ["<c>1</c>"] Buy open long
        /// </summary>
        [Map("1")]
        BuyOpenLong,
        /// <summary>
        /// ["<c>2</c>"] Buy close short
        /// </summary>
        [Map("2")]
        BuyCloseShort,
        /// <summary>
        /// ["<c>3</c>"] Sell close long
        /// </summary>
        [Map("3")]
        SellCloseLong,
        /// <summary>
        /// ["<c>4</c>"] Sell open short
        /// </summary>
        [Map("4")]
        SellOpenShort
    }
}
