using CryptoExchange.Net.Objects;
using BitMart.Net.Interfaces.Clients.SpotApi;
using System.Threading.Tasks;
using BitMart.Net.Objects.Models;
using System.Threading;
using System.Net.Http;
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
        public async Task<HttpResult> TransferSubToMainForMainAsync(string clientOrderId, string asset, decimal quantity, string subAccount, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("requestNo", clientOrderId);
            parameters.Add("currency", asset);
            parameters.Add("amount", quantity);
            parameters.Add("subAccount", subAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/account/sub-account/main/v1/sub-to-main", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer Sub To Main For Sub

        /// <inheritdoc />
        public async Task<HttpResult> TransferSubToMainForSubAsync(string clientOrderId, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("requestNo", clientOrderId);
            parameters.Add("currency", asset);
            parameters.Add("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/account/sub-account/sub/v1/sub-to-main", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer Main To Sub Account

        /// <inheritdoc />
        public async Task<HttpResult> TransferMainToSubAccountAsync(string clientOrderId, string asset, decimal quantity, string subAccount, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("requestNo", clientOrderId);
            parameters.Add("currency", asset);
            parameters.Add("amount", quantity);
            parameters.Add("subAccount", subAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/account/sub-account/main/v1/main-to-sub", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer Sub Account To Sub Account

        /// <inheritdoc />
        public async Task<HttpResult> TransferSubAccountToSubAccountAsync(string clientOrderId, decimal quantity, string asset, string fromAccount, string toAccount, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("requestNo", clientOrderId);
            parameters.Add("quantity", quantity);
            parameters.Add("currency", asset);
            parameters.Add("fromAccount", fromAccount);
            parameters.Add("toAccount", toAccount);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/account/sub-account/main/v1/sub-to-sub", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Sub Account Transfer History For Main

        /// <inheritdoc />
        public async Task<HttpResult<SubAccountTransferHistory>> GetSubAccountTransferHistoryForMainAsync(int limit, string? account = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("N", limit);
            parameters.Add("accountName", account);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/account/sub-account/main/v1/transfer-list", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<SubAccountTransferHistory>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Sub Account Transfer History

        /// <inheritdoc />
        public async Task<HttpResult<SubAccountTransferHistory>> GetSubAccountTransferHistoryAsync(int limit, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("N", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/account/sub-account/v1/transfer-history", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<SubAccountTransferHistory>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Sub Acccount Balance

        /// <inheritdoc />
        public async Task<HttpResult<BitMartSubAccountBalance[]>> GetSubAccountBalanceAsync(string subAccount, string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("subAccount", subAccount);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/account/sub-account/main/v1/wallet", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartSubAccountBalanceWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartSubAccountBalance[]>(result);

            return HttpResult.Ok(result, result.Data.Wallet);
        }

        #endregion

        #region Get Sub Account List

        /// <inheritdoc />
        public async Task<HttpResult<BitMartSubAccount[]>> GetSubAccountListAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/account/sub-account/main/v1/subaccount-list", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartSubAccountWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartSubAccount[]>(result);

            return HttpResult.Ok(result, result.Data.SubAccountList);
        }

        #endregion

    }
}
