using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Objects.Sockets
{
    internal class PingQuery : Query
    {
        public override HashSet<string> ListenerIdentifiers { get; set; } = new HashSet<string> { "pong" };

        public PingQuery() : base("ping", false) { }

        public override void Fail(Error error)
        {
        }

        public override Type? GetMessageType(IMessageAccessor message) => typeof(string);

        public override CallResult Handle(SocketConnection connection, DataEvent<object> message) => new CallResult(null);

        public override void Timeout()
        {
        }
    }
}
