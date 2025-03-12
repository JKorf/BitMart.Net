using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Fee rate type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FeeRateType>))]
    public enum FeeRateType
    {
        /// <summary>
        /// Normal user
        /// </summary>
        [Map("0")]
        Normal,
        /// <summary>
        /// VIP user
        /// </summary>
        [Map("1")]
        Vip,
        /// <summary>
        /// Special VIP user
        /// </summary>
        [Map("2")]
        SpecialVip
    }
}
