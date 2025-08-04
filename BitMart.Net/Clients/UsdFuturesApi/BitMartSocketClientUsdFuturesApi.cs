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
using CryptoExchange.Net.SharedApis;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    /// <summary>
    /// Client providing access to the BitMart UsdFutures websocket Api
    /// </summary>
    internal partial class BitMartSocketClientUsdFuturesApi : SocketApiClient, IBitMartSocketClientUsdFuturesApi
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
            ProcessUnparsableMessages = true;

            MessageSendSizeLimit = 2048;

            RegisterPeriodicQuery(
                "ping",
                TimeSpan.FromSeconds(15),
                x => new FuturesPingQuery(),
                (connection, result) =>
                {
                    if (result.Error?.Message.Equals("Query timeout") == true)
                    {
                        // Ping timeout, reconnect
                        _logger.LogWarning("[Sckt {SocketId}] Ping response timeout, reconnecting", connection.SocketId);
                        _ = connection.TriggerReconnectAsync();
                    }
                });
        }
        #endregion

        protected override IByteMessageAccessor CreateAccessor(WebSocketMessageType type) => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(BitMartExchange._serializerContext));

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BitMartExchange._serializerContext));

        public IBitMartSocketClientUsdFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BitMartAuthenticationProvider(credentials);


        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<BitMartFuturesTickerUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToTickerUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitMartFuturesTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<BitMartFuturesTickerUpdate>(_logger, symbols.Select(x => "futures/ticker:" + x).ToArray(), update => onMessage(update
                .WithSymbol(update.Data.Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<BitMartFuturesTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<BitMartFuturesTickerUpdate>(_logger, new[] { "futures/ticker" } , update => onMessage(update
                .WithSymbol(update.Data.Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<BitMartFundingRateUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToFundingRateUpdatesAsync(new[] { symbol }, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitMartFundingRateUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<BitMartFundingRateUpdate>(_logger, symbols.Select(s => "futures/fundingRate:" + s).ToArray(),
                update => onMessage(update
                    .WithSymbol(update.Data.Symbol)
                    .WithDataTimestamp(update.Data.Timestamp)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<BitMartFuturesTradeUpdate[]>> onMessage, CancellationToken ct = default)
            => SubscribeToTradeUpdatesAsync(new[] { symbol }, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitMartFuturesTradeUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<BitMartFuturesTradeUpdate[]>(_logger, symbols.Select(s => "futures/trade:" + s).ToArray(), 
                update => onMessage(update
                .WithSymbol(update.Data.First().Symbol)
                .WithDataTimestamp(update.Data.Max(x => x.Timestamp))), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<BitMartFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderBookUpdatesAsync(new[] { symbol }, depth, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<BitMartFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<BitMartFuturesOrderBookUpdate>(_logger, symbols.Select(s => $"futures/depth{depth}:" + s).ToArray(),
                update => onMessage(update
                .WithSymbol(update.Data.Symbol)
                .WithDataTimestamp(update.Data.Timestamp)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookSnapshotUpdatesAsync(string symbol, int depth, Action<DataEvent<BitMartFuturesFullOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderBookSnapshotUpdatesAsync(new[] { symbol }, depth, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookSnapshotUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<BitMartFuturesFullOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<BitMartFuturesFullOrderBookUpdate>(_logger, symbols.Select(s => $"futures/depthAll{depth}:" + s).ToArray(),
                update => onMessage(update
                .WithSymbol(update.Data.Symbol)
                .WithDataTimestamp(update.Data.Timestamp)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookIncrementalUpdatesAsync(string symbol, int depth, Action<DataEvent<BitMartFuturesFullOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderBookIncrementalUpdatesAsync(new[] { symbol }, depth, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookIncrementalUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<BitMartFuturesFullOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<BitMartFuturesFullOrderBookUpdate>(_logger, symbols.Select(s => $"futures/depthIncrease{depth}:" + s).ToArray(),
                update => onMessage(update
                .WithSymbol(update.Data.Symbol)
                .WithUpdateType(update.Data.Type == "snapshot" ? SocketUpdateType.Snapshot: SocketUpdateType.Update)
                .WithDataTimestamp(update.Data.Timestamp)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<BitMartBookTicker>> onMessage, CancellationToken ct = default)
            => SubscribeToBookTickerUpdatesAsync(new[] { symbol }, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitMartBookTicker>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<BitMartBookTicker>(_logger, symbols.Select(s => $"futures/bookticker:" + s).ToArray(),
                update => onMessage(update
                .WithSymbol(update.Data.Symbol)
                .WithDataTimestamp(update.Data.Timestamp)), false);
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
                update => onMessage(update
                .WithSymbol(update.Data.Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToMarkKlineUpdatesAsync(string symbol, FuturesStreamKlineInterval interval, Action<DataEvent<BitMartFuturesKlineUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToMarkKlineUpdatesAsync(new[] { symbol }, interval, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkKlineUpdatesAsync(IEnumerable<string> symbols, FuturesStreamKlineInterval interval, Action<DataEvent<BitMartFuturesKlineUpdate>> onMessage, CancellationToken ct = default)
        {
            var intervalStr = EnumConverter.GetString(interval);
            var subscription = new BitMartFuturesSubscription<BitMartFuturesKlineUpdate>(_logger, symbols.Select(s => $"futures/markPriceKlineBin{intervalStr}:" + s).ToArray(),
                update => onMessage(update
                .WithSymbol(update.Data.Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<BitMartFuturesBalanceUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<BitMartFuturesBalanceUpdate>(_logger, new[] { "futures/asset:USDT", "futures/asset:BTC", "futures/asset:ETH" }, onMessage, true);
            return await SubscribeAsync(BaseAddress.AppendPath("user?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(Action<DataEvent<BitMartPositionUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<BitMartPositionUpdate[]>(_logger, new[] { "futures/position" }, x => onMessage(
                x.WithSymbol(x.Data.First().Symbol)
                .WithDataTimestamp(x.Data.Max(x => x.UpdateTime))), true);
            return await SubscribeAsync(BaseAddress.AppendPath("user?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<BitMartFuturesOrderUpdateEvent[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartFuturesSubscription<BitMartFuturesOrderUpdateEvent[]>(_logger, new[] { "futures/order" }, x => onMessage(
                x.WithSymbol(x.Data.First().Order.Symbol)
                .WithDataTimestamp(x.Data.Max(x => x.Order.UpdateTime))), true);
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
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection)
        {
            var timestamp = DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString();
            var authProvider = (BitMartAuthenticationProvider)AuthenticationProvider!;
            var key = authProvider.ApiKey;
            var memo = authProvider.Pass;
            var sign = authProvider.Sign($"{timestamp}#{memo}#bitmart.WebSocket");

            return Task.FromResult<Query?>(new BitMartFuturesLoginQuery(key, timestamp!, sign));
        }

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => BitMartExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);
    }
}
