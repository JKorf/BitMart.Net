using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using BitMart.Net.Objects.Internal;
using System.Linq;
using CryptoExchange.Net.Clients;
using System;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Objects.Errors;

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
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<BitMartFuturesSocketResponse>(parameters.Select(p => operation + "-" + p), HandleMessage);
        }

        public CallResult<BitMartFuturesSocketResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitMartFuturesSocketResponse message)
        {
            if (message.ErrorMessage != null)
            {
                if (message.ErrorMessage.Contains("Invalid channel"))
                    return new CallResult<BitMartFuturesSocketResponse>(new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, message.ErrorMessage)));

                return new CallResult<BitMartFuturesSocketResponse>(new ServerError(new ErrorInfo(ErrorType.Unknown, message.ErrorMessage)));
            }
            return new CallResult<BitMartFuturesSocketResponse>(message, originalData, null);
        }
    }
}
