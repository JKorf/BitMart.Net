using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;

namespace BitMart.Net.Objects.Sockets
{
    internal class PingQuery : Query<string>
    {
        public PingQuery() : base("ping", false) {
            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageMatcher = MessageMatcher.Create<string>("pong");
            MessageRouter = MessageRouter.Create<string>("pong");
        }
    }
}
