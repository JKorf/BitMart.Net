using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using BitMart.Net.Objects.Internal;
using CryptoExchange.Net.Objects.Errors;
using System;
using CryptoExchange.Net.Sockets.Default;

namespace BitMart.Net.Objects.Sockets
{
    internal class BitMartFuturesLoginQuery : Query<BitMartFuturesLoginResponse>
    {
        public BitMartFuturesLoginQuery(string key, string timestamp, string sign) : base(new BitMartFuturesSocketOperation
        {
            Operation = "access",
            Parameters = new[] { key, timestamp, sign, "web" },
        }, false, 1)
        {
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<BitMartFuturesLoginResponse>("access", HandleMessage);
        }

        public CallResult<BitMartFuturesLoginResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitMartFuturesLoginResponse message)
        {
            if (message.Success != true)
                return new CallResult<BitMartFuturesLoginResponse>(new ServerError(ErrorInfo.Unknown with { Message = message.ErrorMessage! }));

            return new CallResult<BitMartFuturesLoginResponse>(message, originalData, null);
        }
    }
}
