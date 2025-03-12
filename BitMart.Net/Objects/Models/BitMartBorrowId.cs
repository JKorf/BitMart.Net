using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Borrow id
    /// </summary>
    [SerializationModel]
    public record BitMartBorrowId
    {
        /// <summary>
        /// Borrow id
        /// </summary>
        [JsonPropertyName("borrow_id")]
        public string BorrowId { get; set; } = string.Empty;
    }


}
