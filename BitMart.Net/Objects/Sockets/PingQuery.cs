using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default.Routing;
using System;

namespace BitMart.Net.Objects.Sockets
{
    internal class PingQuery : Query<string>
    {
        public PingQuery() : base("ping", false) {
            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageRouter = MessageRouter.CreateWithoutHandler<string>("pong");
        }
    }
}
