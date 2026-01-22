using BitMart.Net.Objects.Internal;
using CryptoExchange.Net.Sockets;
using System;

namespace BitMart.Net.Objects.Sockets
{
    internal class FuturesPingQuery : Query<BitMartFuturesUpdate<string>>
    {
        public FuturesPingQuery() : base("{\"action\":\"ping\"}", false) {
            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageRouter = MessageRouter.CreateWithoutHandler<BitMartFuturesUpdate<string>>("pong");
        }
    }
}
