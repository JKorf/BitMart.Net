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
        /// ["<c>plan</c>"] Plan order
        /// </summary>
        [Map("plan")]
        Plan,
        /// <summary>
        /// ["<c>take_profit</c>"] Take profit order
        /// </summary>
        [Map("take_profit")]
        TakeProfit,
        /// <summary>
        /// ["<c>stop_loss</c>"] Stop loss order
        /// </summary>
        [Map("stop_loss")]
        StopLoss
    }
}
