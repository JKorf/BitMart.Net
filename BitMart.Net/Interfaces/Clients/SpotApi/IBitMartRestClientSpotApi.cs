using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.CommonClients;
using System;

namespace BitMart.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// BitMart Spot API endpoints
    /// </summary>
    public interface IBitMartRestClientSpotApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IBitMartRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        public IBitMartRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to margin borrow and repayment
        /// </summary>
        public IBitMartRestClientSpotApiMargin Margin { get; }

        /// <summary>
        /// Endpoints related to sub account management
        /// </summary>
        public IBitMartRestClientSpotApiSubAccount SubAccount { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IBitMartRestClientSpotApiTrading Trading { get; }

        /// <summary>
        /// Get the ISpotClient for this client. This is a common interface which allows for some basic operations without knowing any details of the exchange.
        /// </summary>
        /// <returns></returns>
        public ISpotClient CommonSpotClient { get; }
        IBitMartRestClientSpotApiShared SharedClient { get; }

    }
}
