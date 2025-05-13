using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Trigger order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerOrderType>))]
    public enum TriggerOrderType
    {
        /// <summary>
        /// Plan order
        /// </summary>
        [Map("plan")]
        Plan,
        /// <summary>
        /// Take profit order
        /// </summary>
        [Map("take_profit")]
        TakeProfit,
        /// <summary>
        /// Stop loss order
        /// </summary>
        [Map("stop_loss")]
        StopLoss
    }
}
