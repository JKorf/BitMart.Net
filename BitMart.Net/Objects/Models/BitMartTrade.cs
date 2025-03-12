using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using BitMart.Net.Enums;
using BitMart.Net.Converters;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Trade info
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<BitMartTrade, BitMartSourceGenerationContext>))]
    [SerializationModel]
    public record BitMartTrade
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [ArrayProperty(0)]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [ArrayProperty(1), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [ArrayProperty(2)]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [ArrayProperty(3)]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [ArrayProperty(4)]
        public OrderSide Side { get; set; }
    }
}
