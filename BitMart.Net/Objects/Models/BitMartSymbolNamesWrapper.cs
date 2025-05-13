using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    [SerializationModel]
    internal record BitMartSymbolNamesWrapper
    {
        [JsonPropertyName("symbols")]
        public string[] Symbols { get; set; } = Array.Empty<string>();
    }
}
