using System;
using System.Threading;
using System.Threading.Tasks;
using BitMart.Net.Objects.Models;
using BitMart.Net.Enums;
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
        Task<WebCallResult<BitMartStatus[]>> GetServerStatusAsync(CancellationToken ct = default);

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
        Task<WebCallResult<BitMartAsset[]>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get supported symbols list
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-trading-pair-details-v1" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BitMartSymbol[]>> GetSymbolsAsync( CancellationToken ct = default);

        /// <summary>
        /// Get a list of supported symbol names
        /// <para><a href="https://openapi-doc.bitmart.com/en/spot/#get-trading-pairs-list-v1" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string[]>> GetSymbolNamesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get price ticker for a symbol
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-ticker-of-a-trading-pair-v3" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get price tickers for all symbols
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-ticker-of-all-pairs-v3" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BitMartArrayTicker[]>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get deposit and withdrawal info for assets
        /// <para><a href="https://developer-pro.bitmart.com/en/spot/#get-currencies" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset. Can specify up to 20 assets comma separated</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartAssetDepositWithdrawInfo[]>> GetAssetDepositWithdrawInfoAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlesticks
        /// <para><a href="https://openapi-doc.bitmart.com/en/spot/#get-latest-k-line-v3" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="klineInterval">The interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartKline[]>> GetKlinesAsync(string symbol, KlineInterval klineInterval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get historical klines
        /// <para><a href="https://openapi-doc.bitmart.com/en/spot/#get-history-k-line-v3" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="klineInterval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartKline[]>> GetKlineHistoryAsync(string symbol, KlineInterval klineInterval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get recent trades
        /// <para><a href="https://openapi-doc.bitmart.com/en/spot/#get-recent-trades-v3" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartTrade[]>> GetTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current order book
        /// <para><a href="https://openapi-doc.bitmart.com/en/spot/#get-depth-v3" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="limit">Max number of rows in the book</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BitMartOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

    }
}
