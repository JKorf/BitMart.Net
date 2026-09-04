using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Enums;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using BitMart.Net.Objects.Models;
using CryptoExchange.Net.Objects.Errors;
using Microsoft.Extensions.Logging;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    internal partial class BitMartRestClientUsdFuturesSharedApi
    {
        #region Place Futures Trigger Order

        public PlaceFuturesTriggerOrderOptions PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(_exchangeName, false)
        {
        };

        async Task<ICallResult<SharedId>> IPlaceFuturesTriggerOrder.PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
            => await PlaceFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedId>> PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var side = GetFuturesSide(request);
            var validationError = PlaceFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceTriggerOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderPrice == null ? OrderType.Market : OrderType.Limit,
                side,
                quantity: (int)(request.Quantity?.QuantityInContracts ?? 0),
                request.Leverage ?? 1,
                request.MarginMode == SharedMarginMode.Isolated ? MarginType.IsolatedMargin : MarginType.CrossMargin,
                request.TriggerPrice,
                request.PriceDirection == SharedTriggerPriceDirection.PriceAbove ? PriceDirection.LongDirection : PriceDirection.ShortDirection,
                request.TriggerPriceType == null || request.TriggerPriceType == SharedTriggerPriceType.LastPrice ? TriggerPriceType.LastPrice: TriggerPriceType.FairPrice,
                orderPrice: request.OrderPrice,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.OrderId.ToString()));
        }

        #endregion

        #region Get Futures Trigger Order

        public GetFuturesTriggerOrderOptions GetFuturesTriggerOrderOptions { get; } = new GetFuturesTriggerOrderOptions(_exchangeName, true)
        {
            RequestNotes = "Only pending trigger orders can be requested, executed trigger orders are not available in the API"
        };
        async Task<ICallResult<SharedFuturesTriggerOrder>> IGetFuturesTriggerOrder.GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedFuturesTriggerOrder>> GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, validationError);

            var orders = await _api.Trading.GetTriggerOrdersAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                planType: TriggerPlanType.Plan,
                ct: ct).ConfigureAwait(false);
            if (!orders.Success)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(orders);

            var order = orders.Data.SingleOrDefault(x => x.OrderId == request.OrderId);
            if (order == null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownOrder, "Order not found")));

            return HttpResult.Ok(orders, new SharedFuturesTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Symbol),
                order.Symbol,
                order.OrderId.ToString(),
                order.OrderPrice > 0 ? SharedOrderType.Limit : SharedOrderType.Market,
                order.Side == FuturesSide.BuyOpenLong || order.Side == FuturesSide.SellOpenShort ? SharedTriggerOrderDirection.Enter: SharedTriggerOrderDirection.Exit,
                SharedTriggerOrderStatus.Active,
                order.TriggerPrice,
                order.Side == FuturesSide.SellCloseLong || order.Side == FuturesSide.BuyOpenLong ? SharedPositionSide.Long : SharedPositionSide.Short,
                order.CreateTime)
            {
                OrderPrice = order.OrderPrice == 0 ? null : order.OrderPrice,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Quantity),
                QuantityFilled = new SharedOrderQuantity(contractQuantity: 0),
                UpdateTime = order.UpdateTime,
                ClientOrderId = order.ClientOrderId
            });
        }

        #endregion

        #region Cancel Futures Trigger Order

        public CancelFuturesTriggerOrderOptions CancelFuturesTriggerOrderOptions { get; } = new CancelFuturesTriggerOrderOptions(_exchangeName, true);
        async Task<ICallResult<SharedId>> ICancelFuturesTriggerOrder.CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public async Task<HttpResult<SharedId>> CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var order = await _api.Trading.CancelTriggerOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                request.OrderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion

        private FuturesSide GetFuturesSide(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.OrderDirection == SharedTriggerOrderDirection.Enter)
            {
                if (request.PositionSide == SharedPositionSide.Long)
                    return FuturesSide.BuyOpenLong;
                return FuturesSide.SellOpenShort;
            }

            if (request.PositionSide == SharedPositionSide.Long)
                return FuturesSide.SellCloseLong;
            return FuturesSide.BuyCloseShort;
        }
    }
}
