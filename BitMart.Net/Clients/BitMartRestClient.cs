using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using CryptoExchange.Net.Authentication;
using BitMart.Net.Interfaces.Clients;
using BitMart.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Clients.UsdFuturesApi;
using BitMart.Net.Clients.SpotApi;

namespace BitMart.Net.Clients
{
    /// <inheritdoc cref="IBitMartRestClient" />
    public class BitMartRestClient : BaseRestClient, IBitMartRestClient
    {
        #region Api clients
                
         /// <inheritdoc />
        public IBitMartRestClientUsdFuturesApi UsdFuturesApi { get; }

         /// <inheritdoc />
        public IBitMartRestClientSpotApi SpotApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of the BitMartRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BitMartRestClient(Action<BitMartRestOptions>? optionsDelegate = null) : this(null, null, optionsDelegate)
        {
        }

        /// <summary>
        /// Create a new instance of the BitMartRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public BitMartRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, Action<BitMartRestOptions>? optionsDelegate = null) : base(loggerFactory, "BitMart")
        {
            var options = BitMartRestOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            UsdFuturesApi = AddApiClient(new BitMartRestClientUsdFuturesApi(_logger, this, httpClient, options));
            SpotApi = AddApiClient(new BitMartRestClientSpotApi(_logger, httpClient, options));
        }

        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<BitMartRestOptions> optionsDelegate)
        {
            var options = BitMartRestOptions.Default.Copy();
            optionsDelegate(options);
            BitMartRestOptions.Default = options;
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            UsdFuturesApi.SetApiCredentials(credentials);
            SpotApi.SetApiCredentials(credentials);
        }
    }
}
