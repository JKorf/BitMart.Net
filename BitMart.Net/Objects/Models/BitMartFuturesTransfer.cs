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
        /// ["<c>records</c>"] Records
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
        /// ["<c>transfer_id</c>"] Transfer id
        /// </summary>
        [JsonPropertyName("transfer_id")]
        public string TransferId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Type
        /// </summary>
        [JsonPropertyName("type")]
        public FuturesTransferType? Type { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Status
        /// </summary>
        [JsonPropertyName("state")]
        public FuturesTransferStatus Status { get; set; }
        /// <summary>
        /// ["<c>timestamp</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
    }


}
