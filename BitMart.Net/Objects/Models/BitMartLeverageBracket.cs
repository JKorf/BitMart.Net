using System.Text.Json.Serialization;

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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>brackets</c>"] Bracket info
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
        /// ["<c>bracket</c>"] Bracket
        /// </summary>
        [JsonPropertyName("bracket")]
        public int Bracket { get; set; }
        /// <summary>
        /// ["<c>initial_leverage</c>"] Initial leverage
        /// </summary>
        [JsonPropertyName("initial_leverage")]
        public decimal InitialLeverage { get; set; }
        /// <summary>
        /// ["<c>notional_cap</c>"] Maximum notional value in this bracket
        /// </summary>
        [JsonPropertyName("notional_cap")]
        public decimal MaxNotionalValue { get; set; }
        /// <summary>
        /// ["<c>notional_floor</c>"] Minimum notional value in this bracket
        /// </summary>
        [JsonPropertyName("notional_floor")]
        public decimal MinNotionalValue { get; set; }
        /// <summary>
        /// ["<c>maint_margin_ratio</c>"] Maintenance margin ratio
        /// </summary>
        [JsonPropertyName("maint_margin_ratio")]
        public decimal MaintenanceMarginRatio { get; set; }
        /// <summary>
        /// ["<c>cum</c>"] Cumulative maintenance margin amount
        /// </summary>
        [JsonPropertyName("cum")]
        public decimal CumulativeMaintenanceMargin { get; set; }
    }
}
