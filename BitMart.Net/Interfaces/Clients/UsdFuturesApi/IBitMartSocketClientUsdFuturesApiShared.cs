using CryptoExchange.Net.SharedApis.Interfaces;
using CryptoExchange.Net.SharedApis.Interfaces.Socket;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Interfaces.Clients.UsdFuturesApi
{
    public interface IBitMartSocketClientUsdFuturesApiShared :
        //ITickerSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IBalanceSocketClient,
        IKlineSocketClient
    {
    }
}
