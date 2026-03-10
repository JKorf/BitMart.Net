using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Futures order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesOrderStatus>))]
    public enum FuturesOrderStatus
    {
        /// <summary>
        /// ["<c>status_approval</c>"] Approval
        /// </summary>
        [Map("status_approval", "1")]
        Approval,
        /// <summary>
        /// ["<c>status_check</c>"] Check
        /// </summary>
        [Map("status_check", "2")]
        Check,
        /// <summary>
        /// ["<c>status_finish</c>"] Finish
        /// </summary>
        [Map("status_finish", "4")]
        Finish
    }
}
