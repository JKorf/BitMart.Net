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

namespace BitMart.Net.Clients.SpotApi
{
    internal partial class BitMartRestClientSpotApi : IBitMartRestClientSpotApiShared
    {
        public string Exchange => BitMartExchange.ExchangeName;

        #region Kline client

        GetKlinesOptions IKlineRestClient.GetKlinesOptions { get; } = new GetKlinesOptions(true)
        {
            MaxRequestDataPoints = 200
        };

        async Task<ExchangeWebResult<IEnumerable<SharedKline>>> IKlineRestClient.GetKlinesAsync(GetKlinesRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            var interval = (Enums.KlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.KlineInterval), interval))
                return new ExchangeWebResult<IEnumerable<SharedKline>>(Exchange, new ArgumentError("Interval not supported"));

            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            var startTime = request.Filter?.StartTime;
            var endTime = request.Filter?.EndTime?.AddSeconds(-1);
            var apiLimit = 200;

            if (request.Filter?.StartTime != null)
            {
                // Not paginated, check if the data will fit
                var seconds = apiLimit * (int)request.Interval;
                var maxEndTime = (fromTimestamp ?? request.Filter.StartTime).Value.AddSeconds(seconds - 1);
                if (maxEndTime < endTime)
                    endTime = maxEndTime;
            }

