using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>buy_taker_fee_rate</c>"] Buy taker fee rate
        /// </summary>
        [JsonPropertyName("buy_taker_fee_rate")]
        public decimal BuyTakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>sell_taker_fee_rate</c>"] Sell taker fee rate
        /// </summary>
        [JsonPropertyName("sell_taker_fee_rate")]
        public decimal SellTakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>buy_maker_fee_rate</c>"] Buy maker fee rate
        /// </summary>
        [JsonPropertyName("buy_maker_fee_rate")]
        public decimal BuyMakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>sell_maker_fee_rate</c>"] Sell maker fee rate
        /// </summary>
        [JsonPropertyName("sell_maker_fee_rate")]
        public decimal SellMakerFeeRate { get; set; }
    }


}
