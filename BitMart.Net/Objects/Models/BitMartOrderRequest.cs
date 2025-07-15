using BitMart.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Order request
    /// </summary>
    [SerializationModel]
    public record BitMartOrderRequest
    {
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType Type { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("size"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Limit price
        /// </summary>
        [JsonPropertyName("price"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Price { get; set; }
        /// <summary>
        /// Quote quantity for market order
        /// </summary>
        [JsonPropertyName("notional"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// Self trade prevention mode
        /// </summary>
        [JsonPropertyName("stpMode"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault))]
        public SelfTradePreventionMode? StpMode { get; set; }
    }
}
