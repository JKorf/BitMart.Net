using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Order mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderMode>))]
    public enum OrderMode
    {
        /// <summary>
        /// ["<c>1</c>"] Good till canceled
        /// </summary>
        [Map("1")]
        GoodTilCancel,
        /// <summary>
        /// ["<c>2</c>"] Fill entirely or cancel
        /// </summary>
        [Map("2")]
        FillOrKill,
        /// <summary>
        /// ["<c>3</c>"] Fill at least partially or cancel
        /// </summary>
        [Map("3")]
        ImmediateOrCancel,
        /// <summary>
        /// ["<c>4</c>"] Post only
        /// </summary>
        [Map("4")]
        PostOnly
    }
}
