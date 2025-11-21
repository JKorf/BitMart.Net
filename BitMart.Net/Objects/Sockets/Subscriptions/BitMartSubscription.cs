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
using CryptoExchange.Net.Clients;

namespace BitMart.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class BitMartSubscription<T> : Subscription<BitMartSocketResponse, BitMartSocketResponse>
    {
        private readonly SocketApiClient _client;
        private readonly Action<DateTime, string?, BitMartUpdate<T>> _handler;
        private readonly IEnumerable<string> _topics;

        /// <summary>
        /// ctor
        /// </summary>
        public BitMartSubscription(ILogger logger, SocketApiClient client, string[] topics, Action<DateTime, string?, BitMartUpdate<T>> handler, bool auth) : base(logger, auth)
        {
            _client = client;
            _handler = handler;
            _topics = topics;

            MessageMatcher = MessageMatcher.Create<BitMartUpdate<T>>(topics, DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection) => new BitMartQuery(_client, "subscribe", _topics, Authenticated) { RequiredResponses = _topics.Count() };

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection) => new BitMartQuery(_client, "unsubscribe", _topics, Authenticated) { RequiredResponses = _topics.Count() };

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitMartUpdate<T> message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }
    }
}
