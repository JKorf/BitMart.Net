using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using System.Text.Json;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    internal class BitMartSocketUsdFuturesMessageConverter : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BitMartExchange._serializerContext);

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("group").WithEqualConstraint("System"),
                ],
                StaticIdentifier = "pong"
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("action").WithEqualConstraint("access"),
                ],
                StaticIdentifier = "access"
            },


            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("group").WithNotEqualConstraint("System"),
                    new PropertyFieldReference("action"),
                ],
                TypeIdentifierCallback = x => $"{x.FieldValue("action")}-{x.FieldValue("group")}"
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("group").WithNotEqualConstraint("System"),
                ],
                TypeIdentifierCallback = x => $"{x.FieldValue("group")}"
            },
        ];
    }
}
