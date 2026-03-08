using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using BitMart.Net.Enums;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    [SerializationModel]
    public record BitMartOrder
    {
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>orderMode</c>"] Spot order mode
        /// </summary>
        [JsonPropertyName("orderMode")]
        public SpotMode SpotMode { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Order status
        /// </summary>
        [JsonPropertyName("state")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>priceAvg</c>"] Price average
        /// </summary>
        [JsonPropertyName("priceAvg")]
        public decimal? PriceAverage { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>filledSize</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("filledSize")]
        public decimal? QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>notional</c>"] Quote quantity
        /// </summary>
        [JsonPropertyName("notional")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>filledNotional</c>"] Quote quantity filled
        /// </summary>
        [JsonPropertyName("filledNotional")]
        public decimal? QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>createTime</c>"] Create time
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updateTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>cancelSource</c>"] Cancel source
        /// </summary>
        [JsonPropertyName("cancelSource")]
        public string? CancelSource { get; set; }
        /// <summary>
        /// ["<c>stpMode</c>"] Self trade prevention mode
        /// </summary>
        [JsonPropertyName("stpMode")]
        public SelfTradePreventionMode? StpMode { get; set; }
    }


}
