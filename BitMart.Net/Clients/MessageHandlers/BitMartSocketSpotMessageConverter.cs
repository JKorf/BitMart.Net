using BitMart.Net.Objects.Internal;
using BitMart.Net.Objects.Models;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text.Json;

namespace BitMart.Net.Clients.MessageHandlers
{
    internal class BitMartSocketSpotMessageConverter : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BitMartExchange._serializerContext);

        public BitMartSocketSpotMessageConverter()
        {
            AddTopicMapping<BitMartUpdate<BitMartTickerUpdate[]>>(x => x.Data.First().Symbol);
            AddTopicMapping<BitMartUpdate<BitMartKlineUpdate[]>>(x => x.Data.First().Symbol);
            AddTopicMapping<BitMartUpdate<BitMartOrderBookUpdate[]>>(x => x.Data.First().Symbol);
            AddTopicMapping<BitMartUpdate<BitMartOrderBookIncrementalUpdate[]>>(x => x.Data.First().Symbol);
            AddTopicMapping<BitMartUpdate<BitMartTradeUpdate[]>>(x => x.Data.First().Symbol);
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("event"),
                    new PropertyFieldReference("topic"),
                ],
                TypeIdentifierCallback = x => $"{x.FieldValue("event")}:{x.FieldValue("topic")}"
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("table")
                ],
                TypeIdentifierCallback = x => x.FieldValue("table")!
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("event")
                ],
                TypeIdentifierCallback = x => $"{x.FieldValue("event")}"
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
