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
        /// Get server status
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-system-service-status" /></para>
        /// </summary>
        Task<WebCallResult<IEnumerable<BitMartStatus>>> GetServerStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Get server time
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-system-time" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get supported assets list
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-currency-list-v1" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BitMartAsset>>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get supported symbols list
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-trading-pair-details-v1" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BitMartSymbol>>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get price ticker for a symbol
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-ticker-of-a-trading-pair-v3" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get price tickers for all symbols
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-ticker-of-all-pairs-v3" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<BitMartArrayTicker>>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get deposit and withdrawal info for assets
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-currencies" /></para>
        /// </summary>
        Task<WebCallResult<IEnumerable<BitMartAssetDepositWithdrawInfo>>> GetAssetDepositWithdrawInfoAsync(CancellationToken ct = default);

    }
}
