using CryptoExchange.Net.SharedApis.Interfaces.Rest;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Futures;
using CryptoExchange.Net.SharedApis.Interfaces.Rest.Spot;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Interfaces.Clients.SpotApi
{
    public interface IBitMartRestClientUsdFuturesApiShared :
        IBalanceRestClient,
        IFuturesTickerRestClient,
        IFuturesSymbolRestClient,
        IKlineRestClient,
        ILeverageRestClient,
        IOrderBookRestClient,
        IOpenInterestRestClient,
        IFuturesOrderRestClient
    {
    }
}
