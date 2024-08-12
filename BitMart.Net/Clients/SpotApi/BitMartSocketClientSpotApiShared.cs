using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.Models.Socket;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using CryptoExchange.Net.SharedApis.SubscribeModels;
using GateIo.Net.Interfaces.Clients.SpotApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BitMart.Net.Clients.SpotApi
{
    internal partial class BitMartSocketClientSpotApi : IBitMartSocketClientSpotApiShared
    {
        public string Exchange => BitMartExchange.ExchangeName;

        async Task<CallResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(TickerSubscribeRequest request, Action<DataEvent<SharedTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToTickerUpdatesAsync(symbol, update => handler(update.As(new SharedTicker(update.Data.Symbol, update.Data.LastPrice, update.Data.HighPrice, update.Data.LowPrice))), ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(TradeSubscribeRequest request, Action<DataEvent<IEnumerable<SharedTrade>>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToTradeUpdatesAsync(symbol, update => handler(update.As(update.Data.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)))), ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(BookTickerSubscribeRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);
            var result = await SubscribeToTickerUpdatesAsync(symbol, update => handler(update.As(new SharedBookTicker(update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity))), ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SharedRequest request, Action<DataEvent<IEnumerable<SharedBalance>>> handler, CancellationToken ct)
        {
            var result = await SubscribeToBalanceUpdatesAsync(
                update => handler(update.As(update.Data.Balances.Select(x => new SharedBalance(x.Asset, x.Available, x.Available + x.Frozen)))),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        async Task<CallResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToOrderUpdatesAsync(SharedRequest request, Action<DataEvent<IEnumerable<SharedSpotOrder>>> handler, CancellationToken ct)
        {
            var result = await SubscribeToOrderUpdatesAsync(
                update => handler(update.As<IEnumerable<SharedSpotOrder>>(new[] {
                    new SharedSpotOrder(
                        update.Data.Symbol,
                        update.Data.OrderId.ToString(),
                        update.Data.OrderType == Enums.OrderType.Limit ? CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Limit : update.Data.OrderType == Enums.OrderType.Market ? CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Market : CryptoExchange.Net.SharedApis.Enums.SharedOrderType.Other,
                        update.Data.Side == Enums.OrderSide.Buy ? CryptoExchange.Net.SharedApis.Enums.SharedOrderSide.Buy : CryptoExchange.Net.SharedApis.Enums.SharedOrderSide.Sell,
                        update.Data.Status == Enums.OrderStatus.Canceled ? CryptoExchange.Net.SharedApis.Enums.SharedOrderStatus.Canceled : update.Data.Status == Enums.OrderStatus.New ? CryptoExchange.Net.SharedApis.Enums.SharedOrderStatus.Open : update.Data.Status == Enums.OrderStatus.PartiallyFilled ? CryptoExchange.Net.SharedApis.Enums.SharedOrderStatus.PartiallyFilled : CryptoExchange.Net.SharedApis.Enums.SharedOrderStatus.Filled,
                        update.Data.CreateTime)
                    {
                        ClientOrderId = update.Data.ClientOrderId?.ToString(),
                        Quantity = update.Data.Quantity,
                        QuantityFilled = update.Data.QuantityFilled,
                        QuoteQuantity = update.Data.QuoteQuantity,
                        QuoteQuantityFilled = update.Data.QuoteQuantityFilled,
                        TimeInForce = update.Data.EntrustType == Enums.EntrustType.ImmediateOrCancel ? CryptoExchange.Net.SharedApis.Enums.SharedTimeInForce.ImmediateOrCancel : CryptoExchange.Net.SharedApis.Enums.SharedTimeInForce.GoodTillCanceled,
                        UpdateTime = update.Data.UpdateTime,
                        Price = update.Data.Price,
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return result;
        }
    }
}
