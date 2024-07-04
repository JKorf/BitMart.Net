using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BitMart.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace BitMart.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// BitMart Spot exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IBitMartRestClientSpotApiExchangeData
    {
        /// <summary>
        /// 
        /// <para><a href="XXX" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BitMartAsset>>> GetAssetsAsync(CancellationToken ct = default);

        Task<WebCallResult<IEnumerable<BitMartSymbol>>> GetSymbolsAsync(CancellationToken ct = default);
    }
}
