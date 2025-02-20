using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
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
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "system/time", BitMartExchange.RateLimiter.BitMart, 1, false, new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding), preventCaching: true);
            var result = await _baseClient.SendAsync<BitMartTime>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data?.Timestamp ?? default);
        }

        #endregion

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartAsset>>> GetAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "spot/v1/currencies", BitMartExchange.RateLimiter.BitMart, 1, false, 
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartAssetWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartAsset>>(result.Data?.Currencies);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartSymbol>>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/v1/symbols/details", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartSymbolWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartSymbol>>(result.Data?.Symbols);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<string>>> GetSymbolNamesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/v1/symbols", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartSymbolNamesWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<string>>(result.Data?.Symbols);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartTicker>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/quotation/v3/ticker", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(15, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartTicker>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartArrayTicker>>> GetTickersAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/quotation/v3/tickers", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<IEnumerable<BitMartArrayTicker>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartAssetDepositWithdrawInfo>>> GetAssetDepositWithdrawInfoAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currencies", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/account/v1/currencies", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartAssetDepositWithdrawInfoWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartAssetDepositWithdrawInfo>>(result.Data?.Currencies);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartStatus>>> GetServerStatusAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/system/service", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartStatusWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartStatus>>(result.Data?.Service);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartKline>>> GetKlinesAsync(string symbol, KlineInterval klineInterval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalSecondsString("after", startTime);
            parameters.AddOptionalSecondsString("before", endTime);
            parameters.AddOptional("limit", limit);
            parameters.Add("symbol", symbol);
            parameters.AddEnum("step", klineInterval);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/quotation/v3/lite-klines", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(15, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<IEnumerable<BitMartKline>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartKline>>> GetKlineHistoryAsync(string symbol, KlineInterval klineInterval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalSecondsString("after", startTime);
            parameters.AddOptionalSecondsString("before", endTime);
            parameters.AddOptional("limit", limit);
            parameters.Add("symbol", symbol);
            parameters.AddEnum("step", klineInterval);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/quotation/v3/klines", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(10, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<IEnumerable<BitMartKline>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartTrade>>> GetTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("limit", limit);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/quotation/v3/trades", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(15, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<IEnumerable<BitMartTrade>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("limit", limit);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/quotation/v3/books", BitMartExchange.RateLimiter.BitMart, 1, false,
                new SingleLimitGuard(15, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<BitMartOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

    }
}
