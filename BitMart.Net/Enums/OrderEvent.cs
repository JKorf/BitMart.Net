using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Order event type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderEvent>))]
    public enum OrderEvent
    {
        /// <summary>
        /// Trade filled
        /// </summary>
        [Map("1")]
        Trade,
        /// <summary>
        /// Order submitted
        /// </summary>
        [Map("2")]
        Submit,
        /// <summary>
        /// Order canceled
        /// </summary>
        [Map("3")]
        Cancel,
        /// <summary>
        /// Liquidate cancel order
        /// </summary>
        [Map("4")]
        LiquidationCancel,
        /// <summary>
        /// Adl cancel order
        /// </summary>
        [Map("5")]
        AdlCancel,
        /// <summary>
        /// Partial liquidate
        /// </summary>
        [Map("6")]
        PartialLiquidation,
        /// <summary>
        /// Bankruptcy order
        /// </summary>
        [Map("7")]
        Bankruptcy,
        /// <summary>
        /// Passive Adl trade
        /// </summary>
        [Map("8")]
        PassiveAdlTrade,
        /// <summary>
        /// Active adl trade
        /// </summary>
        [Map("9")]
        ActiveAdlTrade
    }
}
