using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Flow type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FlowType>))]
    public enum FlowType
    {
        /// <summary>
        /// All
        /// </summary>
        [Map("0")]
        All,
        /// <summary>
        /// Transfer
        /// </summary>
        [Map("1")]
        Transfer,
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [Map("2")]
        RealizedPnl,
        /// <summary>
        /// Funding fee
        /// </summary>
        [Map("3")]
        FundingFee,
        /// <summary>
        /// Commission fee
        /// </summary>
        [Map("4")]
        CommissionFee,
        /// <summary>
        /// Liquidation clearance
        /// </summary>
        [Map("5")]
        LiquidationClearance
    }
}
