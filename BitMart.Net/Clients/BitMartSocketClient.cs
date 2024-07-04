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
        /// <param name="loggerFactory">The logger factory</param>
        public BitMartSocketClient(ILoggerFactory? loggerFactory = null) : this((x) => { }, loggerFactory)
        {
        }

        /// <summary>
        /// Create a new instance of BitMartSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BitMartSocketClient(Action<BitMartSocketOptions> optionsDelegate) : this(optionsDelegate, null)
        {
        }

        /// <summary>
        /// Create a new instance of BitMartSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public BitMartSocketClient(Action<BitMartSocketOptions>? optionsDelegate, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "BitMart")
        {
            var options = BitMartSocketOptions.Default.Copy();
            optionsDelegate?.Invoke(options);
            Initialize(options);

            UsdFuturesApi = AddApiClient(new BitMartSocketClientUsdFuturesApi(_logger, options));
            SpotApi = AddApiClient(new BitMartSocketClientSpotApi(_logger, options));
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<BitMartSocketOptions> optionsDelegate)
        {
            var options = BitMartSocketOptions.Default.Copy();
            optionsDelegate(options);
            BitMartSocketOptions.Default = options;
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            
            UsdFuturesApi.SetApiCredentials(credentials);

            SpotApi.SetApiCredentials(credentials);

        }
    }
}
