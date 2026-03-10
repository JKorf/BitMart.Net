using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Transfer direction
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferDirection>))]
    public enum TransferDirection
    {
        /// <summary>
        /// ["<c>in</c>"] Transfer in
        /// </summary>
        [Map("in")]
        TransferIn,
        /// <summary>
        /// ["<c>out</c>"] Transfer out
        /// </summary>
        [Map("out")]
        TransferOut
    }
}
