using CryptoExchange.Net.Interfaces.Clients;
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
        /// <see cref="IBitMartRestClientSpotApiAccount"/>
        public IBitMartRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IBitMartRestClientSpotApiExchangeData"/>
        public IBitMartRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to margin borrow and repayment
        /// </summary>
        /// <see cref="IBitMartRestClientSpotApiMargin"/>
        public IBitMartRestClientSpotApiMargin Margin { get; }

        /// <summary>
        /// Endpoints related to sub account management
        /// </summary>
        /// <see cref="IBitMartRestClientSpotApiSubAccount"/>
        public IBitMartRestClientSpotApiSubAccount SubAccount { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IBitMartRestClientSpotApiTrading"/>
        public IBitMartRestClientSpotApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        IBitMartRestClientSpotApiShared SharedClient { get; }

    }
}
