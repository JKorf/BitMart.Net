using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// System status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SystemMaintenanceStatus>))]
    public enum SystemMaintenanceStatus
    {
        /// <summary>
        /// ["<c>0</c>"] Waiting for start
        /// </summary>
        [Map("0")]
        Pending,
        /// <summary>
        /// ["<c>1</c>"] Currently executing
        /// </summary>
        [Map("1")]
        Working,
        /// <summary>
        /// ["<c>2</c>"] Completed
        /// </summary>
        [Map("2")]
        Completed,
    }
}
