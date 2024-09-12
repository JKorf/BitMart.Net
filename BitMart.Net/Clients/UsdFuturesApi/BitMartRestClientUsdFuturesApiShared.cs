using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Enums;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.Models.Rest;
using CryptoExchange.Net.SharedApis.RequestModels;
using CryptoExchange.Net.SharedApis.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.SharedApis.Models;
using CryptoExchange.Net.SharedApis.Models.FilterOptions;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Spot;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Futures;
using CryptoExchange.Net.SharedApis.Models.EndpointOptions;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    internal partial class BitMartRestClientUsdFuturesApi : IBitMartRestClientUsdFuturesApiShared
    {
        public string Exchange => BitMartExchange.ExchangeName;
        public ApiType[] SupportedApiTypes { get; } = new[] { ApiType.PerpetualLinear, ApiType.DeliveryLinear };

        #region Balance client
        EndpointOptions<GetBalancesRequest> IBalanceRestClient.GetBalancesOptions { get; } = new EndpointOptions<GetBalancesRequest>(true);

        async Task<ExchangeWebResult<IEnumerable<SharedBalance>>> IBalanceRestClient.GetBalancesAsync(GetBalancesRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IBalanceRestClient)this).GetBalancesOptions.ValidateRequest(Exchange, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedBalance>>(Exchange, validationError);

            var result = await Account.GetBalancesAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, default);

            return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, result.Data.Select(x => new SharedBalance(x.Asset, x.AvailableBalance, x.Equity)));
        }

        #endregion

        #region Futures Ticker client

        EndpointOptions<GetTickerRequest> IFuturesTickerRestClient.GetFuturesTickerOptions { get; } = new EndpointOptions<GetTickerRequest>(false);
        async Task<ExchangeWebResult<SharedFuturesTicker>> IFuturesTickerRestClient.GetFuturesTickerAsync(GetTickerRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesTicker>(Exchange, validationError);

            var resultTicker = ExchangeData.GetContractsAsync(request.Symbol.GetSymbol(FormatSymbol), ct);            
            var resultFundingRate = ExchangeData.GetCurrentFundingRateAsync(request.Symbol.GetSymbol(FormatSymbol), ct);
            await Task.WhenAll(resultTicker, resultFundingRate).ConfigureAwait(false);
            if (!resultTicker.Result)
                return resultTicker.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, default);
            if (!resultFundingRate.Result)
                return resultFundingRate.Result.AsExchangeResult<SharedFuturesTicker>(Exchange, default);

            var symbol = resultTicker.Result.Data.SingleOrDefault();
            if (symbol == null)
                return resultTicker.Result.AsExchangeError<SharedFuturesTicker>(Exchange, new ServerError("Symbol not found"));

            return resultTicker.Result.AsExchangeResult(Exchange,
                new SharedFuturesTicker(
                    symbol.Symbol,
                    symbol.LastPrice ?? 0,
                    symbol.HighPrice ?? 0,
                    symbol.LowPrice ?? 0,
                    symbol.Volume24h,
                    symbol.Change24h)
                {
                    IndexPrice = symbol.IndexPrice,
                    FundingRate = symbol.FundingRate,
                    NextFundingTime = resultFundingRate.Result.Data.NextFundingTime
                });
        }

        EndpointOptions<GetTickersRequest> IFuturesTickerRestClient.GetFuturesTickersOptions { get; } = new EndpointOptions<GetTickersRequest>(false);

        async Task<ExchangeWebResult<IEnumerable<SharedFuturesTicker>>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesTickerRestClient)this).GetFuturesTickerOptions.ValidateRequest(Exchange, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesTicker>>(Exchange, validationError);

            var resultTicker = await ExchangeData.GetContractsAsync(ct: ct).ConfigureAwait(false);
            if (!resultTicker)
                return resultTicker.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, default);

            return resultTicker.AsExchangeResult<IEnumerable<SharedFuturesTicker>>(Exchange, resultTicker.Data.Select(x =>
             new SharedFuturesTicker(x.Symbol, x.LastPrice ?? 0, x.HighPrice ?? 0, x.LowPrice ?? 0, x.Volume24h, x.Change24h)
             {
                 FundingRate = x.FundingRate,
                 IndexPrice = x.IndexPrice
             }
            ));
        }

        #endregion

        #region Futures Symbol client

        EndpointOptions<GetFuturesSymbolsRequest> IFuturesSymbolRestClient.GetFuturesSymbolsOptions { get; } = new EndpointOptions<GetFuturesSymbolsRequest>(false);
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesSymbol>>> IFuturesSymbolRestClient.GetFuturesSymbolsAsync(GetFuturesSymbolsRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesSymbolRestClient)this).GetFuturesSymbolsOptions.ValidateRequest(Exchange, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesSymbol>>(Exchange, validationError);

            var result = await ExchangeData.GetContractsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedFuturesSymbol>>(Exchange, default);

            var data = result.Data.Where(x => request.ApiType == ApiType.PerpetualLinear ? x.ProductType == ContractType.Perpetual : x.ProductType != ContractType.Perpetual);
            return result.AsExchangeResult(Exchange, data.Select(s => new SharedFuturesSymbol(s.ProductType == ContractType.Perpetual ? SharedSymbolType.PerpetualLinear : SharedSymbolType.DeliveryLinear, s.BaseAsset, s.QuoteAsset, s.Symbol, true)
            {
                MinTradeQuantity = s.MinQuantity,
                DeliveryTime = s.SettleTime,
                PriceStep = s.PricePrecision,
                QuantityStep = s.QuantityPrecision,
                ContractSize = s.ContractQuantity,
                MaxTradeQuantity = s.MaxQuantity
            }));
        }

        #endregion

        #region Klines client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(SharedPaginationType.Descending, false)
        {
            MaxRequestDataPoints = 1000
        };

        async Task<ExchangeWebResult<IEnumerable<SharedKline>>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var interval = (Enums.FuturesKlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.FuturesKlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineRestClient)this).GetKlinesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, validationError);

            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            var startTime = request.StartTime;
            var endTime = request.EndTime;
            var apiLimit = 1000;

            // API returns the newest data first if the timespan is bigger than the api limit of 1000 results
            // So we need to request the first 1000 from the start time, then the 1000 after that etc
            if (request.StartTime != null)
            {
                // Not paginated, check if the data will fit
                var seconds = apiLimit * (int)request.Interval;
                var maxEndTime = (fromTimestamp ?? request.StartTime).Value.AddSeconds(seconds);
                if (maxEndTime < endTime)
                    endTime = maxEndTime;
            }

            var result = await ExchangeData.GetKlinesAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                interval,
                fromTimestamp ?? request.StartTime ?? DateTime.UtcNow.AddSeconds(-(request.Limit ?? 100) * (int)interval),
                endTime ?? DateTime.UtcNow,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedKline>>(Exchange, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (request.StartTime != null && result.Data.Any())
            {
                var maxOpenTime = result.Data.Max(x => x.Timestamp!.Value);
                if (maxOpenTime < request.EndTime!.Value.AddSeconds(-(int)request.Interval))
                    nextToken = new DateTimeToken(maxOpenTime.AddSeconds((int)interval));
            }

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedKline(x.Timestamp!.Value, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume)), nextToken);
        }

        #endregion

        #region Leverage client

        EndpointOptions<GetLeverageRequest> ILeverageRestClient.GetLeverageOptions { get; } = new EndpointOptions<GetLeverageRequest>(true);
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.GetLeverageAsync(GetLeverageRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).GetLeverageOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Trading.GetPositionRiskAsync(
                symbol: request.Symbol.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, default);

            var position = result.Data.SingleOrDefault(x => x.PositionSide == (request.Side == SharedPositionSide.Short ? PositionSide.Short : PositionSide.Long));

            return result.AsExchangeResult(Exchange, new SharedLeverage(position?.Leverage ?? 0));
        }

        SetLeverageOptions ILeverageRestClient.SetLeverageOptions { get; } = new SetLeverageOptions(false);
        async Task<ExchangeWebResult<SharedLeverage>> ILeverageRestClient.SetLeverageAsync(SetLeverageRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ILeverageRestClient)this).SetLeverageOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedLeverage>(Exchange, validationError);

            var result = await Account.SetLeverageAsync(
                symbol: request.Symbol.GetSymbol(FormatSymbol),
                request.Leverage,
                request.MarginMode == SharedMarginMode.Isolated ? MarginType.IsolatedMargin : MarginType.CrossMargin,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedLeverage>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedLeverage(request.Leverage)
            {
                MarginMode = result.Data.MarginType == MarginType.CrossMargin ? SharedMarginMode.Cross : SharedMarginMode.Isolated
            });
        }
        #endregion

        #region Order Book client
        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(new[] { 5, 10, 20, 50, 100, 500, 1000 }, false);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Open Interest client

        EndpointOptions<GetOpenInterestRequest> IOpenInterestRestClient.GetOpenInterestOptions { get; } = new EndpointOptions<GetOpenInterestRequest>(true);
        async Task<ExchangeWebResult<SharedOpenInterest>> IOpenInterestRestClient.GetOpenInterestAsync(GetOpenInterestRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IOpenInterestRestClient)this).GetOpenInterestOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedOpenInterest>(Exchange, validationError);

            var result = await ExchangeData.GetOpenInterestAsync(request.Symbol.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOpenInterest>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedOpenInterest(result.Data.OpenInterest));
        }

        #endregion

        #region Futures Order Client

        PlaceFuturesOrderOptions IFuturesOrderRestClient.PlaceFuturesOrderOptions { get; } = new PlaceFuturesOrderOptions(
            new[]
            {
                SharedOrderType.Limit,
                SharedOrderType.Market,
                SharedOrderType.LimitMaker
            },
            new[]
            {
                SharedTimeInForce.GoodTillCanceled,
                SharedTimeInForce.ImmediateOrCancel,
                SharedTimeInForce.FillOrKill
            },
            new SharedQuantitySupport(
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset,
                SharedQuantityType.BaseAsset));

        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.PlaceFuturesOrderAsync(PlaceFuturesOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).PlaceFuturesOrderOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var result = await Trading.PlaceOrderAsync(
                request.Symbol.GetSymbol(FormatSymbol),
                GetFuturesSide(request),
                request.OrderType == SharedOrderType.Limit ? Enums.FuturesOrderType.Limit : Enums.FuturesOrderType.Market,
                quantity: (int)(request.Quantity ?? 0),
                price: request.Price,
                marginType: request.MarginMode == null ? null : request.MarginMode == SharedMarginMode.Isolated ? MarginType.IsolatedMargin : MarginType.CrossMargin,
                orderMode: GetOrderMode(request.OrderType, request.TimeInForce),
                clientOrderId: request.ClientOrderId).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedId(result.Data.OrderId.ToString()));
        }

        EndpointOptions<GetOrderRequest> IFuturesOrderRestClient.GetFuturesOrderOptions { get; } = new EndpointOptions<GetOrderRequest>(true);
        async Task<ExchangeWebResult<SharedFuturesOrder>> IFuturesOrderRestClient.GetFuturesOrderAsync(GetOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedFuturesOrder>(Exchange, validationError);

            var order = await Trading.GetOrderAsync(request.Symbol.GetSymbol(FormatSymbol), request.OrderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedFuturesOrder>(Exchange, default);

            return order.AsExchangeResult(Exchange, new SharedFuturesOrder(
                order.Data.Symbol,
                order.Data.OrderId,
                ParseOrderType(order.Data.OrderType),
                (order.Data.Side == FuturesSide.BuyCloseShort || order.Data.Side == FuturesSide.BuyOpenLong) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status, order.Data.Quantity - order.Data.QuantityFilled),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                AveragePrice = order.Data.AveragePrice,
                Price = order.Data.Price,
                Quantity = order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                UpdateTime = order.Data.UpdateTime,
                PositionSide = (order.Data.Side == FuturesSide.SellCloseLong || order.Data.Side == FuturesSide.BuyOpenLong) ? SharedPositionSide.Long : SharedPositionSide.Short,
            });
        }

        EndpointOptions<GetOpenOrdersRequest> IFuturesOrderRestClient.GetOpenFuturesOrdersOptions { get; } = new EndpointOptions<GetOpenOrdersRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesOrder>>> IFuturesOrderRestClient.GetOpenFuturesOrdersAsync(GetOpenOrdersRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetOpenFuturesOrdersOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesOrder>>(Exchange, validationError);

            var symbol = request.Symbol?.GetSymbol(FormatSymbol);
            var orders = await Trading.GetOpenOrdersAsync(symbol).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, default);

            return orders.AsExchangeResult(Exchange, orders.Data.Select(x => new SharedFuturesOrder(
                x.Symbol,
                x.OrderId,
                ParseOrderType(x.OrderType),
                (x.Side == FuturesSide.BuyCloseShort || x.Side == FuturesSide.BuyOpenLong) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status, x.Quantity - x.QuantityFilled),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AveragePrice,
                Price = x.Price,
                Quantity = x.Quantity,
                QuantityFilled = x.QuantityFilled,
                UpdateTime = x.UpdateTime,
                PositionSide = (x.Side == FuturesSide.SellCloseLong || x.Side == FuturesSide.BuyOpenLong) ? SharedPositionSide.Long : SharedPositionSide.Short,
            }));
        }

        PaginatedEndpointOptions<GetClosedOrdersRequest> IFuturesOrderRestClient.GetClosedFuturesOrdersOptions { get; } = new PaginatedEndpointOptions<GetClosedOrdersRequest>(SharedPaginationType.Ascending, true);
        async Task<ExchangeWebResult<IEnumerable<SharedFuturesOrder>>> IFuturesOrderRestClient.GetClosedFuturesOrdersAsync(GetClosedOrdersRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetClosedFuturesOrdersOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedFuturesOrder>>(Exchange, validationError);

            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            // Get data
            var orders = await Trading.GetClosedOrdersAsync(request.Symbol.GetSymbol(FormatSymbol),
                startTime: fromTimestamp ?? request.StartTime,
                endTime: request.EndTime).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedFuturesOrder>>(Exchange, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (orders.Data.Any())
                nextToken = new DateTimeToken(orders.Data.Max(o => o.CreateTime));

            return orders.AsExchangeResult(Exchange, orders.Data.Select(x => new SharedFuturesOrder(
                x.Symbol,
                x.OrderId,
                ParseOrderType(x.OrderType),
                (x.Side == FuturesSide.BuyCloseShort || x.Side == FuturesSide.BuyOpenLong) ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status, x.Quantity - x.QuantityFilled),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                AveragePrice = x.AveragePrice,
                Price = x.Price,
                Quantity = x.Quantity,
                QuantityFilled = x.QuantityFilled,
                UpdateTime = x.UpdateTime,
                PositionSide = (x.Side == FuturesSide.SellCloseLong || x.Side == FuturesSide.BuyOpenLong) ? SharedPositionSide.Long : SharedPositionSide.Short,
            }));
        }

        EndpointOptions<GetOrderTradesRequest> IFuturesOrderRestClient.GetFuturesOrderTradesOptions { get; } = new EndpointOptions<GetOrderTradesRequest>(true)
        {
            ExchangeRequestInfo = "Can only request trades for the past 7 days"
        };
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> IFuturesOrderRestClient.GetFuturesOrderTradesAsync(GetOrderTradesRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesOrderTradesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, new ArgumentError("Invalid order id"));

            var orders = await Trading.GetUserTradesAsync(request.Symbol.GetSymbol(FormatSymbol)).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

            return orders.AsExchangeResult(Exchange, orders.Data.Where(x => x.OrderId == request.OrderId).Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId.ToString(),
                x.TradeId,
                x.Quantity,
                x.Price,
                x.CreateTime)
            {
                Price = x.Price,
                Quantity = x.Quantity,
                Fee = x.Fee,
                Role = x.Role == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
            }));
        }

        PaginatedEndpointOptions<GetUserTradesRequest> IFuturesOrderRestClient.GetFuturesUserTradesOptions { get; } = new PaginatedEndpointOptions<GetUserTradesRequest>(SharedPaginationType.Ascending, true);
        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> IFuturesOrderRestClient.GetFuturesUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetFuturesUserTradesOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedUserTrade>>(Exchange, validationError);

            // Determine page token
            DateTime? fromTime = null;
            if (pageToken is DateTimeToken timeToken)
                fromTime = timeToken.LastTime;

