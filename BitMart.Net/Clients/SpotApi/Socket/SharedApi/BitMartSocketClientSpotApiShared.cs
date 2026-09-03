using BitMart.Net.Enums;
using BitMart.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BitMart.Net.Clients.SpotApi
{
    internal partial class BitMartSocketClientSpotSharedApi : 
        SharedApiBase,
        IBitMartSocketClientSpotApiShared,
        IBitMartSocketClientSpotSharedApi
    {
        private readonly BitMartSocketClientSpotApi _api;

        private const string _topicId = "BitMartSpot";
        private const string _exchangeName = "BitMart";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(BitMartExchange.Metadata, this);

        public BitMartSocketClientSpotSharedApi(BitMartSocketClientSpotApi api)
            : base(
                  SharedTransport.Socket,
                  api.Exchange,
                  new[] { TradingMode.Spot },
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeTickerOptions,
                SubscribeTradeOptions,
                SubscribeBookTickerOptions,
                SubscribeBalanceOptions,
                SubscribeSpotOrderOptions,
                SubscribeKlineOptions,
                SubscribeOrderBookOptions
                );
        }
    }
}
