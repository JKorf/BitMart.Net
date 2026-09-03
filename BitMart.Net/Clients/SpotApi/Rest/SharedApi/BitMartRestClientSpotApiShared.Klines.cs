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
        #region Kline client

        public GetKlinesOptions GetKlinesOptions { get; } = new GetKlinesOptions(_exchangeName, true, false, true, 200, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.TwoHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.OneWeek,
            SharedKlineInterval.OneMonth);

        public async Task<HttpResult<SharedKline[]>> GetKlinesAsync(GetKlinesRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            var validationError = GetKlinesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedKline[]>(Exchange, validationError);

            var direction = DataDirection.Ascending;
            var symbol = request.Symbol!.GetSymbol(FormatSymbol);
            var limit = request.Limit ?? 200;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, true);

            // Get data
            HttpResult<BitMartKline[]> result;
            if ((DateTime.UtcNow - pageParams.EndTime)?.TotalSeconds < (int)interval)
            {
                result = await _api.ExchangeData.GetKlinesAsync(
                    symbol,
                    interval,
                    startTime: pageParams.StartTime?.AddSeconds((int)interval),
                    limit: limit,
                    ct: ct
                    ).ConfigureAwait(false);
            }
            else
            {
                result = await _api.ExchangeData.GetKlineHistoryAsync(
                    symbol,
                    interval,
                    startTime: pageParams.StartTime!.Value.AddSeconds((int)interval),
                    endTime: pageParams.EndTime,
                    limit,
                    ct: ct
                    ).ConfigureAwait(false);
            }
            if (!result.Success)
                return HttpResult.Fail<SharedKline[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                    () => Pagination.NextPageFromTime(pageParams, result.Data.Max(x => x.OpenTime)),
                    result.Data.Length,
                    result.Data.Select(x => x.OpenTime),
                    request.StartTime,
                    request.EndTime ?? DateTime.UtcNow,
                    pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data, x => x.OpenTime, request.StartTime, request.EndTime, direction)
                   .Select(x =>
                        new SharedKline(
                            request.Symbol, 
                            symbol, 
                            x.OpenTime, 
                            x.ClosePrice, 
                            x.HighPrice, 
                            x.LowPrice, 
                            x.OpenPrice, 
                            new SharedOrderQuantity(x.Volume, x.QuoteVolume)))
                   .ToArray(), nextPageRequest);
        }

        #endregion
    }
}
