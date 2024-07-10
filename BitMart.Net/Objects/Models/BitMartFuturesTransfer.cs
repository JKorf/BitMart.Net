using BitMart.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    internal record BitMartFuturesTransferWrapper
    {
        /// <summary>
        /// Records
        /// </summary>
        [JsonPropertyName("records")]
        public IEnumerable<BitMartFuturesTransfer> Records { get; set; } = Array.Empty<BitMartFuturesTransfer>();
    }

    /// <summary>
    /// Futures transfer record
    /// </summary>
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
