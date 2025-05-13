using CryptoExchange.Net.Objects;
using BitMart.Net.Clients.SpotApi;
using BitMart.Net.Interfaces.Clients.SpotApi;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitMart.Net.Objects.Models;
using System.Threading;
using System.Net.Http;
using BitMart.Net.Enums;
using System;
using CryptoExchange.Net.RateLimiting.Guards;

namespace BitMart.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class BitMartRestClientSpotApiSubAccount : IBitMartRestClientSpotApiSubAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitMartRestClientSpotApi _baseClient;

        internal BitMartRestClientSpotApiSubAccount(BitMartRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Transfer Sub To Main For Main

        /// <inheritdoc />
        public async Task<WebCallResult> TransferSubToMainForMainAsync(string clientOrderId, string asset, decimal quantity, string subAccount, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("requestNo", clientOrderId);
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            parameters.Add("subAccount", subAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/account/sub-account/main/v1/sub-to-main", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer Sub To Main For Sub

        /// <inheritdoc />
        public async Task<WebCallResult> TransferSubToMainForSubAsync(string clientOrderId, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("requestNo", clientOrderId);
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/account/sub-account/sub/v1/sub-to-main", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer Main To Sub Account

        /// <inheritdoc />
        public async Task<WebCallResult> TransferMainToSubAccountAsync(string clientOrderId, string asset, decimal quantity, string subAccount, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("requestNo", clientOrderId);
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            parameters.Add("subAccount", subAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/account/sub-account/main/v1/main-to-sub", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer Sub Account To Sub Account

        /// <inheritdoc />
        public async Task<WebCallResult> TransferSubAccountToSubAccountAsync(string clientOrderId, decimal quantity, string asset, string fromAccount, string toAccount, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("requestNo", clientOrderId);
            parameters.AddString("quantity", quantity);
            parameters.Add("currency", asset);
            parameters.Add("fromAccount", fromAccount);
            parameters.Add("toAccount", toAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/account/sub-account/main/v1/sub-to-sub", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Sub Account Transfer History For Main

        /// <inheritdoc />
        public async Task<WebCallResult<SubAccountTransferHistory>> GetSubAccountTransferHistoryForMainAsync(int limit, string? account = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("N", limit);
            parameters.AddOptional("accountName", account);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/account/sub-account/main/v1/transfer-list", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<SubAccountTransferHistory>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Sub Account Transfer History

        /// <inheritdoc />
        public async Task<WebCallResult<SubAccountTransferHistory>> GetSubAccountTransferHistoryAsync(int limit, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("N", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/account/sub-account/v1/transfer-history", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<SubAccountTransferHistory>(request, parameters, ct).ConfigureAwait(false);
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
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/account/sub-account/main/v1/wallet", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartSubAccountBalanceWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<BitMartSubAccountBalance[]>(result.Data?.Wallet);
        }

        #endregion

        #region Get Sub Account List

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartSubAccount[]>> GetSubAccountListAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/account/sub-account/main/v1/subaccount-list", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartSubAccountWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<BitMartSubAccount[]>(result.Data?.SubAccountList);
        }

        #endregion

    }
}
