using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Futures order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesOrderType>))]
    public enum FuturesOrderType
    {
        /// <summary>
        /// Limit order
        /// </summary>
        [Map("limit")]
        Limit,
        /// <summary>
        /// Market order
        /// </summary>
        [Map("market")]
        Market,
        /// <summary>
        /// Liquidation
        /// </summary>
        [Map("liquidate")]
        Liquidation,
        /// <summary>
        /// Bankruptcy
        /// </summary>
        [Map("bankruptcy")]
        Bankruptcy,
        /// <summary>
        /// Auto deleverage
        /// </summary>
        [Map("adl")]
        AutoDeleverage,
        /// <summary>
        /// Trailing
        /// </summary>
        [Map("trailing")]
        Trailing,
        /// <summary>
        /// Plan order
        /// </summary>
        [Map("plan_order", "planorder")]
        PlanOrder,
        /// <summary>
        /// Stop loss
        /// </summary>
        [Map("stop_loss")]
        StopLoss,
        /// <summary>
        /// Take profit
        /// </summary>
        [Map("take_profit")]
        TakeProfit
    }
}
