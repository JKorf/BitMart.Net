using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Self trade prevention mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SelfTradePreventionMode>))]
    public enum SelfTradePreventionMode
    {
        /// <summary>
        /// None
        /// </summary>
        [Map("none")]
        None,
        /// <summary>
        /// Cancel maker order
        /// </summary>
        [Map("cancel_maker")]
        CancelMaker,
        /// <summary>
        /// Cancel taker order
        /// </summary>
        [Map("cancel_taker")]
        CancelTaker,
        /// <summary>
        /// Cancel both orders
        /// </summary>
        [Map("cancel_both")]
        CancelBoth,
    }
}
