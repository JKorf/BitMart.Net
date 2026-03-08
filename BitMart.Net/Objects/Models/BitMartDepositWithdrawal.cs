using CryptoExchange.Net.Converters.SystemTextJson;
using BitMart.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartDepositWithdrawalWrapper
    {
        /// <summary>
        /// ["<c>record</c>"] Record
        /// </summary>
        [JsonPropertyName("record")]
        public BitMartDepositWithdrawal Record { get; set; } = null!;
    }


    [SerializationModel]
    internal record BitMartDepositWithdrawalHistoryWrapper
    {
        /// <summary>
        /// ["<c>records</c>"] Records
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
        /// ["<c>withdraw_id</c>"] Withdraw id
        /// </summary>
        [JsonPropertyName("withdraw_id")]
        public string? WithdrawId { get; set; }
        /// <summary>
        /// ["<c>deposit_id</c>"] Deposit id
        /// </summary>
        [JsonPropertyName("deposit_id")]
        public string? DepositId { get; set; }
        /// <summary>
        /// ["<c>operation_type</c>"] Operation type
        /// </summary>
        [JsonPropertyName("operation_type")]
        public OperationType OperationType { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>apply_time</c>"] Apply time
        /// </summary>
        [JsonPropertyName("apply_time")]
        public DateTime ApplyTime { get; set; }
        /// <summary>
        /// ["<c>arrival_amount</c>"] Arrival quantity
        /// </summary>
        [JsonPropertyName("arrival_amount")]
        public decimal ArrivalQuantity { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public DepositWithdrawalStatus Status { get; set; }
        /// <summary>
        /// ["<c>address</c>"] Address
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        /// <summary>
        /// ["<c>address_memo</c>"] Address memo
        /// </summary>
        [JsonPropertyName("address_memo")]
        public string? AddressMemo { get; set; }
        /// <summary>
        /// ["<c>tx_id</c>"] Transaction id
        /// </summary>
        [JsonPropertyName("tx_id")]
        public string TransactionId { get; set; } = string.Empty;
    }


}
