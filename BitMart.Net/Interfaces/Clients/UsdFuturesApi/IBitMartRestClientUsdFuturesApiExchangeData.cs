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
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-contract-details" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartContract[]>> GetContractsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current order book
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-market-depth" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOrderBook>> GetOrderBookAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get open interest for a symbol
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-futures-openinterest" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the current funding rate for a symbol
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-current-funding-rate" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartFundingRate>> GetCurrentFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-k-line" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="klineInterval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartFuturesKline[]>> GetKlinesAsync(string symbol, FuturesKlineInterval klineInterval, DateTime startTime, DateTime endTime, CancellationToken ct = default);

        /// <summary>
        /// Get mark price klines
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-markprice-k-line" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="klineInterval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartFuturesKline[]>> GetMarkKlinesAsync(string symbol, FuturesKlineInterval klineInterval, DateTime startTime, DateTime endTime, CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-funding-rate-history" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="limit">Max number of results, max 100</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartFundingRateHistory[]>> GetFundingRateHistoryAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get leverage bracket info
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-current-leverage-risk-limit" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartSymbolBracket[]>> GetLeverageBracketsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get recent trades
        /// <para><a href="https://developer-pro.bitmart.com/en/futuresv2/#get-market-trade" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<BitMartRecentTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

    }
}
