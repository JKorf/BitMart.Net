using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using BitMart.Net.Objects.Internal;
using System.Linq;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets.Default;

namespace BitMart.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class BitMartSubscription<T> : Subscription
    {
        private readonly SocketApiClient _client;
        private readonly Action<DateTime, string?, BitMartUpdate<T>> _handler;
        private readonly string[] _subTopics;

        /// <summary>
        /// ctor
        /// </summary>
        public BitMartSubscription(ILogger logger, SocketApiClient client, string topic, string[]? symbols, Action<DateTime, string?, BitMartUpdate<T>> handler, bool auth) : base(logger, auth)
        {
            _client = client;
            _handler = handler;

            IndividualSubscriptionCount = symbols?.Length ?? 1;

            if (topic == "spot/user/balance")
                _subTopics = ["spot/user/balance:BALANCE_UPDATE"];
            else if (topic == "spot/user/orders")
                _subTopics = ["spot/user/orders:ALL_SYMBOLS"];
            else
                _subTopics = symbols!.Select(x => $"{topic}:{x}").ToArray();

            MessageMatcher = MessageMatcher.Create<BitMartUpdate<T>>(_subTopics, DoHandleMessage);
            MessageRouter = MessageRouter.CreateWithOptionalTopicFilters<BitMartUpdate<T>>(topic, symbols, DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection) => new BitMartQuery(_client, "subscribe", _subTopics, Authenticated) { RequiredResponses = _subTopics.Count() };

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection) => new BitMartQuery(_client, "unsubscribe", _subTopics, Authenticated) { RequiredResponses = _subTopics.Count() };

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitMartUpdate<T> message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }
    }
}
