using BitMart.Net.Objects.Internal;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;

namespace BitMart.Net.Objects.Sockets
{
    internal class FuturesPingQuery : Query<BitMartFuturesUpdate<string>>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; } = new HashSet<string> { "pong" };

        public FuturesPingQuery() : base("{\"action\":\"ping\"}", false) { }
    }
}
