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
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#get-system-service-status" /><br />
        /// Endpoint:<br />
        /// GET /system/service
        /// </para>
        /// </summary>
        Task<WebCallResult<BitMartStatus[]>> GetServerStatusAsync(CancellationToken ct = default);

        /// <summary>
        /// Get server time
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#get-system-time" /><br />
        /// Endpoint:<br />
        /// GET /system/time
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get supported assets list
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#get-currency-list-v1" /><br />
        /// Endpoint:<br />
        /// GET /spot/v1/currencies
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BitMartAsset[]>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get supported symbols list
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#get-trading-pair-details-v1" /><br />
        /// Endpoint:<br />
        /// GET /spot/v1/symbols/details
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BitMartSymbol[]>> GetSymbolsAsync( CancellationToken ct = default);

        /// <summary>
        /// Get a list of supported symbol names
        /// <para>
        /// Docs:<br />
        /// <a href="https://openapi-doc.bitmart.com/en/spot/#get-trading-pairs-list-v1" /><br />
        /// Endpoint:<br />
        /// GET /spot/v1/symbols
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string[]>> GetSymbolNamesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get price ticker for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#get-ticker-of-a-trading-pair-v3" /><br />
        /// Endpoint:<br />
        /// GET /spot/quotation/v3/ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get price tickers for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#get-ticker-of-all-pairs-v3" /><br />
        /// Endpoint:<br />
        /// GET /spot/quotation/v3/tickers
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BitMartArrayTicker[]>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get deposit and withdrawal info for assets
        /// <para>
        /// Docs:<br />
        /// <a href="https://developer-pro.bitmart.com/en/spot/#get-currencies" /><br />
        /// Endpoint:<br />
        /// GET /account/v1/currencies
        /// </para>
        /// </summary>
        /// <param name="asset">Filter by asset. Can specify up to 20 assets comma separated</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartAssetDepositWithdrawInfo[]>> GetAssetDepositWithdrawInfoAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlesticks
        /// <para>
        /// Docs:<br />
        /// <a href="https://openapi-doc.bitmart.com/en/spot/#get-latest-k-line-v3" /><br />
        /// Endpoint:<br />
        /// GET /spot/quotation/v3/lite-klines
        /// </para>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://openapi-doc.bitmart.com/en/spot/#get-history-k-line-v3" /><br />
        /// Endpoint:<br />
        /// GET /spot/quotation/v3/klines
        /// </para>
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
        /// <para>
        /// Docs:<br />
        /// <a href="https://openapi-doc.bitmart.com/en/spot/#get-recent-trades-v3" /><br />
        /// Endpoint:<br />
        /// GET /spot/quotation/v3/trades
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartTrade[]>> GetTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current order book
        /// <para>
        /// Docs:<br />
        /// <a href="https://openapi-doc.bitmart.com/en/spot/#get-depth-v3" /><br />
        /// Endpoint:<br />
        /// GET /spot/quotation/v3/books
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="limit">Max number of rows in the book</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<BitMartOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

    }
}
