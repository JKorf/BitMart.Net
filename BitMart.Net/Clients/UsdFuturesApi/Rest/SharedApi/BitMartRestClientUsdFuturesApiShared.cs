using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Enums;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using BitMart.Net.Objects.Models;
using CryptoExchange.Net.Objects.Errors;
using Microsoft.Extensions.Logging;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    internal partial class BitMartRestClientUsdFuturesSharedApi : 
        SharedApiBase,
        IBitMartRestClientUsdFuturesApiShared,
        IBitMartRestClientUsdFuturesSharedApi
    {
        private readonly BitMartRestClientUsdFuturesApi _api;

        private const string _topicId = "BitMartFutures";
        private const string _exchangeName = "BitMart";
        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(BitMartExchange.Metadata, this);

        public BitMartRestClientUsdFuturesSharedApi(BitMartRestClientUsdFuturesApi api)
            : base(
                  SharedTransport.Rest,
                  api.Exchange,
                  new[] { TradingMode.PerpetualLinear, TradingMode.DeliveryLinear },
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                );
        }
    }
}
