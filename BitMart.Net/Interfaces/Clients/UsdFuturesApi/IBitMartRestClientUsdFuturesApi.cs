using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.CommonClients;
using System;

namespace BitMart.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// BitMart UsdFutures API endpoints
    /// </summary>
    public interface IBitMartRestClientUsdFuturesApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IBitMartRestClientUsdFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to sub account management
        /// </summary>
        public IBitMartRestClientUsdFuturesApiSubAccount SubAccount { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        public IBitMartRestClientUsdFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBitMartRestClientUsdFuturesApiTrading Trading { get; }
    }
}
