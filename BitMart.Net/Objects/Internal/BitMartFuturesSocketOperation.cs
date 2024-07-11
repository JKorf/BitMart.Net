using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Internal
{
    internal class BitMartFuturesSocketOperation
    {
        [JsonPropertyName("action")]
        public string Operation { get; set; } = string.Empty;
        [JsonPropertyName("args")]
        public IEnumerable<string> Parameters { get; set; } = Array.Empty<string>();
    }
}
