using CryptoExchange.Net.Objects;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using BitMart.Net.Objects.Models;
using System;
using BitMart.Net.Enums;
using CryptoExchange.Net.RateLimiting.Guards;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc />
    internal class BitMartRestClientUsdFuturesApiAccount : IBitMartRestClientUsdFuturesApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitMartRestClientUsdFuturesApi _baseClient;

        internal BitMartRestClientUsdFuturesApiAccount(BitMartRestClientUsdFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Balances

        /// <inheritdoc />
        public async Task<HttpResult<BitMartFuturesBalance[]>> GetBalancesAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/private/assets-detail", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            
            return await _baseClient.SendAsync<BitMartFuturesBalance[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Transfer History

        /// <inheritdoc />
        public async Task<HttpResult<BitMartFuturesTransfer[]>> GetTransferHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("time_start", startTime);
            parameters.Add("time_end", endTime);
            parameters.Add("page", page ?? 1);
            parameters.Add("limit", limit ?? 10);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/account/v1/transfer-contract-list", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartFuturesTransferWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartFuturesTransfer[]>(result);

            return HttpResult.Ok(result, result.Data.Records);
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<HttpResult<BitMartTransferResult>> TransferAsync(string asset, decimal quantity, FuturesTransferType type, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("amount", quantity);
            parameters.Add("type", type);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/account/v1/transfer-contract", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartTransferResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Leverage

        /// <inheritdoc />
        public async Task<HttpResult<BitMartLeverage>> SetLeverageAsync(string symbol, decimal leverage, MarginType marginType, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("leverage", leverage);
            parameters.Add("open_type", marginType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/contract/private/submit-leverage", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(24, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartLeverage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbol Trade Fee

        /// <inheritdoc />
        public async Task<HttpResult<BitMartFuturesFeeRate>> GetSymbolTradeFeeAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/private/trade-fee-rate", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));

            return await _baseClient.SendAsync<BitMartFuturesFeeRate>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Transaction History

        /// <inheritdoc />
        public async Task<HttpResult<BitMartFuturesTransaction[]>> GetTransactionHistoryAsync(string? symbol = null, FlowType? flowType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("flow_type", flowType);
            parameters.Add("time_start", startTime);
            parameters.Add("time_end", endTime);
            parameters.Add("page_size", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/private/transaction-history", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(6, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<BitMartFuturesTransaction[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Position Mode

        /// <inheritdoc />
        public async Task<HttpResult<BitMartPositionMode>> SetPositionModeAsync(PositionMode positionMode, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("position_mode", positionMode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/contract/private/set-position-mode", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartPositionMode>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Position Mode

        /// <inheritdoc />
        public async Task<HttpResult<BitMartPositionMode>> GetPositionModeAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/contract/private/get-position-mode", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartPositionMode>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
