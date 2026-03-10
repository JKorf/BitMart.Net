using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Trigger order category
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PlanCategory>))]
    public enum PlanCategory
    {
        /// <summary>
        /// ["<c>1</c>"] Take profit / Stop loss
        /// </summary>
        [Map("1")]
        TpSl,
        /// <summary>
        /// ["<c>2</c>"] Position Take profit / Stop loss
        /// </summary>
        [Map("2")]
        PositionTpSl
    }
}