#warning check result data sorting, oldest or newest first for pagination
            // Get data
            var orders = await Trading.GetUserTradesAsync(request.Symbol.GetSymbol(FormatSymbol),
                startTime: fromTime ?? request.StartTime,
                endTime: request.EndTime
                ).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

            // Get next token
            FromIdToken? nextToken = null;
            if (orders.Data.Any())
                nextToken = new FromIdToken(orders.Data.Max(o => o.CreateTime).AddMilliseconds(1).ToString());

            return orders.AsExchangeResult(Exchange, orders.Data.Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId.ToString(),
                x.TradeId.ToString(),
                x.Quantity,
                x.Price,
                x.CreateTime)
            {
                Price = x.Price,
                Quantity = x.Quantity,
                Fee = x.Fee,
                Role = x.Role == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
            }), nextToken);
        }

        EndpointOptions<CancelOrderRequest> IFuturesOrderRestClient.CancelFuturesOrderOptions { get; } = new EndpointOptions<CancelOrderRequest>(true);
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.CancelFuturesOrderAsync(CancelOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).CancelFuturesOrderOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var order = await Trading.CancelOrderAsync(request.Symbol.GetSymbol(FormatSymbol), request.OrderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, default);

            return order.AsExchangeResult(Exchange, new SharedId(request.OrderId));
        }

        EndpointOptions<GetPositionsRequest> IFuturesOrderRestClient.GetPositionsOptions { get; } = new EndpointOptions<GetPositionsRequest>(true);
        async Task<ExchangeWebResult<IEnumerable<SharedPosition>>> IFuturesOrderRestClient.GetPositionsAsync(GetPositionsRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).GetPositionsOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<IEnumerable<SharedPosition>>(Exchange, validationError);

            var result = await Trading.GetPositionsAsync(symbol: request.Symbol?.GetSymbol(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, default);

            return result.AsExchangeResult<IEnumerable<SharedPosition>>(Exchange, result.Data.Select(x => new SharedPosition(x.Symbol, x.CurrentQuantity ?? 0, x.Timestamp)
            {
                UnrealizedPnl = x.UnrealizedPnl,
                AverageEntryPrice = x.OpenAveragePrice,
                PositionSide = x.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                Leverage = x.Leverage,
                MaintenanceMargin = x.MaintenanceMargin
            }).ToList());
        }

        EndpointOptions<ClosePositionRequest> IFuturesOrderRestClient.ClosePositionOptions { get; } = new EndpointOptions<ClosePositionRequest>(true)
        {
            RequiredOptionalParameters = new List<ParameterDescription>
            {
                new ParameterDescription(nameof(ClosePositionRequest.Quantity), typeof(decimal), "Quantity of position to close", 1m)
            }
        };
        async Task<ExchangeWebResult<SharedId>> IFuturesOrderRestClient.ClosePositionAsync(ClosePositionRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IFuturesOrderRestClient)this).ClosePositionOptions.ValidateRequest(Exchange, request, exchangeParameters, request.Symbol.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol(FormatSymbol);            
            var result = await Trading.PlaceOrderAsync(
                symbol,
                request.PositionSide == SharedPositionSide.Short ? FuturesSide.BuyCloseShort : FuturesSide.SellCloseLong,
                FuturesOrderType.Market,
                (int)request.Quantity!.Value,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedId(result.Data.OrderId.ToString()));
        }

        private FuturesSide GetFuturesSide(PlaceFuturesOrderRequest request)
        {
            if (request.Side == SharedOrderSide.Buy)
            {
                if (request.PositionSide == SharedPositionSide.Long)
                    return FuturesSide.BuyOpenLong;
                return FuturesSide.BuyCloseShort;
            }

            if (request.PositionSide == SharedPositionSide.Long)
                return FuturesSide.SellCloseLong;
            return FuturesSide.SellOpenShort;
        }

        private OrderMode? GetOrderMode(SharedOrderType type, SharedTimeInForce? tif)
        {
            if (type == SharedOrderType.LimitMaker)
                return OrderMode.PostOnly;

            if (tif == null)
                return null;

            if (tif == SharedTimeInForce.ImmediateOrCancel) return OrderMode.ImmediateOrCancel;
            if (tif == SharedTimeInForce.FillOrKill) return OrderMode.FillOrKill;
            if (tif == SharedTimeInForce.GoodTillCanceled) return OrderMode.GoodTilCancel;

            return null;
        }

        private SharedOrderStatus ParseOrderStatus(FuturesOrderStatus status, decimal remainingQuantity)
        {
            if (status == FuturesOrderStatus.Approval || status == FuturesOrderStatus.Check) return SharedOrderStatus.Open;
            if (remainingQuantity > 0) return SharedOrderStatus.Canceled;
            return SharedOrderStatus.Filled;
        }

        private SharedOrderType ParseOrderType(FuturesOrderType type)
        {
            if (type == FuturesOrderType.Market) return SharedOrderType.Market;
            if (type == FuturesOrderType.Limit) return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }

        #endregion

        #region Position Mode client

        GetPositionModeOptions IPositionModeRestClient.GetPositionModeOptions { get; } = new GetPositionModeOptions(false);
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.GetPositionModeAsync(GetPositionModeRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            // Only support one mode, so never actually needs to change
            return new ExchangeWebResult<SharedPositionModeResult>(Exchange, new WebCallResult<SharedPositionModeResult>(
                null, null, null, null, null, null, null, null, null, null, ResultDataSource.Server, new SharedPositionModeResult(SharedPositionMode.LongShort), null));
        }

        SetPositionModeOptions IPositionModeRestClient.SetPositionModeOptions { get; } = new SetPositionModeOptions(false, true, false);
        async Task<ExchangeWebResult<SharedPositionModeResult>> IPositionModeRestClient.SetPositionModeAsync(SetPositionModeRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            // Only support one mode, so never actually needs to change
            return new ExchangeWebResult<SharedPositionModeResult>(Exchange, new WebCallResult<SharedPositionModeResult>(
                null, null, null, null, null, null, null, null, null, null, ResultDataSource.Server, new SharedPositionModeResult(request.Mode), null));
        }
        #endregion
    }
}
