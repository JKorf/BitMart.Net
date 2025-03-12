using CryptoExchange.Net.Interfaces;
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
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IBitMartRestClientSpotApiShared SharedClient { get; }

    }
}
