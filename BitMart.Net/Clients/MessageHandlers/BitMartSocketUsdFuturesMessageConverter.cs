using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Net.WebSockets;
using System.Text.Json;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    internal class BitMartSocketUsdFuturesMessageConverter : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BitMartExchange._serializerContext);

        protected override MessageEvaluator[] TypeEvaluators { get; } = [

            new MessageEvaluator {
                Priority = 1,
                Fields = [
                    new PropertyFieldReference("group") { Constraint = x => x!.Equals("System", StringComparison.Ordinal) },
                ],
                StaticIdentifier = "pong"
            },

            new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("action") { Constraint = x => x!.Equals("access", StringComparison.Ordinal) },
                ],
                StaticIdentifier = "access"
            },


            new MessageEvaluator {
                Priority = 3,
                Fields = [
                    new PropertyFieldReference("group") { Constraint = x => !x!.Equals("System", StringComparison.Ordinal) },
                    new PropertyFieldReference("action"),
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("action")}-{x.FieldValue("group")}"
            },

            new MessageEvaluator {
                Priority = 4,
                Fields = [
                    new PropertyFieldReference("group") { Constraint = x => !x!.Equals("System", StringComparison.Ordinal) },
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("group")}"
            },
        ];
    }
}
