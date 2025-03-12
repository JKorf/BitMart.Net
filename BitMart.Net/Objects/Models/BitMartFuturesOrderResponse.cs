using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Futures order response
    /// </summary>
    [SerializationModel]
    public record BitMartFuturesOrderResponse
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// Price. Note that this is a string because when executing a market trade the server will return `market price` as string value.
        /// </summary>
        [JsonPropertyName("price")]
        public string Price { get; set; } = string.Empty;
    }
}
