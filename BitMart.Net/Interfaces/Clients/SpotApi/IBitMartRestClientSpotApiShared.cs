﻿using CryptoExchange.Net.SharedApis.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Interfaces.Clients.SpotApi
{
    public interface IBitMartRestClientSpotApiShared :
        IAssetsRestClient,
        IBalanceRestClient,
        IDepositRestClient,
        IKlineRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        ISpotOrderRestClient,
        ISpotSymbolRestClient,
        ITickerRestClient,
        IWithdrawalRestClient,
        IWithdrawRestClient
    {
    }
}
