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
        /// ["<c>1</c>"] Trade filled
        /// </summary>
        [Map("1")]
        Trade,
        /// <summary>
        /// ["<c>2</c>"] Order submitted
        /// </summary>
        [Map("2")]
        Submit,
        /// <summary>
        /// ["<c>3</c>"] Order canceled
        /// </summary>
        [Map("3")]
        Cancel,
        /// <summary>
        /// ["<c>4</c>"] Liquidate cancel order
        /// </summary>
        [Map("4")]
        LiquidationCancel,
        /// <summary>
        /// ["<c>5</c>"] Adl cancel order
        /// </summary>
        [Map("5")]
        AdlCancel,
        /// <summary>
        /// ["<c>6</c>"] Partial liquidate
        /// </summary>
        [Map("6")]
        PartialLiquidation,
        /// <summary>
        /// ["<c>7</c>"] Bankruptcy order
        /// </summary>
        [Map("7")]
        Bankruptcy,
        /// <summary>
        /// ["<c>8</c>"] Passive Adl trade
        /// </summary>
        [Map("8")]
        PassiveAdlTrade,
        /// <summary>
        /// ["<c>9</c>"] Active adl trade
        /// </summary>
        [Map("9")]
        ActiveAdlTrade
    }
}
