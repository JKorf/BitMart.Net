using CryptoExchange.Net.Converters.SystemTextJson;
using BitMart.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartDepositWithdrawalWrapper
    {
        /// <summary>
        /// Record
        /// </summary>
        [JsonPropertyName("record")]
        public BitMartDepositWithdrawal Record { get; set; } = null!;
    }


    [SerializationModel]
    internal record BitMartDepositWithdrawalHistoryWrapper
    {
        /// <summary>
        /// Records
        /// </summary>
        [JsonPropertyName("records")]
        public BitMartDepositWithdrawal[] Records { get; set; } = Array.Empty<BitMartDepositWithdrawal>();
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record BitMartDepositWithdrawal
    {
        /// <summary>
        /// Withdraw id
        /// </summary>
        [JsonPropertyName("withdraw_id")]
        public string? WithdrawId { get; set; }
        /// <summary>
        /// Deposit id
        /// </summary>
        [JsonPropertyName("deposit_id")]
        public string? DepositId { get; set; }
        /// <summary>
        /// Operation type
        /// </summary>
        [JsonPropertyName("operation_type")]
        public OperationType OperationType { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Apply time
        /// </summary>
        [JsonPropertyName("apply_time")]
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// Arrival quantity
        /// </summary>
        [JsonPropertyName("arrival_amount")]
        public decimal ArrivalQuantity { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public DepositWithdrawalStatus Status { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        /// <summary>
        /// Address memo
        /// </summary>
        [JsonPropertyName("address_memo")]
        public string? AddressMemo { get; set; }
        /// <summary>
        /// Transaction id
        /// </summary>
        [JsonPropertyName("tx_id")]
        public string TransactionId { get; set; } = string.Empty;
    }


}
