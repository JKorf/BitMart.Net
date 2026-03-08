using BitMart.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType Type { get; set; }
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Limit price
        /// </summary>
        [JsonPropertyName("price"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>notional</c>"] Quote quantity for market order
        /// </summary>
        [JsonPropertyName("notional"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>stpMode</c>"] Self trade prevention mode
        /// </summary>
        [JsonPropertyName("stpMode"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public SelfTradePreventionMode? StpMode { get; set; }
    }
}
