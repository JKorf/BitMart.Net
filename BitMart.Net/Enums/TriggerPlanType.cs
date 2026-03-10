using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Trigger plan type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerPlanType>))]
    public enum TriggerPlanType
    {
        /// <summary>
        /// ["<c>plan</c>"] Plan
        /// </summary>
        [Map("plan")]
        Plan,
        /// <summary>
        /// ["<c>profit_loss</c>"] Profit loss
        /// </summary>
        [Map("profit_loss")]
        ProfitLoss
    }
}
