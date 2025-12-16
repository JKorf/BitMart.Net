using System;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Internal
{
    internal class BitMartFuturesSocketOperation
    {
        [JsonPropertyName("action")]
        public string Operation { get; set; } = string.Empty;
        [JsonPropertyName("args")]
        public string[] Parameters { get; set; } = Array.Empty<string>();
    }
}
