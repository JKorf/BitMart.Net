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
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            if (!message.IsJson)
                return "pong";

            var action = message.GetValue<string>(_actionPath);
            var group = message.GetValue<string>(_groupPath);
            
            if (action != null)
                return action + "-" + group;

            return group;
        }

        public override ReadOnlyMemory<byte> PreprocessStreamMessage(WebSocketMessageType type, ReadOnlyMemory<byte> data)
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
            var key = authProvider.GetApiKey();
            var memo = authProvider.GetMemo();
            var sign = authProvider.Sign($"{timestamp}#{memo}#bitmart.Websocket");

            return new BitMartLoginQuery(key, timestamp, sign);
        }

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => baseAsset + quoteAsset;
    }
}
