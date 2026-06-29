using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Objects.Models;
using BitMart.Net.Enums;
using CryptoExchange.Net.RateLimiting.Guards;

namespace BitMart.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BitMartRestClientSpotApiExchangeData : IBitMartRestClientSpotApiExchangeData
    {
        private readonly BitMartRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal BitMartRestClientSpotApiExchangeData(ILogger logger, BitMartRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Server Time

        /// <inheritdoc />
        public async Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "system/time", BitMartExchange.RateLimiter.BitMart, 1, false, new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding), preventCaching: true);
            var result = await _baseClient.SendAsync<BitMartTime>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<DateTime>(result);

            return HttpResult.Ok(result, result.Data.Timestamp);
        }

        #endregion

        /// <inheritdoc />
        public async Task<HttpResult<BitMartAsset[]>> GetAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "spot/v1/currencies", BitMartExchange.RateLimiter.BitMart, 1, false, 
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartAssetWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartAsset[]>(result);

            return HttpResult.Ok(result, result.Data.Currencies);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BitMartSymbol[]>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/spot/v1/symbols/details", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartSymbolWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartSymbol[]>(result);

            return HttpResult.Ok(result, result.Data.Symbols);
        }

        /// <inheritdoc />
        public async Task<HttpResult<string[]>> GetSymbolNamesAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/spot/v1/symbols", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartSymbolNamesWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<string[]>(result);

            return HttpResult.Ok(result, result.Data.Symbols);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BitMartTicker>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/spot/quotation/v3/ticker", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(15, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartTicker>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<HttpResult<BitMartArrayTicker[]>> GetTickersAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/spot/quotation/v3/tickers", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartArrayTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<HttpResult<BitMartAssetDepositWithdrawInfo[]>> GetAssetDepositWithdrawInfoAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("currencies", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/account/v1/currencies", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartAssetDepositWithdrawInfoWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartAssetDepositWithdrawInfo[]>(result);

            return HttpResult.Ok(result, result.Data.Currencies);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BitMartStatus[]>> GetServerStatusAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/system/service", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartStatusWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartStatus[]>(result);

            return HttpResult.Ok(result, result.Data.Service);
        }

        /// <inheritdoc />
        public async Task<HttpResult<BitMartKline[]>> GetKlinesAsync(string symbol, KlineInterval klineInterval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("after", startTime, DateTimeSerialization.SecondsString);
            parameters.Add("before", endTime, DateTimeSerialization.SecondsString);
            parameters.Add("limit", limit);
            parameters.Add("symbol", symbol);
            parameters.Add("step", klineInterval);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/spot/quotation/v3/lite-klines", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(15, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<HttpResult<BitMartKline[]>> GetKlineHistoryAsync(string symbol, KlineInterval klineInterval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("after", startTime, DateTimeSerialization.SecondsString);
            parameters.Add("before", endTime, DateTimeSerialization.SecondsString);
            parameters.Add("limit", limit);
            parameters.Add("symbol", symbol);
            parameters.Add("step", klineInterval);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/spot/quotation/v3/klines", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<HttpResult<BitMartTrade[]>> GetTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("limit", limit);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/spot/quotation/v3/trades", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(15, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<HttpResult<BitMartOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("limit", limit);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/spot/quotation/v3/books", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(15, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

    }
}
