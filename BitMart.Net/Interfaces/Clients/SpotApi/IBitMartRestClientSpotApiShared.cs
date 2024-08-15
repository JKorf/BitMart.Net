using CryptoExchange.Net.SharedApis.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Interfaces.Clients.SpotApi
{
    public interface IBitMartRestClientSpotApiShared :
        ITickerRestClient,
        ISpotSymbolRestClient,
        IKlineRestClient,
        IRecentTradeRestClient,
        IBalanceRestClient,
        ISpotOrderRestClient
    {
    }
}
