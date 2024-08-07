using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using BitMart.Net.Objects.Models;
using BitMart.Net.Objects.Options;
using BitMart.Net.Objects.Sockets.Subscriptions;
using BitMart.Net.Objects;
using BitMart.Net.Objects.Sockets;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net;
using System.Net.WebSockets;
using System.Collections.Generic;
using System.Linq;
using BitMart.Net.Enums;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    /// <summary>
    /// Client providing access to the BitMart UsdFutures websocket Api
    /// </summary>
    internal class BitMartSocketClientUsdFuturesApi : SocketApiClient, IBitMartSocketClientUsdFuturesApi
    {
        #region fields
        private static readonly MessagePath _actionPath = MessagePath.Get().Property("action");
        private static readonly MessagePath _groupPath = MessagePath.Get().Property("group");
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal BitMartSocketClientUsdFuturesApi(ILogger logger, BitMartSocketOptions options) :
            base(logger, options.Environment.SocketClientPerpetualFuturesAddress!, options, options.UsdFuturesOptions)
        {
            KeepAliveInterval = TimeSpan.Zero;

            RegisterPeriodicQuery("ping", TimeSpan.FromSeconds(15), x => new FuturesPingQuery(), null);
        }
        #endregion

        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor();

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BitMartAuthenticationProvider((BitMartApiCredentials)credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<BitMartFuturesTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<BitMartFuturesTickerUpdate>(_logger, new[] { "futures/ticker" } , update => onMessage(update.WithSymbol(update.Data.Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<BitMartFuturesTradeUpdate>>> onMessage, CancellationToken ct = default)
            => SubscribeToTradeUpdatesAsync(new[] { symbol }, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IEnumerable<BitMartFuturesTradeUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<IEnumerable<BitMartFuturesTradeUpdate>>(_logger, symbols.Select(s => "futures/trade:" + s).ToArray(), 
                update => onMessage(update.WithSymbol(update.Data.First().Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<BitMartFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderBookUpdatesAsync(new[] { symbol }, depth, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<BitMartFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<BitMartFuturesOrderBookUpdate>(_logger, symbols.Select(s => $"futures/depth{depth}:" + s).ToArray(),
                update => onMessage(update.WithSymbol(update.Data.Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, FuturesStreamKlineInterval interval, Action<DataEvent<BitMartFuturesKlineUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, FuturesStreamKlineInterval interval, Action<DataEvent<BitMartFuturesKlineUpdate>> onMessage, CancellationToken ct = default)
        {
            var intervalStr = EnumConverter.GetString(interval);
            var subscription = new BitMartFuturesSubscription<BitMartFuturesKlineUpdate>(_logger, symbols.Select(s => $"futures/klineBin{intervalStr}:" + s).ToArray(),
                update => onMessage(update.WithSymbol(update.Data.Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<BitMartFuturesBalanceUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<BitMartFuturesBalanceUpdate>(_logger, new[] { "futures/asset:USDT", "futures/asset:BTC", "futures/asset:ETH" }, onMessage, true);
            return await SubscribeAsync(BaseAddress.AppendPath("user?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(Action<DataEvent<IEnumerable<BitMartPositionUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<IEnumerable<BitMartPositionUpdate>>(_logger, new[] { "futures/position" }, x => onMessage(x.WithSymbol(x.Data.First().Symbol)), true);
            return await SubscribeAsync(BaseAddress.AppendPath("user?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<IEnumerable<BitMartFuturesOrderUpdateEvent>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<IEnumerable<BitMartFuturesOrderUpdateEvent>>(_logger, new[] { "futures/order" }, x => onMessage(x.WithSymbol(x.Data.First().Order.Symbol)), true);
            return await SubscribeAsync(BaseAddress.AppendPath("user?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            var group = message.GetValue<string>(_groupPath);
            if (group == "System")
                return "pong";

            var action = message.GetValue<string>(_actionPath);
            if (action == "access")
                return action;
            
            if (!string.IsNullOrEmpty(action))
                return action + "-" + group;

            return group;
        }

        public override ReadOnlyMemory<byte> PreprocessStreamMessage(SocketConnection connection, WebSocketMessageType type, ReadOnlyMemory<byte> data)
        {
            if (type == WebSocketMessageType.Text)
                return data;

            return data.DecompressGzip();
        }

        /// <inheritdoc />
        protected override Query? GetAuthenticationRequest(SocketConnection connection)
        {
            var timestamp = DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString();
            var authProvider = (BitMartAuthenticationProvider)AuthenticationProvider!;
            var key = authProvider.ApiKey;
            var memo = authProvider.GetMemo();
            var sign = authProvider.Sign($"{timestamp}#{memo}#bitmart.WebSocket");

            return new BitMartFuturesLoginQuery(key, timestamp, sign);
        }

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => baseAsset + quoteAsset;
    }
}
