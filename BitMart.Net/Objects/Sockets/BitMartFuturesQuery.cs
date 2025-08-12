using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using BitMart.Net.Objects.Models;
using BitMart.Net.Objects.Internal;
using System.Linq;
using CryptoExchange.Net.Clients;

namespace BitMart.Net.Objects.Sockets
{
    internal class BitMartFuturesQuery : Query<BitMartSocketResponse>
    {
        private readonly SocketApiClient _client;

        public BitMartFuturesQuery(SocketApiClient client, string operation, IEnumerable<string> parameters, bool authenticated, int weight = 1) : base(new BitMartFuturesSocketOperation
        {
            Operation = operation,
            Parameters = parameters.ToArray(),
        }, authenticated, weight)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<BitMartSocketResponse>(parameters.Select(p => operation + "-" + p), HandleMessage);
        }

        public CallResult<BitMartSocketResponse> HandleMessage(SocketConnection connection, DataEvent<BitMartSocketResponse> message)
        {
            if (message.Data.ErrorCode != null)
                return new CallResult<BitMartSocketResponse>(new ServerError(message.Data.ErrorCode.Value, _client.GetErrorInfo(message.Data.ErrorCode.Value, message.Data.ErrorMessage!)));

            return new CallResult<BitMartSocketResponse>(message.Data, message.OriginalData, null);
        }
    }
}
