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
using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Objects.Models;
using BitMart.Net.Objects.Options;
using BitMart.Net.Objects.Sockets.Subscriptions;
using BitMart.Net.Objects;
using System.Net.WebSockets;
using CryptoExchange.Net;
using System.Collections;
using System.Collections.Generic;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Linq;
using BitMart.Net.Objects.Sockets;
using BitMart.Net.Enums;
using BitMart.Net.Objects.Internal;

namespace BitMart.Net.Clients.SpotApi
{
    /// <summary>
    /// Client providing access to the BitMart Spot websocket Api
    /// </summary>
    internal class BitMartSocketClientSpotApi : SocketApiClient, IBitMartSocketClientSpotApi
    {
        #region fields
        private static readonly MessagePath _topicPath = MessagePath.Get().Property("topic");
        private static readonly MessagePath _tablePath = MessagePath.Get().Property("table");
        private static readonly MessagePath _eventPath = MessagePath.Get().Property("event");
        private static readonly MessagePath _symbolPath = MessagePath.Get().Property("data").Index(0).Property("symbol");
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal BitMartSocketClientSpotApi(ILogger logger, BitMartSocketOptions options) :
            base(logger, options.Environment.SocketClientSpotAddress!, options, options.SpotOptions)
        {
            KeepAliveInterval = TimeSpan.Zero;
            RegisterPeriodicQuery("ping", TimeSpan.FromSeconds(15), x => new PingQuery(), null);
            RateLimiter = BitMartExchange.RateLimiter.SocketLimits;
        }
        #endregion 

        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor();

        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BitMartAuthenticationProvider((BitMartApiCredentials)credentials);

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<BitMartTickerUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToTickerUpdatesAsync(new[] { symbol }, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitMartTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartSubscription<IEnumerable<BitMartTickerUpdate>>(_logger, symbols.Select(s => "spot/ticker:" + s).ToArray(), update => onMessage(update.WithSymbol(update.Data.First().Symbol).As(update.Data.First())), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineStreamInterval interval, Action<DataEvent<IEnumerable<BitMartKlineUpdate>>> onMessage, CancellationToken ct = default)
            => SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineStreamInterval interval, Action<DataEvent<IEnumerable<BitMartKlineUpdate>>> onMessage, CancellationToken ct = default)
        {
            var intervalStr = EnumConverter.GetString(interval);
            var subscription = new BitMartSubscription<IEnumerable<BitMartKlineUpdate>>(_logger, symbols.Select(s => $"spot/kline{intervalStr}:" + s).ToArray(), update => onMessage(update.WithSymbol(update.Data.First().Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<BitMartOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, depth, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<BitMartOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            depth.ValidateIntValues(nameof(depth), 5, 20, 50);

            var subscription = new BitMartSubscription<IEnumerable<BitMartOrderBookUpdate>>(_logger, symbols.Select(s => $"spot/depth{depth}:" + s).ToArray(), update => onMessage(update.WithSymbol(update.Data.First().Symbol).As(update.Data.First())), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<BitMartOrderBookIncrementalUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderBookUpdatesAsync(new[] { symbol }, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<BitMartOrderBookIncrementalUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartSubscription<IEnumerable<BitMartOrderBookIncrementalUpdate>>(_logger, symbols.Select(s => $"spot/depth/increase100:" + s).ToArray(), 
                update => onMessage(update.WithSymbol(update.Data.First().Symbol).WithUpdateType(update.Data.First().Type == "snapshot"? SocketUpdateType.Snapshot: SocketUpdateType.Update).As(update.Data.First()))
                , false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<IEnumerable<BitMartTradeUpdate>>> onMessage, CancellationToken ct = default)
            => SubscribeToTradeUpdatesAsync(new[] { symbol }, onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<IEnumerable<BitMartTradeUpdate>>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartSubscription<IEnumerable<BitMartTradeUpdate>>(_logger, symbols.Select(s => $"spot/trade:" + s).ToArray(),
                update => onMessage(update.WithSymbol(update.Data.First().Symbol)), false);
            return await SubscribeAsync(BaseAddress.AppendPath("api?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<BitMartOrderUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartSubscription<IEnumerable<BitMartOrderUpdate>>(_logger, new[] { "spot/user/orders:ALL_SYMBOLS" },
                update => onMessage(update.WithSymbol(update.Data.First().Symbol).As(update.Data.First())), true);
            return await SubscribeAsync(BaseAddress.AppendPath("user?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<BitMartBalanceUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartSubscription<IEnumerable<BitMartBalanceUpdate>>(_logger, new[] { "spot/user/balance:BALANCE_UPDATE" },
                update => onMessage(update.As(update.Data.First())), true);
            return await SubscribeAsync(BaseAddress.AppendPath("user?protocol=1.1"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            if (!message.IsJson)
                return "pong";

            var evnt = message.GetValue<string>(_eventPath);
            if (evnt != null)
            {
                var topic = message.GetValue<string>(_topicPath);
                if (topic == null)
                    return evnt;

                return evnt + ":" + topic;
            }

            var table = message.GetValue<string>(_tablePath);
            if (string.Equals(table, "spot/user/orders", StringComparison.Ordinal))
                return table + ":ALL_SYMBOLS";

            if (string.Equals(table, "spot/user/balance", StringComparison.Ordinal))
                return table + ":BALANCE_UPDATE";

            var symbol = message.GetValue<string>(_symbolPath);
            return table + ":" + symbol;
        }

        public override ReadOnlyMemory<byte> PreprocessStreamMessage(SocketConnection connection, WebSocketMessageType type, ReadOnlyMemory<byte> data)
        {
            if (type == WebSocketMessageType.Text)
                return data;

            return data.Decompress();
        }

        /// <inheritdoc />
        protected override Query? GetAuthenticationRequest(SocketConnection connection) 
        {
            var timestamp = DateTimeConverter.ConvertToMilliseconds(DateTime.UtcNow).ToString();
            var authProvider = (BitMartAuthenticationProvider)AuthenticationProvider!;
            var key = authProvider.ApiKey;
            var memo = authProvider.GetMemo();
            var sign = authProvider.Sign($"{timestamp}#{memo}#bitmart.WebSocket");

            return new BitMartLoginQuery(key, timestamp, sign);
        }

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => baseAsset + "_" + quoteAsset;
    }
}
