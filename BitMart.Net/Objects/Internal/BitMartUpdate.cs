using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Internal
{
    internal class BitMartUpdate<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
        [JsonPropertyName("table")]
        public string Table { get; set; } = string.Empty;
    }
}
