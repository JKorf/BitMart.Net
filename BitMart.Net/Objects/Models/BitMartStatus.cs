using CryptoExchange.Net.Converters.SystemTextJson;
using BitMart.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartStatusWrapper
    {
        /// <summary>
        /// ["<c>service</c>"] Service
        /// </summary>
        [JsonPropertyName("service")]
        public BitMartStatus[] Service { get; set; } = Array.Empty<BitMartStatus>();
    }

    /// <summary>
    /// Server status info
    /// </summary>
    [SerializationModel]
    public record BitMartStatus
    {
        /// <summary>
        /// ["<c>title</c>"] Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>service_type</c>"] Service type
        /// </summary>
        [JsonPropertyName("service_type")]
        public ServiceType? ServiceType { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public SystemMaintenanceStatus? Status { get; set; }
        /// <summary>
        /// ["<c>start_time</c>"] Start time
        /// </summary>
        [JsonPropertyName("start_time")]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// ["<c>end_time</c>"] End time
        /// </summary>
        [JsonPropertyName("end_time")]
        public DateTime? EndTime { get; set; }
    }
}
