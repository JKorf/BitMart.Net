using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Trade session status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SessionStatus>))]
    public enum SessionStatus
    {
        /// <summary>
        /// ["<c>1</c>"] Normal trading
        /// </summary>
        [Map("1")]
        NormalTradingMode,
        /// <summary>
        /// ["<c>2</c>"] Low liquidity mode
        /// </summary>
        [Map("2")]
        LowLiquidityMode
    }
}
