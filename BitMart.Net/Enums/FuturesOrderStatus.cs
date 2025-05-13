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
        /// Approval
        /// </summary>
        [Map("status_approval", "1")]
        Approval,
        /// <summary>
        /// Check
        /// </summary>
        [Map("status_check", "2")]
        Check,
        /// <summary>
        /// Finish
        /// </summary>
        [Map("status_finish", "4")]
        Finish
    }
}
