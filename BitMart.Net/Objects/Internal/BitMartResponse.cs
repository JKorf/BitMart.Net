using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Internal
{
    [SerializationModel]
    internal record BitMartResponse
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("trace")]
        public string Trace { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }

    [SerializationModel]
    internal record BitMartResponse<T> : BitMartResponse
    {
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
