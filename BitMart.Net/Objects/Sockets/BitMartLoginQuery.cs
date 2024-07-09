using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using BitMart.Net.Objects.Models;
using BitMart.Net.Objects.Internal;
using System.Linq;

namespace BitMart.Net.Objects.Sockets
{
    internal class BitMartLoginQuery : Query<BitMartSocketResponse>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public BitMartLoginQuery(string key, string timestamp, string sign) : base(new BitMartSocketOperation
        {
            Operation = "login",
            Parameters = new[] { key, timestamp, sign },
        }, false, 1)
        {
            ListenerIdentifiers = new HashSet<string>() { "login" };
        }

        public override CallResult<BitMartSocketResponse> HandleMessage(SocketConnection connection, DataEvent<BitMartSocketResponse> message)
        {
            if (message.Data.ErrorCode != null)
                return new CallResult<BitMartSocketResponse>(new ServerError(message.Data.ErrorCode.Value, message.Data.ErrorMessage!));

            return new CallResult<BitMartSocketResponse>(message.Data, message.OriginalData, null);
        }
    }
}
