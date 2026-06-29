using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using BitMart.Net.Objects.Internal;
using CryptoExchange.Net.Clients;
using System;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;

namespace BitMart.Net.Objects.Sockets
{
    internal class BitMartLoginQuery : Query<BitMartSocketResponse>
    {
        private readonly SocketApiClient _client;

        public BitMartLoginQuery(SocketApiClient client, string key, string timestamp, string sign) : base(new BitMartSocketOperation
        {
            Operation = "login",
            Parameters = new[] { key, timestamp, sign },
        }, false, 1)
        {
            _client = client;
            MessageRouter = MessageRouter.CreateForQuery<BitMartSocketResponse>("login", HandleMessage);
        }

        public CallResult<BitMartSocketResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitMartSocketResponse message)
        {
            if (message.ErrorCode != null)
                return CallResult<BitMartSocketResponse>.Fail(new ServerError(message.ErrorCode.Value, _client.GetErrorInfo(message.ErrorCode.Value, message.ErrorMessage!)), originalData);

            return CallResult<BitMartSocketResponse>.Ok(message, originalData);
        }
    }
}
