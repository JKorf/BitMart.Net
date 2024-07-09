using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Internal
{
    internal class BitMartSocketResponse
    {
        [JsonPropertyName("event")]
        public string Event { get; set; } = string.Empty;
        [JsonPropertyName("errorCode")]
        public int? ErrorCode { get; set; }
        [JsonPropertyName("errorMessage")]
        public string? ErrorMessage { get; set; }
        [JsonPropertyName("args")]
        public IEnumerable<string> Parameters { get; set; } = Array.Empty<string>();
    }
}
