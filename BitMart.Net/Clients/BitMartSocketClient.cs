using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Logging;
using System;
using BitMart.Net.Interfaces.Clients;
using BitMart.Net.Objects.Options;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Clients.SpotApi;
using BitMart.Net.Clients.UsdFuturesApi;
using Microsoft.Extensions.Options;

namespace BitMart.Net.Clients
{
    /// <inheritdoc cref="IBitMartSocketClient" />
    public class BitMartSocketClient : BaseSocketClient, IBitMartSocketClient
    {
        #region fields
        #endregion

        #region Api clients

         /// <inheritdoc />
        public IBitMartSocketClientUsdFuturesApi UsdFuturesApi { get; }

         /// <inheritdoc />
        public IBitMartSocketClientSpotApi SpotApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of BitMartSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BitMartSocketClient(Action<BitMartSocketOptions>? optionsDelegate = null)
            : this(Options.Create(ApplyOptionsDelegate(optionsDelegate)), null)
        {
        }

        /// <summary>
        /// Create a new instance of BitMartSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="options">Option configuration</param>
        public BitMartSocketClient(IOptions<BitMartSocketOptions> options, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "BitMart")
        {
            Initialize(options.Value);

            UsdFuturesApi = AddApiClient(new BitMartSocketClientUsdFuturesApi(_logger, options.Value));
            SpotApi = AddApiClient(new BitMartSocketClientSpotApi(_logger, options.Value));
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<BitMartSocketOptions> optionsDelegate)
        {
            BitMartSocketOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            UsdFuturesApi.SetApiCredentials(credentials);
            SpotApi.SetApiCredentials(credentials);
        }
    }
}
