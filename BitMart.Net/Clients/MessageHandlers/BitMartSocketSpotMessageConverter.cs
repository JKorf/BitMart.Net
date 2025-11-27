using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Net.WebSockets;
using System.Text.Json;

namespace BitMart.Net.Clients.MessageHandlers
{
    internal class BitMartSocketSpotMessageConverter : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BitMartExchange._serializerContext);

        protected override MessageEvaluator[] TypeEvaluators { get; } = [

            new MessageEvaluator {
                Priority = 1,
                Fields = [
                    new PropertyFieldReference("event"),
                    new PropertyFieldReference("topic"),
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("event")}:{x.FieldValue("topic")}"
            },

            new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("table") { Constraint = x => x!.Equals("spot/user/orders", StringComparison.Ordinal) },
                ],
                StaticIdentifier = "spot/user/orders:ALL_SYMBOLS"
            },

            new MessageEvaluator {
                Priority = 3,
                Fields = [
                    new PropertyFieldReference("table") { Constraint = x => x!.Equals("spot/user/balance", StringComparison.Ordinal) },
                ],
                StaticIdentifier = "spot/user/balance:BALANCE_UPDATE"
            },

            new MessageEvaluator {
                Priority = 4,
                Fields = [
                    new PropertyFieldReference("table") 
                    { 
                        Constraint = x => {
                            return !x!.Equals("spot/user/balance", StringComparison.Ordinal)
                                && !x.Equals("spot/user/orders", StringComparison.Ordinal);
                        }
                    },
                    new PropertyFieldReference("symbol") { Depth = 3 },
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("table")}:{x.FieldValue("symbol")}"
            },

            new MessageEvaluator {
                Priority = 5,
                Fields = [
                    new PropertyFieldReference("event")
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("event")}"
            },

        ];

        public override string? GetTypeIdentifier(ReadOnlySpan<byte> data, WebSocketMessageType? webSocketMessageType)
        {
            if (data.Length == 4)
                return "pong";

            return base.GetTypeIdentifier(data, webSocketMessageType);
        }
    }
}
