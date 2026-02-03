using BitMart.Net.Clients;
using BitMart.Net.Interfaces;
using BitMart.Net.Interfaces.Clients;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace BitMart.Net
{
    /// <inheritdoc />
    public class BitMartTrackerFactory : IBitMartTrackerFactory
    {
        private readonly IServiceProvider? _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public BitMartTrackerFactory()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public BitMartTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public bool CanCreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval)
        {
            var client = (_serviceProvider?.GetRequiredService<IBitMartSocketClient>() ?? new BitMartSocketClient());
            SubscribeKlineOptions klineOptions = symbol.TradingMode == TradingMode.Spot ? client.SpotApi.SharedClient.SubscribeKlineOptions : client.UsdFuturesApi.SharedClient.SubscribeKlineOptions;
            return klineOptions.IsSupported(interval);
        }

        /// <inheritdoc />
        public bool CanCreateTradeTracker(SharedSymbol symbol) => true;

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IBitMartRestClient>() ?? new BitMartRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IBitMartSocketClient>() ?? new BitMartSocketClient();

            IKlineRestClient sharedRestClient;
            IKlineSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.UsdFuturesApi.SharedClient;
                sharedSocketClient = socketClient.UsdFuturesApi.SharedClient;
            }

            return new KlineTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                sharedRestClient,
                sharedSocketClient,
                symbol,
                interval,
                limit,
                period
                );
        }

        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IBitMartRestClient>() ?? new BitMartRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IBitMartSocketClient>() ?? new BitMartSocketClient();

            IRecentTradeRestClient? sharedRestClient = null;
            ITradeSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.UsdFuturesApi.SharedClient;
                sharedSocketClient = socketClient.UsdFuturesApi.SharedClient;
            }

            return new TradeTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(socketClient.Exchange),
                sharedRestClient,
                null,
                sharedSocketClient,
                symbol,
                limit,
                period
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig config)
        {
            var restClient = _serviceProvider?.GetRequiredService<IBitMartRestClient>() ?? new BitMartRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IBitMartSocketClient>() ?? new BitMartSocketClient();
            return new BitMartUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BitMartUserSpotDataTracker>>() ?? new NullLogger<BitMartUserSpotDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, SpotUserDataTrackerConfig config, ApiCredentials credentials, BitMartEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IBitMartUserClientProvider>() ?? new BitMartUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new BitMartUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BitMartUserSpotDataTracker>>() ?? new NullLogger<BitMartUserSpotDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(FuturesUserDataTrackerConfig config)
        {
            var restClient = _serviceProvider?.GetRequiredService<IBitMartRestClient>() ?? new BitMartRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IBitMartSocketClient>() ?? new BitMartSocketClient();
            return new BitMartUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BitMartUserFuturesDataTracker>>() ?? new NullLogger<BitMartUserFuturesDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, FuturesUserDataTrackerConfig config, ApiCredentials credentials, BitMartEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IBitMartUserClientProvider>() ?? new BitMartUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new BitMartUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<BitMartUserFuturesDataTracker>>() ?? new NullLogger<BitMartUserFuturesDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }
    }
}
