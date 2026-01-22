using BitMart.Net.Objects.Internal;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BitMart.Net.Objects.Sockets
{
    internal class BitMartQuery : Query<BitMartSocketResponse>
    {
        private readonly SocketApiClient _client;

        public BitMartQuery(SocketApiClient client, string operation, IEnumerable<string> parameters, bool authenticated, int weight = 1) : base(new BitMartSocketOperation
        {
            Operation = operation,
            Parameters = parameters.ToArray(),
        }, authenticated, weight)
        {
            _client = client;
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<BitMartSocketResponse>(parameters.Select(p => operation + ":" + p), HandleMessage);
        }

        public CallResult<BitMartSocketResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitMartSocketResponse message)
        {
            if (message.ErrorCode != null && message.ErrorCode != 90008) // 90008 = duplicate subscription, which is fine
                return new CallResult<BitMartSocketResponse>(new ServerError(message.ErrorCode.Value, _client.GetErrorInfo(message.ErrorCode.Value, message.ErrorMessage!)));

            return new CallResult<BitMartSocketResponse>(message, originalData, null);
        }
    }
}
