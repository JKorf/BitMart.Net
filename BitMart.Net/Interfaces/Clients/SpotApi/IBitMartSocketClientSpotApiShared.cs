using CryptoExchange.Net.SharedApis;

namespace BitMart.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Spot socket API usage
    /// </summary>
    public interface IBitMartSocketClientSpotApiShared :
        ITickerSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IBalanceSocketClient,
        ISpotOrderSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IBitMartSocketClientSpotSharedApi :
        ISubscribeTickerSocket,
        ISubscribeTradesSocket,
        ISubscribeBookTickerSocket,
        ISubscribeBalancesSocket,
        ISubscribeSpotOrdersSocket,
        ISubscribeKlinesSocket,
        ISubscribeOrderBookSocket
    { }
}
