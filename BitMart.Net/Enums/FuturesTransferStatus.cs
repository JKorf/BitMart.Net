using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Transfer status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesTransferStatus>))]
    public enum FuturesTransferStatus
    {
        /// <summary>
        /// Processing
        /// </summary>
        [Map("PROCESSING")]
        Processing,
        /// <summary>
        /// Finished
        /// </summary>
        [Map("FINISHED")]
        Finished,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("FAILED")]
        Failed
    }
}
