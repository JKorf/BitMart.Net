using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BitMart.Net.Objects.Sockets
{
    internal class FuturesPingQuery : Query
    {
        public override HashSet<string> ListenerIdentifiers { get; set; } = new HashSet<string> { "pong" };

        public FuturesPingQuery() : base("{\"action\":\"ping\"}", false) { }

        public override void Fail(Error error)
        {
        }

        public override Type? GetMessageType(IMessageAccessor message) => typeof(string);

        public override Task<CallResult> Handle(SocketConnection connection, DataEvent<object> message) => Task.FromResult(new CallResult(null));

        public override void Timeout()
        {
        }
    }
}
