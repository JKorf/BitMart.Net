using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using BitMart.Net.Objects.Models;
using BitMart.Net.Objects.Internal;
using System.Linq;

namespace BitMart.Net.Objects.Sockets
{
    internal class BitMartQuery : Query<BitMartSocketResponse>
    {
        public BitMartQuery(string operation, IEnumerable<string> parameters, bool authenticated, int weight = 1) : base(new BitMartSocketOperation
        {
            Operation = operation,
            Parameters = parameters.ToArray(),
        }, authenticated, weight)
        {
            MessageMatcher = MessageMatcher.Create<BitMartSocketResponse>(parameters.Select(p => operation + ":" + p), HandleMessage);
        }

        public CallResult<BitMartSocketResponse> HandleMessage(SocketConnection connection, DataEvent<BitMartSocketResponse> message)
        {
            if (message.Data.ErrorCode != null && message.Data.ErrorCode != 90008) // 90008 = duplicate subscription, which is fine
                return new CallResult<BitMartSocketResponse>(new ServerError(message.Data.ErrorCode.Value, message.Data.ErrorMessage!));

            return new CallResult<BitMartSocketResponse>(message.Data, message.OriginalData, null);
        }
    }
}
