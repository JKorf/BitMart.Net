﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Cancel orders result
    /// </summary>
    public record BitMartCancelOrdersResult
    {
        /// <summary>
        /// Success ids
        /// </summary>
        [JsonPropertyName("successIds")]
        public IEnumerable<string> SuccessIds { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Fail ids
        /// </summary>
        [JsonPropertyName("failIds")]
        public IEnumerable<string> FailIds { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Total count
        /// </summary>
        [JsonPropertyName("totalCount")]
        public decimal TotalCount { get; set; }
        /// <summary>
        /// Success count
        /// </summary>
        [JsonPropertyName("successCount")]
        public decimal SuccessCount { get; set; }
        /// <summary>
        /// Failed count
        /// </summary>
        [JsonPropertyName("failedCount")]
        public decimal FailedCount { get; set; }
    }

}