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
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Position margin
        /// </summary>
        [JsonPropertyName("position_deposit")]
        public decimal PositionMargin { get; set; }
        /// <summary>
        /// Frozen balance
        /// </summary>
        [JsonPropertyName("frozen_balance")]
        public decimal FrozenBalance { get; set; }
        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("available_balance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// Equity
        /// </summary>
        [JsonPropertyName("equity")]
        public decimal Equity { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized")]
        public decimal UnrealizedPnl { get; set; }
    }


}
