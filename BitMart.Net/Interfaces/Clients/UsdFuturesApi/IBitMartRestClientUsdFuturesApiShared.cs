using CryptoExchange.Net.SharedApis;

namespace BitMart.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Usd futures rest API usage
    /// </summary>
    public interface IBitMartRestClientUsdFuturesApiShared :
        IBalanceRestClient,
        IFuturesTickerRestClient,
        IFuturesSymbolRestClient,
        IKlineRestClient,
        ILeverageRestClient,
        IOrderBookRestClient,
        IOpenInterestRestClient,
        IFuturesOrderRestClient,
        IFeeRestClient,
        IFuturesOrderClientIdClient,
        IFuturesTriggerOrderRestClient,
        IFuturesTpSlRestClient,
        IBookTickerRestClient
    {
    }
}
