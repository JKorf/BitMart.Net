using BitMart.Net.Clients;
using BitMart.Net.Interfaces;
using BitMart.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
    }
}
