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
using Microsoft.Extensions.Options;

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
        public BitMartRestClient(Action<BitMartRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        {
        }

        /// <summary>
        /// Create a new instance of the BitMartRestClient using provided options
        /// </summary>
        /// <param name="options">Option configuration</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public BitMartRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<BitMartRestOptions> options) : base(loggerFactory, "BitMart")
        {
            Initialize(options.Value);

            UsdFuturesApi = AddApiClient(new BitMartRestClientUsdFuturesApi(_logger, this, httpClient, options.Value));
            SpotApi = AddApiClient(new BitMartRestClientSpotApi(_logger, httpClient, options.Value));
        }

        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<BitMartRestOptions> optionsDelegate)
        {
            BitMartRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            UsdFuturesApi.SetApiCredentials(credentials);
            SpotApi.SetApiCredentials(credentials);
        }
    }
}
