using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Internal
{
    internal record BitMartResponse
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("trace")]
        public string Trace { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }

    internal record BitMartResponse<T> : BitMartResponse
    {
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
    }
}
