using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using BitMart.Net.Enums;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// 
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
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
        [ArrayProperty(4), JsonConverter(typeof(EnumConverter))]
        public OrderSide Side { get; set; }
    }
}
