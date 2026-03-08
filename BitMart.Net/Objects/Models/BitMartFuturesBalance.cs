using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartFuturesBalanceWrapper 
    { 
        [JsonPropertyName("list")]
        public BitMartFuturesBalance[] List { get; set; } = Array.Empty<BitMartFuturesBalance>();
    }

    /// <summary>
    /// Futures account balance
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesBalance
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>position_deposit</c>"] Position margin
        /// </summary>
        [JsonPropertyName("position_deposit")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// ["<c>frozen_balance</c>"] Frozen balance
        /// </summary>
        [JsonPropertyName("frozen_balance")]
        public decimal FrozenBalance { get; set; }
        /// <summary>
        /// ["<c>available_balance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("available_balance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// ["<c>equity</c>"] Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal Equity { get; set; }
        /// <summary>
        /// ["<c>unrealized</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized")]
        public decimal UnrealizedPnl { get; set; }
    }


}
