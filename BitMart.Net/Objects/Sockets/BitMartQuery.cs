using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using BitMart.Net.Objects.Models;

namespace BitMart.Net.Objects.Sockets
{
    internal class BitMartQuery<T> : Query<T>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public BitMartQuery(BitMartModel request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            ListenerIdentifiers = new HashSet<string> { };
        }

        public override CallResult<T> HandleMessage(SocketConnection connection, DataEvent<T> message)
        {
            return new CallResult<T>(message.Data, message.OriginalData, null);
        }
    }
}
