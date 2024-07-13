using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Internal
{
    internal class BitMartFuturesLoginResponse
    {
        [JsonPropertyName("action")]
        public string Action { get; set; } = string.Empty;
        [JsonPropertyName("success")]
        public bool? Success { get; set; }
        [JsonPropertyName("error")]
        public string? ErrorMessage { get; set; }
    }
}
