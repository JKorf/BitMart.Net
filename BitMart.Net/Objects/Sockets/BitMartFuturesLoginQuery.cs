using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using BitMart.Net.Objects.Models;
using BitMart.Net.Objects.Internal;
using System.Linq;

namespace BitMart.Net.Objects.Sockets
{
    internal class BitMartFuturesLoginQuery : Query<BitMartFuturesLoginResponse>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public BitMartFuturesLoginQuery(string key, string timestamp, string sign) : base(new BitMartFuturesSocketOperation
        {
            Operation = "access",
            Parameters = new[] { key, timestamp, sign, "web" },
        }, false, 1)
        {
            ListenerIdentifiers = new HashSet<string>() { "access" };
        }

        public override CallResult<BitMartFuturesLoginResponse> HandleMessage(SocketConnection connection, DataEvent<BitMartFuturesLoginResponse> message)
        {
            if (message.Data.Success != true)
                return new CallResult<BitMartFuturesLoginResponse>(new ServerError(message.Data.ErrorMessage!));

            return message.ToCallResult(message.Data);
        }
    }
}
