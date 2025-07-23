using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using BitMart.Net.Objects.Models;
using BitMart.Net.Objects.Internal;
using System.Linq;

namespace BitMart.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class BitMartFuturesSubscription<T> : Subscription<BitMartSocketResponse, BitMartSocketResponse>
    {

        private readonly Action<DataEvent<T>> _handler;
        private readonly IEnumerable<string> _topics;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="topics"></param>
        /// <param name="handler"></param>
        /// <param name="auth"></param>
        public BitMartFuturesSubscription(ILogger logger, string[] topics, Action<DataEvent<T>> handler, bool auth) : base(logger, auth)
        {
            _handler = handler;
            _topics = topics;

            MessageMatcher = MessageMatcher.Create<BitMartFuturesUpdate<T>>(topics, DoHandleMessage);
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection) => new BitMartFuturesQuery("subscribe", _topics, Authenticated) { RequiredResponses = _topics.Count() };

        /// <inheritdoc />
        public override Query? GetUnsubQuery() => new BitMartFuturesQuery("unsubscribe", _topics, Authenticated) { RequiredResponses = _topics.Count() };

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<BitMartFuturesUpdate<T>> message)
        {
            _handler.Invoke(message.As(message.Data.Data, message.Data.Group, null, SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }
    }
}
