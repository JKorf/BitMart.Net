using BitMart.Net.Interfaces.Clients;
using BitMart.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;

namespace BitMart.Net.Clients
{
    /// <inheritdoc />
    public class BitMartUserClientProvider : IBitMartUserClientProvider
    {
        private static ConcurrentDictionary<string, IBitMartRestClient> _restClients = new ConcurrentDictionary<string, IBitMartRestClient>();
        private static ConcurrentDictionary<string, IBitMartSocketClient> _socketClients = new ConcurrentDictionary<string, IBitMartSocketClient>();

        private readonly IOptions<BitMartRestOptions> _restOptions;
        private readonly IOptions<BitMartSocketOptions> _socketOptions;
        private readonly HttpClient _httpClient;
        private readonly ILoggerFactory? _loggerFactory;

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
        {
            _httpClient = httpClient ?? new HttpClient();
            _loggerFactory = loggerFactory;
            _restOptions = restOptions;
            _socketOptions = socketOptions;
        }

        /// <inheritdoc />
        public void InitializeUserClient(string userIdentifier, ApiCredentials credentials, BitMartEnvironment? environment = null)
        {
            CreateRestClient(userIdentifier, credentials, environment);
            CreateSocketClient(userIdentifier, credentials, environment);
        }

        /// <inheritdoc />
        public IBitMartRestClient GetRestClient(string userIdentifier, ApiCredentials? credentials = null, BitMartEnvironment? environment = null)
        {
            if (!_restClients.TryGetValue(userIdentifier, out var client))
                client = CreateRestClient(userIdentifier, credentials, environment);

            return client;
        }

        /// <inheritdoc />
        public IBitMartSocketClient GetSocketClient(string userIdentifier, ApiCredentials? credentials = null, BitMartEnvironment? environment = null)
        {
            if (!_socketClients.TryGetValue(userIdentifier, out var client))
                client = CreateSocketClient(userIdentifier, credentials, environment);

            return client;
        }

        private IBitMartRestClient CreateRestClient(string userIdentifier, ApiCredentials? credentials, BitMartEnvironment? environment)
        {
            var clientRestOptions = SetRestEnvironment(environment);
            var client = new BitMartRestClient(_httpClient, _loggerFactory, clientRestOptions);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _restClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IBitMartSocketClient CreateSocketClient(string userIdentifier, ApiCredentials? credentials, BitMartEnvironment? environment)
        {
            var clientSocketOptions = SetSocketEnvironment(environment);
            var client = new BitMartSocketClient(clientSocketOptions!, _loggerFactory);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _socketClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IOptions<BitMartRestOptions> SetRestEnvironment(BitMartEnvironment? environment)
        {
            if (environment == null)
                return _restOptions;

            var newRestClientOptions = new BitMartRestOptions();
            var restOptions = _restOptions.Value.Set(newRestClientOptions);
            newRestClientOptions.Environment = environment;
            return Options.Create(newRestClientOptions);
        }

        private IOptions<BitMartSocketOptions> SetSocketEnvironment(BitMartEnvironment? environment)
        {
            if (environment == null)
                return _socketOptions;

            var newSocketClientOptions = new BitMartSocketOptions();
            var restOptions = _socketOptions.Value.Set(newSocketClientOptions);
            newSocketClientOptions.Environment = environment;
            return Options.Create(newSocketClientOptions);
        }

        private static T ApplyOptionsDelegate<T>(Action<T>? del) where T : new()
        {
            var opts = new T();
            del?.Invoke(opts);
            return opts;
        }
    }
}
