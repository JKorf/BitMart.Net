using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderSide>))]
    public enum OrderSide
    {
        /// <summary>
        /// ["<c>buy</c>"] Buy
        /// </summary>
        [Map("buy")]
        Buy,
        /// <summary>
        /// ["<c>sell</c>"] Sell
        /// </summary>
        [Map("sell")]
        Sell
    }
}
