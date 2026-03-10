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
        /// ["<c>spot</c>"] Spot API service
        /// </summary>
        [Map("spot")]
        SpotApiService,
        /// <summary>
        /// ["<c>contract</c>"] Contract API service
        /// </summary>
        [Map("contract")]
        ContractApiService,
        /// <summary>
        /// ["<c>account</c>"] Account API service
        /// </summary>
        [Map("account")]
        AccountApiService,
    }
}
