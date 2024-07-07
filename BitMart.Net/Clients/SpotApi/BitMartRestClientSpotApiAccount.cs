using CryptoExchange.Net.Objects;
using BitMart.Net.Clients.SpotApi;
using BitMart.Net.Interfaces.Clients.SpotApi;
using System.Threading.Tasks;
using System.Collections.Generic;
using BitMart.Net.Objects.Models;
using System.Threading;
using System.Net.Http;

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
        public async Task<WebCallResult<IEnumerable<BitMartBalance>>> GetFundingBalancesAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/account/v1/wallet", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<BitMartBalanceWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartBalance>>(result.Data?.Wallet);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartSpotBalance>>> GetSpotBalancesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/v1/wallet", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<BitMartSpotBalanceWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartSpotBalance>>(result.Data?.Wallet);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartDepositAddress>> GetDepositAddressAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/account/v1/deposit/address", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<BitMartDepositAddress>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartWithdrawalQuota>> GetWithdrawalQuotaAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "account/v1/withdraw/charge", BitMartExchange.RateLimiter.BitMart, 1, true);
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
            parameters.AddOptionalEnum("type", accountDestType);
            parameters.AddOptional("value", targetAccount);
            parameters.AddOptional("areaCode", areaCode);
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/account/v1/withdraw/apply", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<BitMartWithdrawId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }


    }
}
