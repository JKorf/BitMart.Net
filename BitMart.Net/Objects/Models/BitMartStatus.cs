using BitMart.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    internal record BitMartStatusWrapper
    {
        /// <summary>
        /// Service
        /// </summary>
        [JsonPropertyName("service")]
        public IEnumerable<BitMartStatus> Service { get; set; } = Array.Empty<BitMartStatus>();
    }

    /// <summary>
    /// Server status info
    /// </summary>
    public record BitMartStatus
    {
        /// <summary>
        /// Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Service type
        /// </summary>
        [JsonPropertyName("service_type")]
        public ServiceType? ServiceType { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public SystemMaintenanceStatus? Status { get; set; }
        /// <summary>
        /// Start time
        /// </summary>
        [JsonPropertyName("start_time")]
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        [JsonPropertyName("end_time")]
        public DateTime? EndTime { get; set; }
    }
}
