using CryptoExchange.Net.SharedApis;

namespace BitMart.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// Shared interface for Usd futures socket API usage
    /// </summary>
    public interface IBitMartSocketClientUsdFuturesApiShared :
        ITickersSocketClient,
        ITickerSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IBalanceSocketClient,
        IKlineSocketClient,
        IFuturesOrderSocketClient,
        IPositionSocketClient
    {
    }
}
