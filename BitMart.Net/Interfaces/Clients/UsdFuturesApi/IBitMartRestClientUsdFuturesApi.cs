using BitMart.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace BitMart.Net.Interfaces.Clients.UsdFuturesApi
{
    /// <summary>
    /// BitMart UsdFutures API endpoints
    /// </summary>
    public interface IBitMartRestClientUsdFuturesApi : IRestApiClient<BitMartCredentials>, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IBitMartRestClientUsdFuturesApiAccount"/>
        public IBitMartRestClientUsdFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to sub account management
        /// </summary>
        /// <see cref="IBitMartRestClientUsdFuturesApiSubAccount"/>
        public IBitMartRestClientUsdFuturesApiSubAccount SubAccount { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IBitMartRestClientUsdFuturesApiExchangeData"/>
        public IBitMartRestClientUsdFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IBitMartRestClientUsdFuturesApiTrading"/>
        public IBitMartRestClientUsdFuturesApiTrading Trading { get; }

        /// <summary>
        /// [V1] Get the shared rest requests client. For new implementations prefer using <see cref="SharedApi"/>
        /// </summary>
        public IBitMartRestClientUsdFuturesApiShared SharedClient { get; }
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IBitMartRestClientUsdFuturesSharedApi SharedApi { get; }
    }
}
