using BitMart.Net.Enums;
using BitMart.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BitMart.Net.Clients.SpotApi
{
    internal partial class BitMartSocketClientSpotSharedApi
    {
        #region Subscribe To Spot Order Updates

        async Task<WebSocketResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
            => await SubscribeToSpotOrderUpdatesAsync(request, x => handler(x.ToType<SharedSpotOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeSpotOrderOptions SubscribeSpotOrderOptions { get; } = new SubscribeSpotOrderOptions(_exchangeName, false);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToOrderUpdatesAsync(
                update => handler(update.ToType<SharedSpotOrderUpdate[]>(new[] {
                    new SharedSpotOrderUpdate(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                        update.Data.Symbol,
                        update.Data.OrderId.ToString(),
                        update.Data.OrderType == Enums.OrderType.Limit ? SharedOrderType.Limit : update.Data.OrderType == Enums.OrderType.Market ? SharedOrderType.Market : SharedOrderType.Other,
                        update.Data.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(update.Data.Status),
                        update.Data.CreateTime)
                    {
                        ClientOrderId = update.Data.ClientOrderId?.ToString(),
                        OrderQuantity = new SharedOrderQuantity(update.Data.Quantity == 0 ? null :update.Data.Quantity, update.Data.QuoteQuantity),
                        QuantityFilled = new SharedOrderQuantity(update.Data.QuantityFilled, update.Data.QuoteQuantityFilled),
                        TimeInForce = update.Data.EntrustType == Enums.EntrustType.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : SharedTimeInForce.GoodTillCanceled,
                        UpdateTime = update.Data.UpdateTime,
                        OrderPrice = update.Data.Price == 0 ? null : update.Data.Price,
#pragma warning disable CS0618 // Type or member is obsolete
                        FeeAsset = update.Data.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                        LastTrade = update.Data.LastTradeId == null ? null : 
                            new SharedUserTrade(
                                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol), 
                                update.Data.Symbol,
                                update.Data.OrderId,
                                update.Data.LastTradeId,
                                update.Data.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                new SharedOrderQuantity(update.Data.LastTradeQuantity), 
                                update.Data.LastTradePrice,
                                update.Data.LastTradeTime!.Value)
                            {
                                ClientOrderId = update.Data.ClientOrderId,
                                Role = update.Data.LastTradeRole == Enums.TradeRole.Taker ? SharedRole.Taker : SharedRole.Maker,
                                Fee = update.Data.Fee,
                                FeeAsset = update.Data.FeeAsset
                            }
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.PartiallyFilled || status == OrderStatus.New) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled || status == OrderStatus.PartiallyCanceled || status == OrderStatus.Failed) return SharedOrderStatus.Canceled;
            if (status == OrderStatus.Filled) return SharedOrderStatus.Filled;

            return SharedOrderStatus.Unknown;
        }
    }
}
