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
        #region Deposit client

        public GetDepositAddressesOptions GetDepositAddressesOptions { get; } = new GetDepositAddressesOptions(_exchangeName, true);
        public async Task<HttpResult<SharedDepositAddress[]>> GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = GetDepositAddressesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, validationError);

            var assetName = request.Asset;
            if (request.Network != null && request.Network != request.Asset)
                assetName += "-" + request.Network;

            var depositAddresses = await _api.Account.GetDepositAddressAsync(assetName).ConfigureAwait(false);
            if (!depositAddresses.Success)
                return HttpResult.Fail<SharedDepositAddress[]>(depositAddresses);

            return HttpResult.Ok(depositAddresses, new[] { new SharedDepositAddress(depositAddresses.Data.Asset.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0], depositAddresses.Data.Address)
            {
                TagOrMemo = depositAddresses.Data.AddressMemo,
                Network = depositAddresses.Data.Network
            }
            });
        }

        Task<HttpResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? nextPageToken, CancellationToken ct)
            => GetDepositHistoryAsync(request, nextPageToken, ct);
        GetDepositHistoryOptions IDepositRestClient.GetDepositsOptions => GetDepositHistoryOptions;

        public GetDepositHistoryOptions GetDepositHistoryOptions { get; } = new GetDepositHistoryOptions(_exchangeName, false, true, true, 1000);
        public async Task<HttpResult<SharedDeposit[]>> GetDepositHistoryAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetDepositHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDeposit[]>(Exchange, validationError);

            var direction = DataDirection.Descending;
            var limit = request.Limit ?? 1000;
            var maxTimespan = TimeSpan.FromDays(90) - TimeSpan.FromSeconds(1);
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, maxPeriod: maxTimespan);

            // Get data
            var result = await _api.Account.GetDepositHistoryAsync(
                asset: request.Asset,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedDeposit[]>(result);

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
                            new SharedDeposit(
                                x.Asset.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0],
                                x.ArrivalQuantity, 
                                x.Status == DepositWithdrawalStatus.Completed,
                                x.ApplyTime,
                                ParseTransferStatus(x.Status))
                            {
                                Id = x.DepositId!,
                                Tag = x.AddressMemo,
                                TransactionId = x.TransactionId,
                            })
                       .ToArray(), nextPageRequest);
        }

        private SharedTransferStatus ParseTransferStatus(DepositWithdrawalStatus status)
        {
            if (status == DepositWithdrawalStatus.Completed)
                return SharedTransferStatus.Completed;
            if (status == DepositWithdrawalStatus.Failed || status == DepositWithdrawalStatus.Canceled)
                return SharedTransferStatus.Failed;
            if (status == DepositWithdrawalStatus.Submitted || status == DepositWithdrawalStatus.Created || status == DepositWithdrawalStatus.Processing)
                return SharedTransferStatus.InProgress;

            return SharedTransferStatus.Unknown;
        }

        #endregion
    }
}
