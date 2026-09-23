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
        IPositionSocketClient,
        IOrderBookSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IBitMartSocketClientUsdFuturesSharedApi :
        ISubscribeAllTickersSocket,
        ISubscribeTickerSocket,
        ISubscribeTradesSocket,
        ISubscribeBookTickerSocket,
        ISubscribeBalancesSocket,
        ISubscribeKlinesSocket,
        ISubscribeFuturesOrdersSocket,
        ISubscribePositionsSocket,
        ISubscribeOrderBookSocket
    { }
}
