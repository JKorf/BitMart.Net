using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitMart.Net.Objects.Models
{
    internal record BitMartSymbolBrackets
    {
        [JsonPropertyName("rules")]
        public BitMartSymbolBracket[] Rules { get; set; } = [];
    }

    /// <summary>
    /// Symbol leverage bracket info
    /// </summary>
    public record BitMartSymbolBracket
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        /// <summary>
        /// Bracket info
        /// </summary>
        [JsonPropertyName("brackets")]
        public BitMartLeverageBracket[] Brackets { get; set; } = [];
    }

    /// <summary>
    /// Leverage bracket
    /// </summary>
    public record BitMartLeverageBracket
    {
        /// <summary>
        /// Bracket
        /// </summary>
        [JsonPropertyName("bracket")]
        public int Bracket { get; set; }
        /// <summary>
        /// Initial leverage
        /// </summary>
        [JsonPropertyName("initial_leverage")]
        public decimal InitialLeverage { get; set; }
        /// <summary>
        /// Maximum notional value in this bracket
        /// </summary>
        [JsonPropertyName("notional_cap")]
        public decimal MaxNotionalValue { get; set; }
        /// <summary>
        /// Minimum notional value in this bracket
        /// </summary>
        [JsonPropertyName("notional_floor")]
        public decimal MinNotionalValue { get; set; }
        /// <summary>
        /// Maintenance margin ratio
        /// </summary>
        [JsonPropertyName("maint_margin_ratio")]
        public decimal MaintenanceMarginRatio { get; set; }
        /// <summary>
        /// Cumulative maintenance margin amount
        /// </summary>
        [JsonPropertyName("cum")]
        public decimal CumulativeMaintenanceMargin { get; set; }
    }
}
