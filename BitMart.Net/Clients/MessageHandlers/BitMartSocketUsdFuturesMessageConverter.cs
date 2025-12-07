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

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("group").WithEqualContstraint("System"),
                ],
                StaticIdentifier = "pong"
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("action").WithEqualContstraint("access"),
                ],
                StaticIdentifier = "access"
            },


            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("group").WithNotEqualContstraint("System"),
                    new PropertyFieldReference("action"),
                ],
                TypeIdentifierCallback = x => $"{x.FieldValue("action")}-{x.FieldValue("group")}"
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("group").WithNotEqualContstraint("System"),
                ],
                TypeIdentifierCallback = x => $"{x.FieldValue("group")}"
            },
        ];
    }
}
