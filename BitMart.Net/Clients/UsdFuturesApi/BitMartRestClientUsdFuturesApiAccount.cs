using CryptoExchange.Net.Objects;
using BitMart.Net.Clients.UsdFuturesApi;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using BitMart.Net.Objects.Models;
using System;
using BitMart.Net.Enums;
using CryptoExchange.Net.RateLimiting.Guards;
using BitMart.Net.Objects.Internal;

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
        public async Task<WebCallResult<IEnumerable<BitMartFuturesBalance>>> GetBalancesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/private/assets-detail", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            
            return await _baseClient.SendAsync<IEnumerable<BitMartFuturesBalance>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Transfer History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartFuturesTransfer>>> GetTransferHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptionalMillisecondsString("time_start", startTime);
            parameters.AddOptionalMillisecondsString("time_end", endTime);
            parameters.Add("page", page ?? 1);
            parameters.Add("limit", limit ?? 10);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/account/v1/transfer-contract-list", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartFuturesTransferWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartFuturesTransfer>>(result.Data?.Records);
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartTransferResult>> TransferAsync(string asset, decimal quantity, FuturesTransferType type, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            parameters.AddEnum("type", type);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/account/v1/transfer-contract", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(1, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartTransferResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartLeverage>> SetLeverageAsync(string symbol, decimal leverage, MarginType marginType, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddString("leverage", leverage);
            parameters.AddEnum("open_type", marginType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/contract/private/submit-leverage", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(24, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartLeverage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbol Trade Fee

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartFuturesFeeRate>> GetSymbolTradeFeeAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection()
            {
                { "symbol", symbol }
            };
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/private/trade-fee-rate", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));

            return await _baseClient.SendAsync<BitMartFuturesFeeRate>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Transaction History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartFuturesTransaction>>> GetTransactionHistoryAsync(string? symbol = null, FlowType? flowType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("flow_type", flowType);
            parameters.AddOptionalMillisecondsString("time_start", startTime);
            parameters.AddOptionalMillisecondsString("time_end", endTime);
            parameters.AddOptional("page_size", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/private/transaction-history", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(6, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            return await _baseClient.SendAsync<IEnumerable<BitMartFuturesTransaction>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
