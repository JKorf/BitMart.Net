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
        /// Plan
        /// </summary>
        [Map("plan")]
        Plan,
        /// <summary>
        /// Profit loss
        /// </summary>
        [Map("profit_loss")]
        ProfitLoss
    }
}
