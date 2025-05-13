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

namespace BitMart.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc />
    internal class BitMartRestClientUsdFuturesApiSubAccount : IBitMartRestClientUsdFuturesApiSubAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitMartRestClientUsdFuturesApi _baseClient;

        internal BitMartRestClientUsdFuturesApiSubAccount(BitMartRestClientUsdFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Transfer Sub To Main For Main

        /// <inheritdoc />
        public async Task<WebCallResult> TransferSubToMainForMainAsync(string asset, decimal quantity, string subAccount, string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            parameters.Add("subAccount", subAccount);
            parameters.Add("requestNo", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/account/contract/sub-account/main/v1/sub-to-main", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer Main To Sub For Main

        /// <inheritdoc />
        public async Task<WebCallResult> TransferMainToSubForMainAsync(string asset, decimal quantity, string subAccount, string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            parameters.Add("subAccount", subAccount);
            parameters.Add("requestNo", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/account/contract/sub-account/main/v1/main-to-sub", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer Sub To Main For Sub

        /// <inheritdoc />
        public async Task<WebCallResult> TransferSubToMainForSubAsync(string asset, decimal quantity, string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            parameters.Add("requestNo", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/account/contract/sub-account/sub/v1/sub-to-main", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Sub Acccount Balance

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartSubAccountBalance[]>> GetSubAcccountBalanceAsync(string subAccount, string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("subAccount", subAccount);
            parameters.AddOptional("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/account/contract/sub-account/main/v1/wallet", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartSubAccountBalanceWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<BitMartSubAccountBalance[]>(result.Data?.Wallet);
        }

        #endregion

        #region Get Sub Account Transfer History

        /// <inheritdoc />
        public async Task<WebCallResult<SubAccountTransfer[]>> GetSubAccountTransferHistoryForMainAsync(string subAccount, int limit, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("subAccount", subAccount);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/account/contract/sub-account/main/v1/transfer-list", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<SubAccountTransfer[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Sub Account Transfer History

        /// <inheritdoc />
        public async Task<WebCallResult<SubAccountTransfer[]>> GetSubAccountTransferHistoryAsync(int limit, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/account/contract/sub-account/v1/transfer-history", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<SubAccountTransfer[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
