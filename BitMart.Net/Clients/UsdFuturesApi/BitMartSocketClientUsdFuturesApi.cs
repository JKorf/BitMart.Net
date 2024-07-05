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

namespace BitMart.Net.Clients.UsdFuturesApi
{
    /// <summary>
    /// Client providing access to the BitMart UsdFutures websocket Api
    /// </summary>
    public class BitMartSocketClientUsdFuturesApi : SocketApiClient, IBitMartSocketClientUsdFuturesApi
    {
        #region fields
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal BitMartSocketClientUsdFuturesApi(ILogger logger, BitMartSocketOptions options) :
            base(logger, options.Environment.SocketClientAddress!, options, options.UsdFuturesOptions)
        {
        }
        #endregion 

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BitMartAuthenticationProvider((BitMartApiCredentials)credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToXXXUpdatesAsync(Action<DataEvent<BitMartModel>> onMessage, CancellationToken ct = default)
        {
            var subscription = new BitMartSubscription<BitMartModel>(_logger, new [] { "XXX" }, onMessage, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            return message.GetValue<string>(_idPath);
        }

        /// <inheritdoc />
        protected override Query? GetAuthenticationRequest(SocketConnection connection) => null;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset) => throw new NotImplementedException();
    }
}
