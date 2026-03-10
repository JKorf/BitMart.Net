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
        /// ["<c>0</c>"] Normal user
        /// </summary>
        [Map("0")]
        Normal,
        /// <summary>
        /// ["<c>1</c>"] VIP user
        /// </summary>
        [Map("1")]
        Vip,
        /// <summary>
        /// ["<c>2</c>"] Special VIP user
        /// </summary>
        [Map("2")]
        SpecialVip
    }
}
