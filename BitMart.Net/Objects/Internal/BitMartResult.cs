using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Internal
{
    [SerializationModel]
    internal record BitMartResult
    {
        /// <summary>
        /// Result
        /// </summary>
        [JsonPropertyName("result")]
        public bool Result { get; set; }
    }


}