            // Get data
            var result = await ExchangeData.GetKlineHistoryAsync(
                request.GetSymbol(FormatSymbol),
                interval,
                fromTimestamp ?? request.Filter?.StartTime,
                endTime,
                request.Filter?.Limit ?? apiLimit,
                ct: ct
                ).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedKline>>(Exchange, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (request.Filter?.StartTime != null && result.Data.Any())
            {
                var maxOpenTime = result.Data.Max(x => x.OpenTime);
                if (maxOpenTime < request.Filter.EndTime!.Value.AddSeconds(-(int)request.Interval))
                    nextToken = new DateTimeToken(maxOpenTime.AddSeconds((int)interval));
            }

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedKline(x.OpenTime, x.ClosePrice, x.HighPrice, x.LowPrice, x.OpenPrice, x.Volume)), nextToken);
        }

        #endregion

        #region Spot Symbol client

        async Task<ExchangeWebResult<IEnumerable<SharedSpotSymbol>>> ISpotSymbolRestClient.GetSpotSymbolsAsync(CancellationToken ct)
        {
            var result = await ExchangeData.GetSymbolsAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedSpotSymbol>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Select(s => new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Symbol)
            {
                MinTradeQuantity = s.BaseMinQuantity,
                PriceDecimals = s.PriceMaxPrecision
            }));
        }

        #endregion

        #region Ticker client

        async Task<ExchangeWebResult<SharedTicker>> ITickerRestClient.GetTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetTickerAsync(request.GetSymbol(FormatSymbol), ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedTicker>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedTicker(result.Data.Symbol, result.Data.LastPrice, result.Data.HighPrice, result.Data.LowPrice));
        }

        async Task<ExchangeWebResult<IEnumerable<SharedTicker>>> ITickerRestClient.GetTickersAsync(ApiType? apiType, CancellationToken ct)
        {
            var result = await ExchangeData.GetTickersAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedTicker>>(Exchange, default);

            return result.AsExchangeResult<IEnumerable<SharedTicker>>(Exchange, result.Data.Select(x => new SharedTicker(x.Symbol, x.LastPrice, x.HighPrice, x.LowPrice)));
        }

        #endregion

        #region Recent Trade client
        GetRecentTradesOptions IRecentTradeRestClient.GetRecentTradesOptions { get; } = new GetRecentTradesOptions(50);

        async Task<ExchangeWebResult<IEnumerable<SharedTrade>>> IRecentTradeRestClient.GetRecentTradesAsync(GetRecentTradesRequest request, CancellationToken ct)
        {
            var result = await ExchangeData.GetTradesAsync(
                request.GetSymbol(FormatSymbol),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedTrade>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)));
        }

        #endregion

        #region Balance client

        async Task<ExchangeWebResult<IEnumerable<SharedBalance>>> IBalanceRestClient.GetBalancesAsync(ApiType? apiType, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var result = await Account.GetSpotBalancesAsync(ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<IEnumerable<SharedBalance>>(Exchange, default);

            return result.AsExchangeResult(Exchange, result.Data.Select(x => new SharedBalance(x.Id, x.Available, x.Available + x.Frozen)));
        }

        #endregion

        #region Spot Order client

        PlaceSpotOrderOptions ISpotOrderRestClient.PlaceSpotOrderOptions { get; } = new PlaceSpotOrderOptions(
            new[]
            {
                SharedOrderType.Limit,
                SharedOrderType.Market,
                SharedOrderType.LimitMaker
            },
            new[]
            {
                SharedTimeInForce.GoodTillCanceled,
                SharedTimeInForce.ImmediateOrCancel
            },
            new SharedQuantitySupport(
                SharedQuantityType.BaseAssetQuantity,
                SharedQuantityType.BaseAssetQuantity,
                SharedQuantityType.Both,
                SharedQuantityType.Both));

        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.PlaceOrderAsync(PlaceSpotOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            if (request.OrderType == SharedOrderType.Other)
                throw new ArgumentException("OrderType can't be `Other`", nameof(request.OrderType));

            var result = await Trading.PlaceOrderAsync(
                request.GetSymbol(FormatSymbol),
                request.Side == SharedOrderSide.Buy ? OrderSide.Buy : OrderSide.Sell,
                GetOrderType(request.OrderType, request.TimeInForce),
                quantity: request.Quantity,
                quoteQuantity: request.QuoteQuantity,
                price: request.Price,
                clientOrderId: request.ClientOrderId).ConfigureAwait(false);

            if (!result)
                return result.AsExchangeResult<SharedId>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedId(result.Data.OrderId));
        }

        async Task<ExchangeWebResult<SharedSpotOrder>> ISpotOrderRestClient.GetOrderAsync(GetOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var order = await Trading.GetOrderAsync(request.OrderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedSpotOrder>(Exchange, default);

            return order.AsExchangeResult(Exchange, new SharedSpotOrder(
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                ParseOrderType(order.Data.OrderType),
                order.Data.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(order.Data.Status),
                order.Data.CreateTime)
            {
                ClientOrderId = order.Data.ClientOrderId,
                Price = order.Data.Price,
                Quantity = order.Data.Quantity,
                QuantityFilled = order.Data.QuantityFilled,
                QuoteQuantity = order.Data.QuoteQuantity,
                QuoteQuantityFilled = order.Data.QuoteQuantityFilled,
                AveragePrice = order.Data.PriceAverage,
                UpdateTime = order.Data.UpdateTime,
                TimeInForce = ParseTimeInForce(order.Data.OrderType)
            });
        }

        async Task<ExchangeWebResult<IEnumerable<SharedSpotOrder>>> ISpotOrderRestClient.GetOpenOrdersAsync(GetSpotOpenOrdersRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            string? symbol = null;
            if (request.BaseAsset != null && request.QuoteAsset != null)
                symbol = FormatSymbol(request.BaseAsset, request.QuoteAsset, request.ApiType);

            var orders = await Trading.GetOpenOrdersAsync(symbol).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, default);

            return orders.AsExchangeResult(Exchange, orders.Data.Select(x => new SharedSpotOrder(
                x.Symbol,
                x.OrderId.ToString(),
                ParseOrderType(x.OrderType),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                Price = x.Price,
                Quantity = x.Quantity,
                QuantityFilled = x.QuantityFilled,
                QuoteQuantity = x.QuoteQuantity,
                QuoteQuantityFilled = x.QuoteQuantityFilled,
                AveragePrice = x.PriceAverage,
                UpdateTime = x.UpdateTime,
                TimeInForce = ParseTimeInForce(x.OrderType)
            }));
        }

        async Task<ExchangeWebResult<IEnumerable<SharedSpotOrder>>> ISpotOrderRestClient.GetClosedOrdersAsync(GetSpotClosedOrdersRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            // Get data
            var orders = await Trading.GetClosedOrdersAsync(request.GetSymbol(FormatSymbol),
                startTime: request.Filter?.StartTime,
                endTime: request.Filter?.EndTime,
                limit: request.Filter?.Limit ?? 200).ConfigureAwait(false);
            if (!orders)
                return orders.AsExchangeResult<IEnumerable<SharedSpotOrder>>(Exchange, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (orders.Data.Count() == (request.Filter?.Limit ?? 200))
                nextToken = new DateTimeToken(orders.Data.Max(o => o.CreateTime));

            return orders.AsExchangeResult(Exchange, orders.Data.Select(x => new SharedSpotOrder(
                x.Symbol,
                x.OrderId.ToString(),
                ParseOrderType(x.OrderType),
                x.Side == OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                ParseOrderStatus(x.Status),
                x.CreateTime)
            {
                ClientOrderId = x.ClientOrderId,
                Price = x.Price,
                Quantity = x.Quantity,
                QuantityFilled = x.QuantityFilled,
                QuoteQuantity = x.QuoteQuantity,
                QuoteQuantityFilled = x.QuoteQuantityFilled,
                AveragePrice = x.PriceAverage,
                UpdateTime = x.UpdateTime,
                TimeInForce = ParseTimeInForce(x.OrderType)
            }));
        }

        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> ISpotOrderRestClient.GetOrderTradesAsync(GetOrderTradesRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var trades = await Trading.GetOrderTradesAsync(request.OrderId).ConfigureAwait(false);
            if (!trades)
                return trades.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

            return trades.AsExchangeResult(Exchange, trades.Data.Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId,
                x.TradeId,
                x.Quantity,
                x.Price,
                x.CreateTime)
            {
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.TradeRole == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
            }));
        }

        async Task<ExchangeWebResult<IEnumerable<SharedUserTrade>>> ISpotOrderRestClient.GetUserTradesAsync(GetUserTradesRequest request, INextPageToken? pageToken, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            // Determine page token
            DateTime? fromTimestamp = null;
            if (pageToken is DateTimeToken dateTimeToken)
                fromTimestamp = dateTimeToken.LastTime;

            // Get data
            var trades = await Trading.GetUserTradesAsync(request.GetSymbol(FormatSymbol),
                startTime: fromTimestamp ?? request.Filter?.StartTime,
                endTime: request.Filter?.EndTime,
                limit: request.Filter?.Limit ?? 200).ConfigureAwait(false);
            if (!trades)
                return trades.AsExchangeResult<IEnumerable<SharedUserTrade>>(Exchange, default);

            // Get next token
            DateTimeToken? nextToken = null;
            if (trades.Data.Count() == (request.Filter?.Limit ?? 200))
                nextToken = new DateTimeToken(trades.Data.Max(o => o.CreateTime));

            return trades.AsExchangeResult(Exchange, trades.Data.Select(x => new SharedUserTrade(
                x.Symbol,
                x.OrderId,
                x.TradeId,
                x.Quantity,
                x.Price,
                x.CreateTime)
            {
                Fee = x.Fee,
                FeeAsset = x.FeeAsset,
                Role = x.TradeRole == TradeRole.Maker ? SharedRole.Maker : SharedRole.Taker
            }), nextToken);
        }

        async Task<ExchangeWebResult<SharedId>> ISpotOrderRestClient.CancelOrderAsync(CancelOrderRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var order = await Trading.CancelOrderAsync(request.GetSymbol(FormatSymbol), request.OrderId).ConfigureAwait(false);
            if (!order)
                return order.AsExchangeResult<SharedId>(Exchange, default);

            return order.AsExchangeResult(Exchange, new SharedId(request.OrderId));
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

        async Task<ExchangeWebResult<IEnumerable<SharedAsset>>> IAssetRestClient.GetAssetsAsync(CancellationToken ct)
        {
            var assets = await ExchangeData.GetAssetDepositWithdrawInfoAsync(ct: ct).ConfigureAwait(false);
            if (!assets)
                return assets.AsExchangeResult<IEnumerable<SharedAsset>>(Exchange, default);

            return assets.AsExchangeResult<IEnumerable<SharedAsset>>(Exchange, assets.Data.GroupBy(x => x.Asset.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0]).Select(x => new SharedAsset(x.Key)
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

        async Task<ExchangeWebResult<IEnumerable<SharedDepositAddress>>> IDepositRestClient.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var assetName = request.Asset;
            if (request.Network != null && request.Network != request.Asset)
                assetName += "-" + request.Network;

            var depositAddresses = await Account.GetDepositAddressAsync(assetName).ConfigureAwait(false);
            if (!depositAddresses)
                return depositAddresses.AsExchangeResult<IEnumerable<SharedDepositAddress>>(Exchange, default);

            return depositAddresses.AsExchangeResult<IEnumerable<SharedDepositAddress>>(Exchange, new[] { new SharedDepositAddress(depositAddresses.Data.Asset, depositAddresses.Data.Address)
            {
                Tag = depositAddresses.Data.AddressMemo,
                Network = depositAddresses.Data.Network
            }
            });
        }

#warning No pagination supported, only most recent 100
        async Task<ExchangeWebResult<IEnumerable<SharedDeposit>>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            // Get data
            var deposits = await Account.GetDepositHistoryAsync(
                asset: request.Asset,
                limit: request.Filter?.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!deposits)
                return deposits.AsExchangeResult<IEnumerable<SharedDeposit>>(Exchange, default);

            return deposits.AsExchangeResult(Exchange, deposits.Data.Select(x => new SharedDeposit(x.Asset, x.ArrivalQuantity, x.Status == DepositWithdrawalStatus.Completed, x.ApplyTime)
            {
                Tag = x.AddressMemo,
                TransactionId = x.TransactionId,
            }));
        }

        #endregion

        #region Order Book client

        GetOrderBookOptions IOrderBookRestClient.GetOrderBookOptions { get; } = new GetOrderBookOptions(1, 50);
        async Task<ExchangeWebResult<SharedOrderBook>> IOrderBookRestClient.GetOrderBookAsync(GetOrderBookRequest request, CancellationToken ct)
        {
            var validationError = ((IOrderBookRestClient)this).GetOrderBookOptions.Validate(request);
            if (validationError != null)
                return new ExchangeWebResult<SharedOrderBook>(Exchange, validationError);

            var result = await ExchangeData.GetOrderBookAsync(
                request.GetSymbol(FormatSymbol),
                limit: request.Limit,
                ct: ct).ConfigureAwait(false);
            if (!result)
                return result.AsExchangeResult<SharedOrderBook>(Exchange, default);

            return result.AsExchangeResult(Exchange, new SharedOrderBook(result.Data.Asks, result.Data.Bids));
        }

        #endregion

        #region Withdrawal client
#warning No pagination supported, only most recent 100

        async Task<ExchangeWebResult<IEnumerable<SharedWithdrawal>>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, INextPageToken? pageToken, CancellationToken ct)
        {
            // Get data
            var withdrawals = await Account.GetWithdrawalHistoryAsync(
                asset: request.Asset,
                limit: request.Filter?.Limit ?? 100,
                ct: ct).ConfigureAwait(false);
            if (!withdrawals)
                return withdrawals.AsExchangeResult<IEnumerable<SharedWithdrawal>>(Exchange, default);

            return withdrawals.AsExchangeResult(Exchange, withdrawals.Data.Select(x => new SharedWithdrawal(x.Asset, x.Address!, x.ArrivalQuantity, x.Status == DepositWithdrawalStatus.Completed, x.ApplyTime)
            {
                Tag = x.AddressMemo,
                TransactionId = x.TransactionId,
                Fee = x.Fee
            }));
        }

        #endregion

        #region Withdraw client

        WithdrawOptions IWithdrawRestClient.WithdrawOptions { get; } = new WithdrawOptions();

        async Task<ExchangeWebResult<SharedId>> IWithdrawRestClient.WithdrawAsync(WithdrawRequest request, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var assetName = request.Asset;
            if (request.Network != null && request.Network != request.Asset)
                assetName += "-" + request.Network;

            var validationError = ((IWithdrawRestClient)this).WithdrawOptions.Validate(request);
            if (validationError != null)
                return new ExchangeWebResult<SharedId>(Exchange, validationError);

            // Get data
            var withdrawal = await Account.WithdrawAsync(
                request.Asset,
                request.Quantity,
                request.Address,
                memo: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal)
                return withdrawal.AsExchangeResult<SharedId>(Exchange, default);

            return withdrawal.AsExchangeResult(Exchange, new SharedId(withdrawal.Data.WithdrawId));
        }

        #endregion
    }
}
