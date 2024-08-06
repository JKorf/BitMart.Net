using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BitMart.Net.Objects.Models;
using BitMart.Net.Enums;
using CryptoExchange.Net.Objects;

namespace BitMart.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// BitMart UsdFutures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IBitMartRestClientUsdFuturesApiExchangeData
    {
        /// <summary>
        /// Get contracts
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#get-contract-details" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartContract>>> GetContractsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current order book
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#get-market-depth" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrderBook>> GetOrderBookAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get open interest for a symbol
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#get-futures-openinterest" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the current funding rate for a symbol
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#get-current-funding-rate" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartFundingRate>> GetCurrentFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data
        /// <para><a href="https://developer-pro.bitmart.com/en/futures/#get-k-line" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="klineInterval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<BitMartFuturesKline>>> GetKlinesAsync(string symbol, FuturesKlineInterval klineInterval, DateTime startTime, DateTime endTime, CancellationToken ct = default);

    }
}
