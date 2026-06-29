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
        public async Task<HttpResult<BitMartBorrowId>> BorrowAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("currency", asset);
            parameters.Add("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/spot/v1/margin/isolated/borrow", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartBorrowId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Repay

        /// <inheritdoc />
        public async Task<HttpResult<BitMartRepayId>> RepayAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("currency", asset);
            parameters.Add("amount", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/spot/v1/margin/isolated/repay", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BitMartRepayId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Borrow History

        /// <inheritdoc />
        public async Task<HttpResult<BitMartBorrowRecord[]>> GetBorrowHistoryAsync(string symbol, string? borrowId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("borrow_id", borrowId);
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("N", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/spot/v1/margin/isolated/borrow_record", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BorrowRecordWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartBorrowRecord[]>(result);

            return HttpResult.Ok(result, result.Data.Records);
        }

        #endregion

        #region Get Repay History

        /// <inheritdoc />
        public async Task<HttpResult<BitMartRepayRecord[]>> GetRepayHistoryAsync(string symbol, string? asset = null, string? repayId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("currency", asset);
            parameters.Add("repay_id", repayId);
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("N", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/spot/v1/margin/isolated/repay_record", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(60, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<RepayRecordWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartRepayRecord[]>(result);

            return HttpResult.Ok(result, result.Data.Records);
        }

        #endregion

        #region Get Borrow Info

        /// <inheritdoc />
        public async Task<HttpResult<BitMartBorrowInfo[]>> GetBorrowInfoAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(BitMartExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/spot/v1/margin/isolated/pairs", BitMartExchange.RateLimiter.BitMart, 1, true,
                new SingleLimitGuard(2, TimeSpan.FromSeconds(2), RateLimitWindowType.Sliding, keySelector: SingleLimitGuard.PerApiKey));
            var result = await _baseClient.SendAsync<BorrowInfoWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<BitMartBorrowInfo[]>(result);

            return HttpResult.Ok(result, result.Data.Symbols);
        }

        #endregion


    }
}
