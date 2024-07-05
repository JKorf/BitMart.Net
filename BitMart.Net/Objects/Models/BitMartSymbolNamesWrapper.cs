using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    internal record BitMartSymbolNamesWrapper
    {
        [JsonPropertyName("symbols")]
        public IEnumerable<string> Symbols { get; set; } = Array.Empty<string>();
    }
}
