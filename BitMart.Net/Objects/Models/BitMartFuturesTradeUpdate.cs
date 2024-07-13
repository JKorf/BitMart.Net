using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using BitMart.Net.Enums;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Trade update
    /// </summary>
    public record BitMartFuturesTradeUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("deal_price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("deal_vol")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public long TradeId { get; set; }
    }
}
