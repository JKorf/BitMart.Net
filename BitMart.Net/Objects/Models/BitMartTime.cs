using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    internal record BitMartTime
    {
        [JsonPropertyName("server_time")]
        public DateTime Timestamp { get; set; }
    }
}
