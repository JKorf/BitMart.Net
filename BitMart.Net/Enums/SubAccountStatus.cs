using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Sub account status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SubAccountStatus>))]
    public enum SubAccountStatus
    {
        /// <summary>
        /// Disabled in background
        /// </summary>
        [Map("0")]
        Disabled,
        /// <summary>
        /// Normal
        /// </summary>
        [Map("1")]
        Normal,
        /// <summary>
        /// Frozen by main account
        /// </summary>
        [Map("2")]
        FrozenByMainAccount
    }
}
