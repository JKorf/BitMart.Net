using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Symbol trading fee
    /// </summary>
    [SerializationModel]
    public record BitMartSymbolTradeFee
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Buy taker fee rate
        /// </summary>
        [JsonPropertyName("buy_taker_fee_rate")]
        public decimal BuyTakerFeeRate { get; set; }
        /// <summary>
        /// Sell taker fee rate
        /// </summary>
        [JsonPropertyName("sell_taker_fee_rate")]
        public decimal SellTakerFeeRate { get; set; }
        /// <summary>
        /// Buy maker fee rate
        /// </summary>
        [JsonPropertyName("buy_maker_fee_rate")]
        public decimal BuyMakerFeeRate { get; set; }
        /// <summary>
        /// Sell maker fee rate
        /// </summary>
        [JsonPropertyName("sell_maker_fee_rate")]
        public decimal SellMakerFeeRate { get; set; }
    }


}
