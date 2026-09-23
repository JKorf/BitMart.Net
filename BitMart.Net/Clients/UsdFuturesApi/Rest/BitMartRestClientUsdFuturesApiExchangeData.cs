using System;
using System.Net.Http;
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
        public async Task<HttpResult<BitMartContract[]>> GetContractsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/public/details", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartContractWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartContract[]>(result);

            return HttpResult.Ok(result, result.Data.Symbols);
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<HttpResult<BitMartOrderBook>> GetOrderBookAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/public/depth", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Interest

        /// <inheritdoc />
        public async Task<HttpResult<BitMartOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/public/open-interest", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartOpenInterest>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Current Funding Rate

        /// <inheritdoc />
        public async Task<HttpResult<BitMartFundingRate>> GetCurrentFundingRateAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/public/funding-rate", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartFundingRate>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Current Funding Rates

        /// <inheritdoc />
        public async Task<HttpResult<BitMartFundingRate[]>> GetCurrentFundingRatesAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/public/funding-rate-v2", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartFundingRates>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartFundingRate[]>(result);

            return HttpResult.Ok(result, result.Data.FundingRates);
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<HttpResult<BitMartFundingRateHistory[]>> GetFundingRateHistoryAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/public/funding-rate-history", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartFundingRateHistoryWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartFundingRateHistory[]>(result);

            return HttpResult.Ok(result, result.Data.History);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<HttpResult<BitMartFuturesKline[]>> GetKlinesAsync(string symbol, FuturesKlineInterval klineInterval, DateTime startTime, DateTime endTime, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("step", klineInterval);
            parameters.Add("start_time", startTime, DateTimeSerialization.SecondsString);
            parameters.Add("end_time", endTime, DateTimeSerialization.SecondsString);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/public/kline", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartFuturesKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Mark Klines

        /// <inheritdoc />
        public async Task<HttpResult<BitMartFuturesKline[]>> GetMarkKlinesAsync(string symbol, FuturesKlineInterval klineInterval, DateTime startTime, DateTime endTime, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("step", klineInterval);
            parameters.Add("start_time", startTime, DateTimeSerialization.SecondsString);
            parameters.Add("end_time", endTime, DateTimeSerialization.SecondsString);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/public/markprice-kline", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartFuturesKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Leverage Brackets

        /// <inheritdoc />
        public async Task<HttpResult<BitMartSymbolBracket[]>> GetLeverageBracketsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/public/leverage-bracket", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartSymbolBrackets>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartSymbolBracket[]>(result);

            return HttpResult.Ok(result, result.Data.Rules);
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<HttpResult<BitMartRecentTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "contract/public/market-trade", BitMartExchange.RateLimiter.BitMart, 1, false);
            var result = await _baseClient.SendAsync<BitMartRecentTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
