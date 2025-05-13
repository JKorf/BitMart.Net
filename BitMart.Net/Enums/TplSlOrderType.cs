using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// TakeProfit/StopLoss order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TplSlOrderType>))]
    public enum TplSlOrderType
    {
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
