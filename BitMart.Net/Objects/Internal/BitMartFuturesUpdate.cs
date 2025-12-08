using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Internal
{
    internal class BitMartFuturesUpdate<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
        [JsonPropertyName("group")]
        public string Group { get; set; } = string.Empty;
    }
}
