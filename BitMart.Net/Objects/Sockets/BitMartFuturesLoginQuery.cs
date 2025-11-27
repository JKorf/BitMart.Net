using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using BitMart.Net.Objects.Models;
using BitMart.Net.Objects.Internal;
using System.Linq;
using CryptoExchange.Net.Objects.Errors;
using System;

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
            MessageMatcher = MessageMatcher.Create<BitMartFuturesLoginResponse>("access", HandleMessage);
            MessageRouter = MessageRouter.Create<BitMartFuturesLoginResponse>("access", HandleMessage);
        }

        public CallResult<BitMartFuturesLoginResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, BitMartFuturesLoginResponse message)
        {
            if (message.Success != true)
                return new CallResult<BitMartFuturesLoginResponse>(new ServerError(ErrorInfo.Unknown with { Message = message.ErrorMessage! }));

            return new CallResult<BitMartFuturesLoginResponse>(message, originalData, null);
        }
    }
}
