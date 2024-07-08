using BitMart.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// User fee rate
    /// </summary>
    public record BitMartFeeRate
    {
        /// <summary>
        /// User fee rate type
        /// </summary>
        [JsonPropertyName("user_rate_type")]
        public FeeRateType? FeeRateType { get; set; }
        /// <summary>
        /// Level
        /// </summary>
        [JsonPropertyName("level")]
        public string Level { get; set; } = string.Empty;
        /// <summary>
        /// Taker fee rate for Class-A pairs
        /// </summary>
        [JsonPropertyName("taker_fee_rate_A")]
        public decimal TakerFeeRateA { get; set; }
        /// <summary>
        /// Maker fee rate for Class-A pairs
        /// </summary>
        [JsonPropertyName("maker_fee_rate_A")]
        public decimal MakerFeeRateA { get; set; }
        /// <summary>
        /// Taker fee rate for Class-B pairs
        /// </summary>
        [JsonPropertyName("taker_fee_rate_B")]
        public decimal TakerFeeRateB { get; set; }
        /// <summary>
        /// Maker fee rate for Class-B pairs
        /// </summary>
        [JsonPropertyName("maker_fee_rate_B")]
        public decimal MakerFeeRateB { get; set; }
        /// <summary>
        /// Taker fee rate for Class-C pairs
        /// </summary>
        [JsonPropertyName("taker_fee_rate_C")]
        public decimal TakerFeeRateC { get; set; }
        /// <summary>
        /// Maker fee rate for Class-C pairs
        /// </summary>
        [JsonPropertyName("maker_fee_rate_C")]
        public decimal MakerFeeRateC { get; set; }
    }


}
