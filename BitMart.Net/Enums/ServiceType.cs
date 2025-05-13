using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// System type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ServiceType>))]
    public enum ServiceType
    {
        /// <summary>
        /// Spot API service
        /// </summary>
        [Map("spot")]
        SpotApiService,
        /// <summary>
        /// Contract API service
        /// </summary>
        [Map("contract")]
        ContractApiService,
        /// <summary>
        /// Account API service
        /// </summary>
        [Map("account")]
        AccountApiService,
    }
}
