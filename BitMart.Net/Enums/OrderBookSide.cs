using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Side of the book
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderBookSide>))]
    public enum OrderBookSide
    {
        /// <summary>
        /// Bids
        /// </summary>
        [Map("1")]
        Bids,
        /// <summary>
        /// Asks
        /// </summary>
        [Map("2")]
        Asks
    }
}
