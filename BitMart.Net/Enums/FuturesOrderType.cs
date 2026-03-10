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
        /// ["<c>limit</c>"] Limit order
        /// </summary>
        [Map("limit")]
        Limit,
        /// <summary>
        /// ["<c>market</c>"] Market order
        /// </summary>
        [Map("market")]
        Market,
        /// <summary>
        /// ["<c>liquidate</c>"] Liquidation
        /// </summary>
        [Map("liquidate")]
        Liquidation,
        /// <summary>
        /// ["<c>bankruptcy</c>"] Bankruptcy
        /// </summary>
        [Map("bankruptcy")]
        Bankruptcy,
        /// <summary>
        /// ["<c>adl</c>"] Auto deleverage
        /// </summary>
        [Map("adl")]
        AutoDeleverage,
        /// <summary>
        /// ["<c>trailing</c>"] Trailing
        /// </summary>
        [Map("trailing")]
        Trailing,
        /// <summary>
        /// ["<c>plan_order</c>"] Plan order
        /// </summary>
        [Map("plan_order", "planorder")]
        PlanOrder,
        /// <summary>
        /// ["<c>stop_loss</c>"] Stop loss
        /// </summary>
        [Map("stop_loss")]
        StopLoss,
        /// <summary>
        /// ["<c>take_profit</c>"] Take profit
        /// </summary>
        [Map("take_profit")]
        TakeProfit
    }
}
