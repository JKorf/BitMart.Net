using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using BitMart.Net.Objects.Models;
using BitMart.Net.Objects.Internal;

namespace BitMart.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class BitMartSubscription<T> : Subscription<BitMartSocketResponse, BitMartSocketResponse>
    {
        /// <inheritdoc />
        public override HashSet<string> ListenerIdentifiers { get; set; }

        private readonly Action<DataEvent<T>> _handler;
        private readonly IEnumerable<string> _topics;

        /// <inheritdoc />
        public override Type? GetMessageType(IMessageAccessor message)
        {
            return typeof(BitMartUpdate<T>);
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="topics"></param>
        /// <param name="handler"></param>
        /// <param name="auth"></param>
        public BitMartSubscription(ILogger logger, string[] topics, Action<DataEvent<T>> handler, bool auth) : base(logger, auth)
        {
            _handler = handler;
            _topics = topics;
            ListenerIdentifiers = new HashSet<string>(topics);
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection) => new BitMartQuery("subscribe", _topics, Authenticated);

        /// <inheritdoc />
        public override Query? GetUnsubQuery() => new BitMartQuery("unsubscribe", _topics, Authenticated);

        /// <inheritdoc />
        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var data = (BitMartUpdate<T>)message.Data;
            _handler.Invoke(message.As(data.Data, data.Table, null, SocketUpdateType.Update));
            return new CallResult(null);
        }
    }
}
