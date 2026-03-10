using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Trigger price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerPriceType>))]
    public enum TriggerPriceType
    {
        /// <summary>
        /// ["<c>1</c>"] Last price
        /// </summary>
        [Map("1")]
        LastPrice,
        /// <summary>
        /// ["<c>2</c>"] Fair price
        /// </summary>
        [Map("2")]
        FairPrice
    }
}
