using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Enums;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BitMart.Net.Objects.Models;

namespace BitMart.Net.Clients.SpotApi
{
    internal partial class BitMartRestClientSpotApi : IBitMartRestClientSpotApiShared
    {
        public string Exchange => BitMartExchange.ExchangeName;
        public TradingMode[] SupportedTradingModes { get; } = new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();

        #region Kline client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationSupport.Descending, false)
        {
            MaxRequestDataPoints = 200
        };

        async Task<ExchangeWebResult<IEnumerable<SharedKline>>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, validationError);

            // Determine pagination
            // Data is normally returned oldest first, so to do newest first pagination we have to do some calc
            DateTime endTime = request.EndTime ?? DateTime.UtcNow;
            DateTime? startTime = request.StartTime;
            if (pageToken is DateTimeToken dateTimeToken)
                endTime = dateTimeToken.LastTime;

            var limit = request.Limit ?? 200;
            if (startTime == null || startTime < endTime)
            {
                var offset = (int)interval * limit;
                startTime = endTime.AddSeconds(-offset);
            }

            if (startTime < request.StartTime)
                startTime = request.StartTime;

            // Get data
            WebCallResult<IEnumerable<BitMartKline>> result;
            if ((DateTime.UtcNow - endTime).TotalSeconds < (int)interval)
            {
                result = await ExchangeData.GetKlinesAsync(
                    request.Symbol.GetSymbol(FormatSymbol),
                    interval,
                    startTime: startTime,
                    limit: limit,
                    ct: ct
                    ).ConfigureAwait(false);
            }
            else
            {
                result = await ExchangeData.GetKlineHistoryAsync(
                    request.Symbol.GetSymbol(FormatSymbol),
                    interval,
                    startTime,
                    endTime,
                    limit,
                    ct: ct
                    ).ConfigureAwait(false);
            }
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedKline>>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (result.Data.Count() == limit)
            {
                //var minOpenTime = result.Data.Min(x => x.OpenTime);
                //if (request.StartTime == null || minOpenTime > request.StartTime.Value)
                //    nextToken = new DateTimeToken(minOpenTime.AddSeconds(-(int)(interval - 1)));
                nextToken = new DateTimeToken(result.Data.Min(x => x.OpenTime));
            }

