using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis.Enums;
using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using CryptoExchange.Net.SharedApis.Models;
using CryptoExchange.Net.SharedApis.Models.FilterOptions;
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

namespace BitMart.Net.Clients.UsdFuturesApi
{
    internal partial class BitMartSocketClientUsdFuturesApi : IBitMartSocketClientUsdFuturesApiShared
    {
        public string Exchange => BitMartExchange.ExchangeName;
        public ApiType[] SupportedApiTypes { get; } = new[] { ApiType.PerpetualLinear, ApiType.DeliveryLinear };

        // Ticker subscription information is not complete..
        //#region Ticker client
        //SubscriptionOptions<SubscribeTickerRequest> ITickerSocketClient.SubscribeTickerOptions { get; } = new SubscriptionOptions<SubscribeTickerRequest>(false);
        //async Task<ExchangeResult<UpdateSubscription>> ITickerSocketClient.SubscribeToTickerUpdatesAsync(SubscribeTickerRequest request, Action<ExchangeEvent<SharedSpotTicker>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        //{
        //    var validationError = ((ITickerSocketClient)this).SubscribeTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
        //    if (validationError != null)
        //        return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

        //    var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
        //    var result = await SubscribeToKlineUpdatesAsync(symbol, Enums.FuturesStreamKlineInterval.OneDay update => handler(update.AsExchangeEvent(Exchange, new SharedSpotTicker(update.Data.Symbol, update.Data.LastPrice, update.Data.HighPrice, update.Data.LowPrice, update.Data.Volume24h))), ct).ConfigureAwait(false);

        //    return new ExchangeResult<UpdateSubscription>(Exchange, result);
        //}

        //#endregion

        #region Trade client

        SubscriptionOptions<SubscribeTradeRequest> ITradeSocketClient.SubscribeTradeOptions { get; } = new SubscriptionOptions<SubscribeTradeRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> ITradeSocketClient.SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<ExchangeEvent<IEnumerable<SharedTrade>>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((ITradeSocketClient)this).SubscribeTradeOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var result = await SubscribeToTradeUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, update.Data.Select(x => new SharedTrade(x.Quantity, x.Price, x.Timestamp)))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Book Ticker client

        SubscriptionOptions<SubscribeBookTickerRequest> IBookTickerSocketClient.SubscribeBookTickerOptions { get; } = new SubscriptionOptions<SubscribeBookTickerRequest>(false);
        async Task<ExchangeResult<UpdateSubscription>> IBookTickerSocketClient.SubscribeToBookTickerUpdatesAsync(SubscribeBookTickerRequest request, Action<ExchangeEvent<SharedBookTicker>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IBookTickerSocketClient)this).SubscribeBookTickerOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
#warning Ticker doesn't have filter. Test to see if can be added. See documentation for :filter
            var result = await SubscribeToTickerUpdatesAsync(symbol, update => handler(update.AsExchangeEvent(Exchange, new SharedBookTicker(update.Data.BestAskPrice, update.Data.BestAskQuantity, update.Data.BestBidPrice, update.Data.BestBidQuantity))), ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        #region Balance client
        SubscriptionOptions IBalanceSocketClient.SubscribeBalanceOptions { get; } = new SubscriptionOptions("SubscribeBalanceRequest", false);
        async Task<ExchangeResult<UpdateSubscription>> IBalanceSocketClient.SubscribeToBalanceUpdatesAsync(ApiType apiType, Action<ExchangeEvent<IEnumerable<SharedBalance>>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var validationError = ((IBalanceSocketClient)this).SubscribeBalanceOptions.ValidateRequest(Exchange, exchangeParameters, apiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var result = await SubscribeToBalanceUpdatesAsync(
                update => handler(update.AsExchangeEvent<IEnumerable<SharedBalance>>(Exchange, new[] { new SharedBalance(update.Data.Asset, update.Data.Available, update.Data.Available + update.Data.Frozen) })),
                ct: ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }

        #endregion

        #region Kline client
        SubscribeKlineOptions IKlineSocketClient.SubscribeKlineOptions { get; } = new SubscribeKlineOptions(false);
        async Task<ExchangeResult<UpdateSubscription>> IKlineSocketClient.SubscribeToKlineUpdatesAsync(SubscribeKlineRequest request, Action<ExchangeEvent<SharedKline>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        {
            var interval = (Enums.FuturesStreamKlineInterval)request.Interval;
            if (!Enum.IsDefined(typeof(Enums.FuturesStreamKlineInterval), interval))
                return new ExchangeResult<UpdateSubscription>(Exchange, new ArgumentError("Interval not supported"));

            var validationError = ((IKlineSocketClient)this).SubscribeKlineOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
            if (validationError != null)
                return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

            var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
            var result = await SubscribeToKlineUpdatesAsync(symbol, interval, update => {
                foreach (var item in update.Data.Klines)
                    handler(update.AsExchangeEvent(Exchange, new SharedKline(item.Timestamp!.Value, item.ClosePrice, item.HighPrice, item.LowPrice, item.OpenPrice, item.Volume)));
                }, ct).ConfigureAwait(false);

            return new ExchangeResult<UpdateSubscription>(Exchange, result);
        }
        #endregion

        // Order book only sends one side at a time
        //#region Order Book client
        //SubscribeOrderBookOptions IOrderBookSocketClient.SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(false, new[] { 5, 20, 50 });
        //async Task<ExchangeResult<UpdateSubscription>> IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<ExchangeEvent<SharedOrderBook>> handler, ExchangeParameters? exchangeParameters, CancellationToken ct)
        //{
        //    var validationError = ((IOrderBookSocketClient)this).SubscribeOrderBookOptions.ValidateRequest(Exchange, request, exchangeParameters, request.ApiType, SupportedApiTypes);
        //    if (validationError != null)
        //        return new ExchangeResult<UpdateSubscription>(Exchange, validationError);

        //    var symbol = request.Symbol.GetSymbol((baseAsset, quoteAsset) => FormatSymbol(baseAsset, quoteAsset, request.ApiType));
        //    var result = await SubscribeToOrderBookUpdatesAsync(symbol, request.Limit ?? 20, update => handler(update.AsExchangeEvent(Exchange, new SharedOrderBook(update.Data.Depths.Asks, update.Data.Bids))), ct).ConfigureAwait(false);

        //    return new ExchangeResult<UpdateSubscription>(Exchange, result);
        //}
        //#endregion
    }
}
