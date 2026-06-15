using BitMart.Net.Interfaces.Clients;
using BitMart.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace BitMart.Net.Clients
{
    /// <inheritdoc />
    public class BitMartUserClientProvider : UserClientProvider<
        IBitMartRestClient,
        IBitMartSocketClient,
        BitMartRestOptions,
        BitMartSocketOptions,
        BitMartCredentials,
        BitMartEnvironment
        >, IBitMartUserClientProvider
    {
        /// <inheritdoc />
        public override string ExchangeName => BitMartExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public BitMartUserClientProvider(Action<BitMartOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        public BitMartUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<BitMartRestOptions> restOptions,
            IOptions<BitMartSocketOptions> socketOptions)
            : base(httpClient, loggerFactory, restOptions, socketOptions)
        {
        }

        /// <inheritdoc />
        protected override IBitMartRestClient ConstructRestClient(HttpClient client, ILoggerFactory? loggerFactory, IOptions<BitMartRestOptions> options)
            => new BitMartRestClient(client, loggerFactory, options);
        /// <inheritdoc />
        protected override IBitMartSocketClient ConstructSocketClient(ILoggerFactory? loggerFactory, IOptions<BitMartSocketOptions> options)
            => new BitMartSocketClient(options, loggerFactory);
    }
}
