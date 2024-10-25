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
        private readonly IServiceProvider _serviceProvider;

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
            IKlineRestClient restClient;
            IKlineSocketClient socketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                restClient = _serviceProvider.GetRequiredService<IBitMartRestClient>().SpotApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IBitMartSocketClient>().SpotApi.SharedClient;
            }
            else
            {
                restClient = _serviceProvider.GetRequiredService<IBitMartRestClient>().UsdFuturesApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IBitMartSocketClient>().UsdFuturesApi.SharedClient;
            }

            return new KlineTracker(
                _serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                restClient,
                socketClient,
                symbol,
                interval,
                limit,
                period
                );
        }

        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            IRecentTradeRestClient? restClient = null;
            ITradeSocketClient socketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                restClient = _serviceProvider.GetRequiredService<IBitMartRestClient>().SpotApi.SharedClient;
                socketClient = _serviceProvider.GetRequiredService<IBitMartSocketClient>().SpotApi.SharedClient;
            }
            else
            {
                socketClient = _serviceProvider.GetRequiredService<IBitMartSocketClient>().UsdFuturesApi.SharedClient;
            }

            return new TradeTracker(
                _serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(socketClient.Exchange),
                restClient,
                socketClient,
                symbol,
                limit,
                period
                );
        }
    }
}
