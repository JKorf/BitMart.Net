using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    internal partial class BitMartSocketClientUsdFuturesSharedApi : 
        SharedApiBase,
        IBitMartSocketClientUsdFuturesApiShared,
        IBitMartSocketClientUsdFuturesSharedApi
    {
        private readonly BitMartSocketClientUsdFuturesApi _api;

        private const string _topicId = "BitMartFutures";
        private const string _exchangeName = "BitMart";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(BitMartExchange.Metadata, this);

        public BitMartSocketClientUsdFuturesSharedApi(BitMartSocketClientUsdFuturesApi api)
        : base(
                  SharedTransport.Socket,
                  api.Exchange,
                  new[] { TradingMode.PerpetualLinear, TradingMode.DeliveryLinear },
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeTickerOptions,
                SubscribeAllTickersOptions,
                SubscribeTradeOptions,
                SubscribeBookTickerOptions,
                SubscribeBalanceOptions,
                SubscribeKlineOptions,
                SubscribeFuturesOrderOptions,
                SubscribePositionOptions,
                SubscribeOrderBookOptions
                );
        }
    }
}