            return result.AsExchangeResult<IEnumerable<SharedKline>>(Exchange, request.Symbol.TradingMode, result.Data.Reverse().Select(x => new SharedKline(x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume)).ToArray(), nextToken);
        }

        #endregion

        #region Spot Symbol client

        EndpointOptions<GetSymbolsRequest> ISpotSymbolRestClient.GetSpotSymbolsOptions { get; } = new EndpointOptions<GetSymbolsRequest>(false);
        async Task<ExchangeWebResult<IEnumerable<SharedSpotSymbol>>> ISpotSymbolRestClient.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotSymbolRestClient)this).GetSpotSymbolsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotSymbol>>(Exchange, validationError);

            var result = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedSpotSymbol>>(Exchange, null, default);

            return result.AsExchangeResult<IEnumerable<SharedSpotSymbol>>(Exchange, TradingMode.Spot, result.Data.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Symbol, s.TradeStatus == SymbolStatus.Trading)
            {
                MinTradeQuantity = s.BaseMinQuantity,
                QuantityStep = s.BaseMinQuantity,
                PriceDecimals = s.PriceMaxPrecision,
            }).ToArray());
        }

        #endregion

        #region Ticker client

        EndpointOptions<GetTickerRequest> ISpotTickerRestClient.GetSpotTickerOptions { get; } = new EndpointOptions<GetTickerRequest>(false);
        async Task<ExchangeWebResult<SharedSpotTicker>> ISpotTickerRestClient.GetSpotTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickerOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotTicker>(Exchange, validationError);

            var result = await ExchangeData.GetTickerAsync(request.Symbol.GetSymbol(FormatSymbol), ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedSpotTicker>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotTicker(result.Data.Symbol, result.Data.LastPrice, result.Data.HighPrice, result.Data.LowPrice, result.Data.Volume24h, result.Data.Change * 100));
        }

        EndpointOptions<GetTickersRequest> ISpotTickerRestClient.GetSpotTickersOptions { get; } = new EndpointOptions<GetTickersRequest>(false);
        async Task<ExchangeWebResult<IEnumerable<SharedSpotTicker>>> ISpotTickerRestClient.GetSpotTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotTickerRestClient)this).GetSpotTickersOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotTicker>>(Exchange, validationError);

            var result = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedSpotTicker>>(Exchange, null, default);

            return result.AsExchangeResult<IEnumerable<SharedSpotTicker>>(Exchange, TradingMode.Spot, result.Data.Select(x => new SharedSpotTicker(x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice, x.Volume24h, x.Change * 100)).ToArray());
        }

        #endregion

        #region Recent Trade client
        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(50, false);

        async Task<ExchangeWebResult<IEnumerable<SharedTrade>>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var validationError = ((IRecentTradeRestClient)this).GetRecentTradesOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedTrade>>(Exchange, validationError);

            var result = await ExchangeData.GetTradesAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, null, default);

            return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, request.Symbol.TradingMode, result.Data.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)).ToArray());
        }

        #endregion

        #region Balance client
        EndpointOptions<GetBalancesRequest> IBalanceRestClient.GetBalancesOptions { get; } = new EndpointOptions<GetBalancesRequest>(true);

        async Task<ExchangeWebResult<IEnumerable<SharedBalance>>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, request, request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedBalance>>(Exchange, validationError);

            var result = await Account.GetSpotBalancesAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, null, default);

            return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, TradingMode.Spot, result.Data.Select(x => new SharedBalance(x.Id, x.Available, x.Available + x.Frozen)).ToArray());
        }

        #endregion

        #region Spot Order client

        SharedFeeAssetType ISpotOrderRestClient.SpotFeeAssetType => SharedFeeAssetType.QuoteAsset;
        SharedFeeDeductionType ISpotOrderRestClient.SpotFeeDeductionType => SharedFeeDeductionType.DeductFromOutput;
        IEnumerable<SharedOrderType> ISpotOrderRestClient.SpotSupportedOrderTypes { get; } = new[] { SharedOrderType.Limit, SharedOrderType.Market };
        IEnumerable<SharedTimeInForce> ISpotOrderRestClient.SpotSupportedTimeInForce { get; } = new[] { SharedTimeInForce.GoodTillCanceled, SharedTimeInForce.ImmediateOrCancel, SharedTimeInForce.FillOrKill };
        SharedQuantitySupport ISpotOrderRestClient.SpotSupportedOrderQuantity { get; } = new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAndQuoteAsset,
                SharedQuantityType.BaseAndQuoteAsset);

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions();
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.PlaceSpotOrderAsync(PlaceSpotOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).PlaceSpotOrderOptions.ValidateRequest(
                Exchange,
                request,
                request.Symbol.TradingMode,
                SupportedTradingModes,
                ((ISpotOrderRestClient)this).SpotSupportedOrderTypes,
                ((ISpotOrderRestClient)this).SpotSupportedTimeInForce,
                ((ISpotOrderRestClient)this).SpotSupportedOrderQuantity);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                GetOrderType(request.OrderType, request.TimeInForce),
                quantity: request.Quantity,
                quoteQuantity: request.QuoteQuantity,
                price: request.Price,
                clientOrderId: request.ClientOrderId,
                ct: ct).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(result.Data.OrderId));
        }

        EndpointOptions<GetOrderRequest> ISpotOrderRestClient.GetSpotOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderRestClient.GetSpotOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedSpotOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, TradingMode.Spot, new SharedSpotOrder(
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                ParseOrderType(order.Data.OrderType),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                Quantity = order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                QuoteQuantity = order.Data.QuoteQuantity,
                QuoteQuantityFilled = order.Data.QuoteQuantityFilled,
                AveragePrice = order.Data.PriceAverage == 0 ? null : order.Data.PriceAverage,
                UpdateTime = order.Data.UpdateTime,
                TimeInForce = ParseTimeInForce(order.Data.OrderType)
            });
        }

        EndpointOptions<GetOpenOrdersRequest> ISpotOrderRestClient.GetOpenSpotOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedSpotOrder>>> ISpotOrderRestClient.GetOpenSpotOrdersAsync(GetOpenOrdersRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetOpenSpotOrdersOptions.ValidateRequest(Exchange, request, request.Symbol?.TradingMode ?? request.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotOrder>>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var orders = await Trading.GetOpenOrdersAsync(symbol, ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, null, default);

            return orders.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, TradingMode.Spot, orders.Data.Select(x => new SharedSpotOrder(
                x.Symbol,
                x.OrderId.ToString(),
                ParseOrderType(x.OrderType),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                OrderPrice = x.Price == 0 ? null : x.Price,
                Quantity = x.Quantity,
                QuantityFilled = x.QuantityFilled,
                QuoteQuantity = x.QuoteQuantity,
                QuoteQuantityFilled = x.QuoteQuantityFilled,
                AveragePrice = x.PriceAverage == 0 ? null : x.PriceAverage,
                UpdateTime = x.UpdateTime,
                TimeInForce = ParseTimeInForce(x.OrderType)
            }).ToArray());
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> ISpotOrderRestClient.GetClosedSpotOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(SharedPaginationSupport.Descending, true);
        async Task<ExchangeWebResult<IEnumerable<SharedSpotOrder>>> ISpotOrderRestClient.GetClosedSpotOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetClosedSpotOrdersOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedSpotOrder>>(Exchange, validationError);

            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            // Get data
            var orders = await Trading.GetClosedOrdersAsync(request.Symbol.GetSymbol(FormatSymbol),
                SpotMode.Spot,
                startTime: request.StartTime,
                endTime: fromTimestamp ?? request.EndTime,
                limit: request.Limit ?? 200,
                ct: ct).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (orders.Data.Count() == (request.Limit ?? 200))
                nextToken = new DateTimeToken(orders.Data.Min(o => o.CreateTime).AddMilliseconds(-1));

            return orders.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, TradingMode.Spot, orders.Data.Select(x => new SharedSpotOrder(
                x.Symbol,
                x.OrderId.ToString(),
                ParseOrderType(x.OrderType),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                OrderPrice = x.Price == 0 ? null : x.Price,
                Quantity = x.Quantity,
                QuantityFilled = x.QuantityFilled,
                QuoteQuantity = x.QuoteQuantity,
                QuoteQuantityFilled = x.QuoteQuantityFilled,
                AveragePrice = x.PriceAverage == 0 ? null : x.PriceAverage,
                UpdateTime = x.UpdateTime,
                TimeInForce = ParseTimeInForce(x.OrderType)
            }).ToArray(), nextToken);
        }

        EndpointOptions<GetOrderTradesRequest> ISpotOrderRestClient.GetSpotOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> ISpotOrderRestClient.GetSpotOrderTradesAsync(GetOrderTradesRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotOrderTradesOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            var trades = await Trading.GetOrderTradesAsync(request.OrderId, ct: ct).ConfigureAwait(false);
            if (!trades)
                return trades.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, null, default);

            return trades.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, TradingMode.Spot, trades.Data.Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId,
                x.TradeId,
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                x.Quantity,
                x.Price,
                x.CreateTime)
            {
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.TradeRole == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray());
        }

        PaginatedEndpointOptions<GetUserTradesRequest> ISpotOrderRestClient.GetSpotUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(SharedPaginationSupport.Descending, true);
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> ISpotOrderRestClient.GetSpotUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).GetSpotUserTradesOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            // Get data
            var trades = await Trading.GetUserTradesAsync(request.Symbol.GetSymbol(FormatSymbol),
                startTime: request.StartTime,
                endTime: fromTimestamp ?? request.EndTime,
                limit: request.Limit ?? 200,
                ct: ct).ConfigureAwait(false);
            if (!trades)
                return trades.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, null, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (trades.Data.Count() == (request.Limit ?? 200))
                nextToken = new DateTimeToken(trades.Data.Min(o => o.CreateTime).AddMilliseconds(-1));

            return trades.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, TradingMode.Spot, trades.Data.Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId,
                x.TradeId,
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                x.Quantity,
                x.Price,
                x.CreateTime)
            {
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.TradeRole == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
            }).ToArray(), nextToken);
        }

        EndpointOptions<CancelOrderRequest> ISpotOrderRestClient.CancelSpotOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.CancelSpotOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = ((ISpotOrderRestClient)this).CancelSpotOrderOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(request.Symbol.GetSymbol(FormatSymbol), request.OrderId, ct: ct).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, null, default);

            return order.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedId(request.OrderId));
        }

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.PartiallyFilled || status == OrderStatus.New) return SharedOrderStatus.Open;
            if (status == OrderStatus.Canceled || status == OrderStatus.PartiallyCanceled || status == OrderStatus.Failed) return SharedOrderStatus.Canceled;
            return SharedOrderStatus.Filled;
        }

        private SharedOrderType ParseOrderType(OrderType type)
        {
            if (type == OrderType.Market) return SharedOrderType.Market;
            if (type == OrderType.LimitMaker) return SharedOrderType.LimitMaker;
            if (type == OrderType.Limit) return SharedOrderType.Limit;
            if (type == OrderType.ImmediateOrCancel) return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }

        private SharedTimeInForce? ParseTimeInForce(OrderType type)
        {
            if (type == OrderType.ImmediateOrCancel) return SharedTimeInForce.ImmediateOrCancel;

            return null;
        }

        private OrderType GetOrderType(SharedOrderType type, SharedTimeInForce? tif)
        {
            if (type == SharedOrderType.LimitMaker) return OrderType.LimitMaker;
            if (type == SharedOrderType.Market) return OrderType.Market;
            if (tif == SharedTimeInForce.ImmediateOrCancel) return OrderType.ImmediateOrCancel;
            if (type == SharedOrderType.Limit && (tif == null || tif == SharedTimeInForce.GoodTillCanceled)) return OrderType.Limit;

            throw new ArgumentException("Unknown timeInForce setting");
        }

        #endregion

        #region Asset client
        EndpointOptions<GetAssetRequest> IAssetsRestClient.GetAssetOptions { get; } = new EndpointOptions<GetAssetRequest>(false);
        async Task<ExchangeWebResult<SharedAsset>> IAssetsRestClient.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedAsset>(Exchange, validationError);

            var assets = await ExchangeData.GetAssetDepositWithdrawInfoAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<SharedAsset>(Exchange, null, default);

            var asset = assets.Data.Where(x => x.Asset.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0].Equals(request.Asset, StringComparison.InvariantCultureIgnoreCase)).ToList();
            if (asset == null)
                return assets.AsExchangeError<SharedAsset>(Exchange, new ServerError("Asset not found"));

            return assets.AsExchangeResult(Exchange, TradingMode.Spot, new SharedAsset(request.Asset)
            {
                Networks = asset.Select(x => new SharedAssetNetwork(x.Network)
                {
                    DepositEnabled = x.DepositEnabled,
                    MinWithdrawQuantity = x.WithdrawMinsize,
                    WithdrawEnabled = x.WithdrawEnabled,
                    // WithdrawFee = x.WithdrawMinfee This is in USDT, so can't be used
                }).ToArray()
            });
        }

        EndpointOptions<GetAssetsRequest> IAssetsRestClient.GetAssetsOptions { get; } = new EndpointOptions<GetAssetsRequest>(false);

        async Task<ExchangeWebResult<IEnumerable<SharedAsset>>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = ((IAssetsRestClient)this).GetAssetsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedAsset>>(Exchange, validationError);

            var assets = await ExchangeData.GetAssetDepositWithdrawInfoAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<IEnumerable<SharedAsset>>(Exchange, null, default);

            return assets.AsExchangeResult<IEnumerable<SharedAsset>>(Exchange, TradingMode.Spot, assets.Data.GroupBy(x => x.Asset.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0]).Select(x => new SharedAsset(x.Key)
            {
                Networks = x.Select(x => new SharedAssetNetwork(x.Network)
                {
                    DepositEnabled = x.DepositEnabled,
                    MinWithdrawQuantity = x.WithdrawMinsize,
                    WithdrawEnabled = x.WithdrawEnabled,
                    // WithdrawFee = x.WithdrawMinfee This is in USDT, so can't be used
                }).ToArray()
            }).ToArray());
        }

        #endregion

        #region Deposit client

        EndpointOptions<GetDepositAddressesRequest> IDepositRestClient.GetDepositAddressesOptions { get; } = new EndpointOptions<GetDepositAddressesRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedDepositAddress>>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositAddressesOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedDepositAddress>>(Exchange, validationError);

            var assetName = request.Asset;
            if (request.Network != null && request.Network != request.Asset)
                assetName += "-" + request.Network;

            var depositAddresses = await Account.GetDepositAddressAsync(assetName).ConfigureAwait(false);
            if (!depositAddresses)
                return depositAddresses.AsExchangeResult<IEnumerable<SharedDepositAddress>>(Exchange, null, default);

            return depositAddresses.AsExchangeResult<IEnumerable<SharedDepositAddress>>(Exchange, TradingMode.Spot, new[] { new SharedDepositAddress(depositAddresses.Data.Asset.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0], depositAddresses.Data.Address)
            {
                TagOrMemo = depositAddresses.Data.AddressMemo,
                Network = depositAddresses.Data.Network
            }
            });
        }

        GetDepositsOptions IDepositRestClient.GetDepositsOptions { get; } = new GetDepositsOptions(SharedPaginationSupport.Descending, true);
        async Task<ExchangeWebResult<IEnumerable<SharedDeposit>>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IDepositRestClient)this).GetDepositsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedDeposit>>(Exchange, validationError);

            // Get data
            var deposits = await Account.GetDepositHistoryAsync(
                asset: request.Asset,
                limit: request.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!deposits)
                return deposits.AsExchangeResult<IEnumerable<SharedDeposit>>(Exchange, null, default);

            return deposits.AsExchangeResult<IEnumerable<SharedDeposit>>(Exchange, TradingMode.Spot, deposits.Data.Select(x => new SharedDeposit(x.Asset.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0], x.ArrivalQuantity, x.Status == DepositWithdrawalStatus.Completed, x.ApplyTime)
            {
                Id = x.DepositId!,
                Tag = x.AddressMemo,
                TransactionId = x.TransactionId,
            }).ToArray());
        }

        #endregion

        #region Order Book client

        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(1, 50, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, request.Symbol.TradingMode, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, null, default);

            return result.AsExchangeResult(Exchange, request.Symbol.TradingMode, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Withdrawal client

        GetWithdrawalsOptions IWithdrawalRestClient.GetWithdrawalsOptions { get; } = new GetWithdrawalsOptions(SharedPaginationSupport.Descending, true);
        async Task<ExchangeWebResult<IEnumerable<SharedWithdrawal>>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var validationError = ((IWithdrawalRestClient)this).GetWithdrawalsOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedWithdrawal>>(Exchange, validationError);

            // Get data
            var withdrawals = await Account.GetWithdrawalHistoryAsync(
                asset: request.Asset,
                limit: request.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!withdrawals)
                return withdrawals.AsExchangeResult<IEnumerable<SharedWithdrawal>>(Exchange, null, default);

            return withdrawals.AsExchangeResult<IEnumerable<SharedWithdrawal>>(Exchange, TradingMode.Spot, withdrawals.Data.Select(x => new SharedWithdrawal(x.Asset.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0], x.Address!, x.ArrivalQuantity, x.Status == DepositWithdrawalStatus.Completed, x.ApplyTime)
            {
                Network = x.Asset.Split('-')[1],
                Id = x.WithdrawId!,
                Tag = x.AddressMemo,
                TransactionId = x.TransactionId,
                Fee = x.Fee
            }).ToArray());
        }

        #endregion

        #region Withdraw client

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions();

        async Task<ExchangeWebResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = ((IWithdrawRestClient)this).WithdrawOptions.ValidateRequest(Exchange, request, TradingMode.Spot, SupportedTradingModes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var assetName = request.Asset;
            if (request.Network != null && request.Network != request.Asset)
                assetName += "-" + request.Network;

            // Get data
            var withdrawal = await Account.WithdrawAsync(
                assetName,
                request.Quantity,
                request.Address,
                memo: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal)
                return withdrawal.AsExchangeResult<SharedId>(Exchange, null, default);

            return withdrawal.AsExchangeResult(Exchange, TradingMode.Spot, new SharedId(withdrawal.Data.WithdrawId));
        }

        #endregion
    }
}
