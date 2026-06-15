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
    internal partial class BitMartSocketClientUsdFuturesApi : IBitMartSocketClientUsdFuturesApiShared
    {
        private const string _topicId = "BitMartFutures";
        private const string _exchangeName = "BitMart";
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.PerpetualLinear, TradingMode.DeliveryLinear };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();
        public SharedClientInfo Discover() => SharedUtils.GetClientInfo(this);

        #region Tickers client
        SubscribeTickersOptions ITickersSocketClient.SubscribeAllTickersOptions { get; } = new SubscribeTickersOptions(_exchangeName);
        async Task<WebSocketResult<UpdateSubscription>> ITickersSocketClient.SubscribeToAllTickersUpdatesAsync(SubscribeAllTickersRequest request, Action<DataEvent<SharedSpotTicker[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeAllTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToTickerUpdatesAsync(update => handler(update.ToType<SharedSpotTicker[]>(new[] { new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.Symbol), update.Data.Symbol, update.Data.LastPrice, null, null, update.Data.Volume24h, Math.Round(update.Data.PriceRange * 100, 2)) })), ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Ticker client
        SubscribeTickerOptions ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscribeTickerOptions(_exchangeName)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 75
        };
        async Task<WebSocketResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<DataEvent<SharedSpotTicker>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToTickerUpdatesAsync(symbols, update =>
            {
                handler(update.ToType(new SharedSpotTicker(ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.Symbol), update.Data.Symbol, update.Data.LastPrice, null, null, update.Data.Volume24h, Math.Round(update.Data.PriceRange * 100, 2))));
            }, ct).ConfigureAwait(false);            

            return result;
        }

        #endregion

        #region Trade client

        SubscribeTradeOptions ITradeSocketClient.SubscribeTradeOptions { get; } = new SubscribeTradeOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 80
        };
        async Task<WebSocketResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToTradeUpdatesAsync(symbols, update => handler(update.ToType(update.Data.Select(x =>
                new SharedTrade(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, x.Quantity, x.Price, x.Timestamp) { Side = x.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy }).ToArray())), ct).ConfigureAwait(false);
            
            return result;
        }
        #endregion

        #region Book Ticker client

        SubscribeBookTickerOptions IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new SubscribeBookTickerOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 65
        };
        async Task<WebSocketResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<DataEvent<SharedBookTicker>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeBookTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToBookTickerUpdatesAsync(symbols, update =>
            {
                handler(update.ToType(new SharedBookTicker(ExchangeSymbolCache.ParseSymbol(_topicId, update.Data.Symbol), update.Data.Symbol, update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity)));
            }, ct).ConfigureAwait(false);
            
            return result;
        }
        #endregion

        #region Balance client
        SubscribeBalanceOptions IBalanceSocketClient.SubscribeBalanceOptions { get; } = new SubscribeBalanceOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(SubscribeBalancesRequest request, Action<DataEvent<SharedBalance[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeBalanceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToBalanceUpdatesAsync(
                update => handler(update.ToType<SharedBalance[]>(new[] { new SharedBalance(update.Data.Asset, update.Data.Available, update.Data.Available + update.Data.Frozen) })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(_exchangeName, false,
            SharedKlineInterval.OneMinute,
            SharedKlineInterval.ThreeMinutes,
            SharedKlineInterval.FiveMinutes,
            SharedKlineInterval.FifteenMinutes,
            SharedKlineInterval.ThirtyMinutes,
            SharedKlineInterval.OneHour,
            SharedKlineInterval.TwoHours,
            SharedKlineInterval.FourHours,
            SharedKlineInterval.OneDay,
            SharedKlineInterval.OneWeek)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 65
        };
        async Task<WebSocketResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<DataEvent<SharedKline>> handler, CancellationToken ct)
        {
            var interval = (Enums.FuturesStreamKlineInterval)request.Interval;
            var validationError = SharedClient.SubscribeKlineOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToKlineUpdatesAsync(symbols, interval, update =>
            {
                foreach (var item in update.Data.Klines)
                    handler(update.ToType(new SharedKline(ExchangeSymbolCache.ParseSymbol(_topicId, update.Symbol), update.Symbol!, item.Timestamp!.Value, item.ClosePrice, item.HighPrice, item.LowPrice, item.OpenPrice, item.Volume)));
            }, ct).ConfigureAwait(false);
            
            return result;
        }
        #endregion

        #region Futures Order client

        SubscribeFuturesOrderOptions IFuturesOrderSocketClient.SubscribeFuturesOrderOptions { get; } = new SubscribeFuturesOrderOptions(_exchangeName, false);
        async Task<WebSocketResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToOrderUpdatesAsync(
                update => handler(update.ToType<SharedFuturesOrder[]>(update.Data.Select(x => 
                    new SharedFuturesOrder(
                        ExchangeSymbolCache.ParseSymbol(_topicId, x.Order.Symbol),
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
                        LastTrade = x.Order.LastTrade == null ? null : new SharedUserTrade(ExchangeSymbolCache.ParseSymbol(_topicId, x.Order.Symbol), x.Order.Symbol, x.Order.OrderId, x.Order.LastTrade.TradeId.ToString(), (x.Order.Side == Enums.FuturesSide.BuyCloseShort || x.Order.Side == Enums.FuturesSide.BuyOpenLong) ? SharedOrderSide.Buy : SharedOrderSide.Sell, x.Order.LastTrade.Quantity, x.Order.LastTrade.Price, x.Order.UpdateTime!.Value)
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
        #endregion

        #region Position client
        SubscribePositionOptions IPositionSocketClient.SubscribePositionOptions { get; } = new SubscribePositionOptions(_exchangeName, true);
        async Task<WebSocketResult<UpdateSubscription>> IPositionSocketClient.SubscribeToPositionUpdatesAsync(SubscribePositionRequest request, Action<DataEvent<SharedPosition[]>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribePositionOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await SubscribeToPositionUpdatesAsync(
                update => handler(update.ToType(update.Data.Select(x => new SharedPosition(ExchangeSymbolCache.ParseSymbol(_topicId, x.Symbol), x.Symbol, x.PositionSize, x.UpdateTime)
                {
                    AverageOpenPrice = x.AverageOpenPrice,
                    PositionMode = SharedPositionMode.HedgeMode,
                    PositionSide = x.PositionSide == Enums.PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                    LiquidationPrice = x.LiquidationPrice == 0 ? null : x.LiquidationPrice
                }).ToArray())),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        #region Order Book client
        SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, new[] { 5, 20, 50 })
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 65
        };
        async Task<WebSocketResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SharedClient.SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await SubscribeToOrderBookSnapshotUpdatesAsync(symbols, request.Limit ?? 20, update => handler(update.ToType(new SharedOrderBook(update.Data.Asks, update.Data.Bids))), ct).ConfigureAwait(false);
            
            return result;
        }
        #endregion
    }
}
