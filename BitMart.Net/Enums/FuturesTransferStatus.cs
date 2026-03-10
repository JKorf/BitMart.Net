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
        /// ["<c>PROCESSING</c>"] Processing
        /// </summary>
        [Map("PROCESSING")]
        Processing,
        /// <summary>
        /// ["<c>FINISHED</c>"] Finished
        /// </summary>
        [Map("FINISHED")]
        Finished,
        /// <summary>
        /// ["<c>FAILED</c>"] Failed
        /// </summary>
        [Map("FAILED")]
        Failed
    }
}
