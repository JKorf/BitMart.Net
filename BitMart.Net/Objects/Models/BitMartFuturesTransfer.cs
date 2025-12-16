using CryptoExchange.Net.Converters.SystemTextJson;
using BitMart.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartFuturesTransferWrapper
    {
        /// <summary>
        /// Records
        /// </summary>
        [JsonPropertyName("records")]
        public BitMartFuturesTransfer[] Records { get; set; } = Array.Empty<BitMartFuturesTransfer>();
    }

    /// <summary>
    /// Futures transfer record
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesTransfer
    {
        /// <summary>
        /// Transfer id
        /// </summary>
        [JsonPropertyName("transfer_id")]
        public string TransferId { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        public FuturesTransferType? Type { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("state")]
        public FuturesTransferStatus Status { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }


}
