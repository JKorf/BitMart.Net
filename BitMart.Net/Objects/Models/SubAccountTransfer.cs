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
        /// ["<c>total</c>"] Total
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// ["<c>historyList</c>"] History list
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
        /// ["<c>fromAccount</c>"] From account
        /// </summary>
        [JsonPropertyName("fromAccount")]
        public string FromAccount { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fromWalletType</c>"] From wallet type
        /// </summary>
        [JsonPropertyName("fromWalletType")]
        public string FromWalletType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>toAccount</c>"] To account
        /// </summary>
        [JsonPropertyName("toAccount")]
        public string ToAccount { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>toWalletType</c>"] To wallet type
        /// </summary>
        [JsonPropertyName("toWalletType")]
        public string ToWalletType { get; set; } = string.Empty;
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
        /// ["<c>submissionTime</c>"] Submission time
        /// </summary>
        [JsonPropertyName("submissionTime")]
        public DateTime Timestamp { get; set; }
    }


}
