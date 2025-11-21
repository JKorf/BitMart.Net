using BitMart.Net.Objects.Internal;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;

namespace BitMart.Net.Objects.Sockets
{
    internal class FuturesPingQuery : Query<BitMartFuturesUpdate<string>>
    {
        public FuturesPingQuery() : base("{\"action\":\"ping\"}", false) {
            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageMatcher = MessageMatcher.Create<BitMartFuturesUpdate<string>>("pong");
        }
    }
}
