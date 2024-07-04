using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;

namespace BitMart.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// BitMart UsdFutures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IBitMartRestClientUsdFuturesApiExchangeData
    {
        /// <summary>
        /// 
        /// <para><a href="XXX" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);
    }
}
