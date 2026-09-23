using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    internal partial class BitMartSocketClientUsdFuturesSharedApi
    {
        #region Subscribe To Futures Order Updates

        async Task<WebSocketResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
            => await SubscribeToFuturesOrderUpdatesAsync(request, x => handler(x.ToType<SharedFuturesOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeFuturesOrderOptions SubscribeFuturesOrderOptions { get; } = new SubscribeFuturesOrderOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToOrderUpdatesAsync(
                update => handler(update.ToType<SharedFuturesOrderUpdate[]>(update.Data.Select(x => 
                    new SharedFuturesOrderUpdate(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Order.Symbol),
                        x.Order.Symbol,
                        x.Order.OrderId.ToString(),
                        ParseOrderType(x.Order.OrderType, x.Order.Price),
                        (x.Order.Side == Enums.FuturesSide.BuyCloseShort || x.Order.Side == Enums.FuturesSide.BuyOpenLong) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(x.Order.Status, x.Order.Quantity - x.Order.QuantityFilled),
                        x.Order.CreateTime)
                    {
                        ClientOrderId = x.Order.ClientOrderId?.ToString(),
                        OrderQuantity = new SharedOrderQuantity(contractQuantity: x.Order.Quantity),
                        QuantityFilled = new SharedOrderQuantity(contractQuantity: x.Order.QuantityFilled),
                        AveragePrice = x.Order.AveragePrice == 0 ? null : x.Order.AveragePrice,
                        UpdateTime = x.Order.UpdateTime,
                        OrderPrice = x.Order.Price,
                        Leverage = x.Order.Leverage,
                        TriggerPrice = x.Order.TriggerPrice,
                        IsTriggerOrder = x.Order.TriggerPrice > 0,
                        PositionSide = (x.Order.Side == Enums.FuturesSide.SellCloseLong || x.Order.Side == Enums.FuturesSide.BuyOpenLong) ? SharedPositionSide.Long : SharedPositionSide.Short,
                        LastTrade = x.Order.LastTrade == null ? null :
                            new SharedUserTrade(ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, x.Order.Symbol), 
                            x.Order.Symbol, 
                            x.Order.OrderId,
                            x.Order.LastTrade.TradeId.ToString(), 
                            (x.Order.Side == Enums.FuturesSide.BuyCloseShort || x.Order.Side == Enums.FuturesSide.BuyOpenLong) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                            new SharedOrderQuantity(x.Order.LastTrade.Quantity),
                            x.Order.LastTrade.Price,
                            x.Order.UpdateTime!.Value)
                            {
                                Fee = x.Order.LastTrade.Fee,
                                FeeAsset = x.Order.LastTrade.FeeAsset,
                                ClientOrderId = x.Order.ClientOrderId
                            }
                    }
                ).ToArray())),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        private SharedOrderStatus ParseOrderStatus(Enums.FuturesOrderStatus status, decimal remainingQuantity)
        {
            if (status == Enums.FuturesOrderStatus.Approval || status == Enums.FuturesOrderStatus.Check) return SharedOrderStatus.Open;
            if (status != Enums.FuturesOrderStatus.Finish)
                return SharedOrderStatus.Unknown;

            if (remainingQuantity > 0) return SharedOrderStatus.Canceled;
            return SharedOrderStatus.Filled;
        }

        private SharedOrderType ParseOrderType(Enums.FuturesOrderType type, decimal? orderPrice)
        {
            if (type == Enums.FuturesOrderType.Market) return SharedOrderType.Market;
            if (type == Enums.FuturesOrderType.Limit) return SharedOrderType.Limit;
            if (type == Enums.FuturesOrderType.PlanOrder) return orderPrice > 0 ? SharedOrderType.Limit : SharedOrderType.Market;
            return SharedOrderType.Other;
        }
    }
}
