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
        /// ["<c>0</c>"] All
        /// </summary>
        [Map("0")]
        All,
        /// <summary>
        /// ["<c>1</c>"] Transfer
        /// </summary>
        [Map("1")]
        Transfer,
        /// <summary>
        /// ["<c>2</c>"] Realized profit and loss
        /// </summary>
        [Map("2")]
        RealizedPnl,
        /// <summary>
        /// ["<c>3</c>"] Funding fee
        /// </summary>
        [Map("3")]
        FundingFee,
        /// <summary>
        /// ["<c>4</c>"] Commission fee
        /// </summary>
        [Map("4")]
        CommissionFee,
        /// <summary>
        /// ["<c>5</c>"] Liquidation clearance
        /// </summary>
        [Map("5")]
        LiquidationClearance
    }
}
