using CryptoExchange.Net.Objects;
using BitMart.Net.Clients.UsdFuturesApi;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using BitMart.Net.Objects.Models;
using System;

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
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/contract/private/assets-detail", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<IEnumerable<BitMartFuturesBalance>>(request, parameters, ct).ConfigureAwait(false);
            return result;
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
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/account/v1/transfer-contract-list", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<BitMartFuturesTransferWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartFuturesTransfer>>(result.Data?.Records);
        }

        #endregion

    }
}
