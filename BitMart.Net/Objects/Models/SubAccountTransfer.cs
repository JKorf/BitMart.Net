using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Transfer history
    /// </summary>
    [SerializationModel]
    public record SubAccountTransferHistory
    {
        /// <summary>
        /// Total
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// History list
        /// </summary>
        [JsonPropertyName("historyList")]
        public SubAccountTransfer[] HistoryList { get; set; } = Array.Empty<SubAccountTransfer>();
    }

    /// <summary>
    /// Transfer info
    /// </summary>
    [SerializationModel]
    public record SubAccountTransfer
    {
        /// <summary>
        /// From account
        /// </summary>
        [JsonPropertyName("fromAccount")]
        public string FromAccount { get; set; } = string.Empty;
        /// <summary>
        /// From wallet type
        /// </summary>
        [JsonPropertyName("fromWalletType")]
        public string FromWalletType { get; set; } = string.Empty;
        /// <summary>
        /// To account
        /// </summary>
        [JsonPropertyName("toAccount")]
        public string ToAccount { get; set; } = string.Empty;
        /// <summary>
        /// To wallet type
        /// </summary>
        [JsonPropertyName("toWalletType")]
        public string ToWalletType { get; set; } = string.Empty;
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
        /// Submission time
        /// </summary>
        [JsonPropertyName("submissionTime")]
        public DateTime Timestamp { get; set; }
    }


}
