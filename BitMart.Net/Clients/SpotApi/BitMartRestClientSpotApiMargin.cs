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
    internal class BitMartRestClientSpotApiMargin : IBitMartRestClientSpotApiMargin
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitMartRestClientSpotApi _baseClient;

        internal BitMartRestClientSpotApiMargin(BitMartRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Borrow

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartBorrowId>> BorrowAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/spot/v1/margin/isolated/borrow", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartBorrowId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Repay

        /// <inheritdoc />
        public async Task<WebCallResult<BitMartRepayId>> RepayAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/spot/v1/margin/isolated/repay", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartRepayId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Borrow History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BorrowRecord>>> GetBorrowHistoryAsync(string symbol, string? borrowId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("borrow_id", borrowId);
            parameters.AddOptionalMilliseconds("start_time", startTime);
            parameters.AddOptionalMilliseconds("end_time", endTime);
            parameters.AddOptional("N", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/v1/margin/isolated/borrow_record", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BorrowRecordWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BorrowRecord>>(result.Data?.Records);
        }

        #endregion

        #region Get Repay History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<RepayRecord>>> GetRepayHistoryAsync(string symbol, string? asset = null, string? repayId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("currency", asset);
            parameters.AddOptional("repay_id", repayId);
            parameters.AddOptionalMillisecondsString("start_time", startTime);
            parameters.AddOptionalMillisecondsString("end_time", endTime);
            parameters.AddOptional("N", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/v1/margin/isolated/repay_record", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<RepayRecordWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<RepayRecord>>(result.Data?.Records);
        }

        #endregion

        #region Get Borrow Info

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BorrowInfo>>> GetBorrowInfoAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/v1/margin/isolated/pairs", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BorrowInfoWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BorrowInfo>>(result.Data?.Symbols);
        }

        #endregion


    }
}
