using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using BitMart.Net.Objects.Models;
using BitMart.Net.Enums;
using CryptoExchange.Net.RateLimiting.Guards;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc />
    internal class BitMartRestClientUsdFuturesApiExchangeData : IBitMartRestClientUsdFuturesApiExchangeData
    {
        private readonly BitMartRestClientUsdFuturesApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal BitMartRestClientUsdFuturesApiExchangeData(ILogger logger, BitMartRestClientUsdFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Contracts

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartContract[]>> GetContractsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/public/details", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartContractWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<BitMartContract[]>(result.Data?.Symbols);
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartOrderBook>> GetOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/public/depth", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Interest

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/public/open-interest", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartOpenInterest>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Current Funding Rate

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartFundingRate>> GetCurrentFundingRateAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/public/funding-rate", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartFundingRate>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartFundingRateHistory[]>> GetFundingRateHistoryAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/public/funding-rate-history", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartFundingRateHistoryWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<BitMartFundingRateHistory[]>(result.Data?.History);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartFuturesKline[]>> GetKlinesAsync(string symbol, FuturesKlineInterval klineInterval, DateTime startTime, DateTime endTime, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("step", klineInterval);
            parameters.AddSeconds("start_time", startTime);
            parameters.AddSeconds("end_time", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/public/kline", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartFuturesKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Mark Klines

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartFuturesKline[]>> GetMarkKlinesAsync(string symbol, FuturesKlineInterval klineInterval, DateTime startTime, DateTime endTime, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("step", klineInterval);
            parameters.AddSeconds("start_time", startTime);
            parameters.AddSeconds("end_time", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/public/markprice-kline", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartFuturesKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
