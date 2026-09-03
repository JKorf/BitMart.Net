using BitMart.Net.Enums;
using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BitMart.Net.Clients.SpotApi
{
    internal partial class BitMartRestClientSpotSharedApi
    {
        #region Withdrawal client

        Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? nextPageToken, CancellationToken ct)
            => GetWithdrawalHistoryAsync(request, nextPageToken, ct);
        GetWithdrawalHistoryOptions IWithdrawalRestClient.GetWithdrawalsOptions => GetWithdrawalHistoryOptions;

        public GetWithdrawalHistoryOptions GetWithdrawalHistoryOptions { get; } = new GetWithdrawalHistoryOptions(_exchangeName, false, true, true, 1000);
        public async Task<HttpResult<SharedWithdrawal[]>> GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetWithdrawalHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var limit = request.Limit ?? 1000;
            var maxTimespan = TimeSpan.FromDays(90) - TimeSpan.FromSeconds(1);
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: maxTimespan);

            // Get data
            var result = await _api.Account.GetWithdrawalHistoryAsync(
                asset: request.Asset,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedWithdrawal[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
               () => Pagination.NextPageFromTime(pageParams, result.Data.Min(x => x.ApplyTime)),
               result.Data.Length,
               result.Data.Select(x => x.ApplyTime),
               request.StartTime,
               request.EndTime ?? DateTime.UtcNow,
               pageParams,
               maxTimespan);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.ApplyTime, request.StartTime, request.EndTime, direction)
                       .Select(x => 
                           new SharedWithdrawal(
                               x.Asset.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0],
                               x.Address!,
                               x.ArrivalQuantity,
                               x.Status == DepositWithdrawalStatus.Completed,
                               x.ApplyTime,
                               GetWithdrawalStatus(x))
                            {
                                Network = x.Asset.Split('-')[1],
                                Id = x.WithdrawId!,
                                Tag = x.AddressMemo,
                                TransactionId = x.TransactionId,
                                Fee = x.Fee
                            })
                       .ToArray(), nextPageRequest);
        }

        private SharedTransferStatus GetWithdrawalStatus(BitMartDepositWithdrawal x)
        {
            if (x.Status == DepositWithdrawalStatus.Canceled || x.Status == DepositWithdrawalStatus.Failed)
                return SharedTransferStatus.Failed;

            if (x.Status == DepositWithdrawalStatus.Completed)
                return SharedTransferStatus.Completed;

            if (x.Status == DepositWithdrawalStatus.Created || x.Status == DepositWithdrawalStatus.Processing || x.Status == DepositWithdrawalStatus.Submitted)
                return SharedTransferStatus.InProgress;

            return SharedTransferStatus.Unknown;
        }

        #endregion

        #region Withdraw client

        public WithdrawOptions WithdrawOptions { get; } = new WithdrawOptions(_exchangeName);

        public async Task<HttpResult<SharedId>> WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var assetName = request.Asset;
            if (request.Network != null && request.Network != request.Asset)
                assetName += "-" + request.Network;

            // Get data
            var withdrawal = await _api.Account.WithdrawAsync(
                assetName,
                request.Quantity,
                request.Address,
                memo: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal.Success)
                return HttpResult.Fail<SharedId>(withdrawal);

            return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data.WithdrawId));
        }

        #endregion
    }
}
