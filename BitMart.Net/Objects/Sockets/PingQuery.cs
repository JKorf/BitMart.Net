using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;

namespace BitMart.Net.Objects.Sockets
{
    internal class PingQuery : Query<string>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; } = new HashSet<string> { "pong" };

        public PingQuery() : base("ping", false) {
            RequestTimeout = TimeSpan.FromSeconds(5);
        }
    }
}
