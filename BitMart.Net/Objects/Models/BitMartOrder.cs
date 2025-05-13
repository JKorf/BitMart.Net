using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
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
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Spot order mode
        /// </summary>
        [JsonPropertyName("orderMode")]
        public SpotMode SpotMode { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("state")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Price average
        /// </summary>
        [JsonPropertyName("priceAvg")]
        public decimal? PriceAverage { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonPropertyName("filledSize")]
        public decimal? QuantityFilled { get; set; }
        /// <summary>
        /// Quote quantity
        /// </summary>
        [JsonPropertyName("notional")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// Quote quantity filled
        /// </summary>
        [JsonPropertyName("filledNotional")]
        public decimal? QuoteQuantityFilled { get; set; }
        /// <summary>
        /// Create time
        /// </summary>
        [JsonPropertyName("createTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Cancel source
        /// </summary>
        [JsonPropertyName("cancelSource")]
        public string? CancelSource { get; set; }
    }


}
