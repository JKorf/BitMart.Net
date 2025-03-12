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
    internal class BitMartRestClientSpotApiAccount : IBitMartRestClientSpotApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitMartRestClientSpotApi _baseClient;

        internal BitMartRestClientSpotApiAccount(BitMartRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartBalance>>> GetFundingBalancesAsync(string? asset = null, bool? needUsdValuation = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("needUsdValuation", needUsdValuation);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/account/v1/wallet", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartBalanceWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartBalance>>(result.Data?.Wallet);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartSpotBalance>>> GetSpotBalancesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/v1/wallet", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartSpotBalanceWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartSpotBalance>>(result.Data?.Wallet);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartDepositAddress>> GetDepositAddressAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/account/v1/deposit/address", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartDepositAddress>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartWithdrawalQuota>> GetWithdrawalQuotaAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "account/v1/withdraw/charge", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartWithdrawalQuota>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartWithdrawId>> WithdrawAsync(string asset, decimal quantity, string? targetAddress = null, string? memo = null, string? remark = null, string? accountDestType = null, string? targetAccount = null, string? areaCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("address", targetAddress);
            parameters.AddOptional("address_memo", memo);
            parameters.AddOptional("destination", remark);
            parameters.AddOptional("type", accountDestType);
            parameters.AddOptional("value", targetAccount);
            parameters.AddOptional("areaCode", areaCode);
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/account/v1/withdraw/apply", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartWithdrawId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartDepositWithdrawal>>> GetDepositHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("operation_type", "deposit");
            parameters.Add("N", limit ?? 100);
            parameters.AddOptional("currency", asset);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "account/v2/deposit-withdraw/history", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartDepositWithdrawalHistoryWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartDepositWithdrawal>>(result.Data?.Records);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartDepositWithdrawal>>> GetWithdrawalHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("operation_type", "withdraw");
            parameters.Add("N", limit ?? 100);
            parameters.AddOptional("currency", asset);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "account/v2/deposit-withdraw/history", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartDepositWithdrawalHistoryWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartDepositWithdrawal>>(result.Data?.Records);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartDepositWithdrawal>> GetDepositWithdrawalAsync(string id, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("id", id);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "account/v1/deposit-withdraw/detail", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(8, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartDepositWithdrawalWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<BitMartDepositWithdrawal>(result.Data?.Record);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartIsolatedMarginAccount>>> GetIsolatedMarginAccountsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/v1/margin/isolated/account", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(12, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartIsolatedMarginAccountWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartIsolatedMarginAccount>>(result.Data?.Symbols);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartTransferId>> IsolatedMarginTransferAsync(string symbol, string asset, decimal quantity, TransferDirection direction, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            parameters.AddEnum("side", direction);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/v1/margin/isolated/transfer", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartTransferId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartFeeRate>> GetBaseTradeFeesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/v1/user_fee", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartFeeRate>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartSymbolTradeFee>> GetSymbolTradeFeeAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/v1/trade_fee", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartSymbolTradeFee>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartWithdrawalAddress>>> GetWithdrawalAddressesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/account/v1/withdraw/address/list", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartWithdrawalAddressesWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartWithdrawalAddress>>(result.Data?.List);
        }

    }
}
