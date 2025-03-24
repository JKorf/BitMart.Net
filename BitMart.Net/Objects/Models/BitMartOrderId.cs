using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Order id
    /// </summary>
    [SerializationModel]
    public record BitMartOrderId
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id"), JsonConverter(typeof(NumberStringConverter))]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string? ClientOrderId { get; set; } = string.Empty;
    }


}
