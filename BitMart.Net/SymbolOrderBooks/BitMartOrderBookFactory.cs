using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.OrderBook;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using BitMart.Net.Interfaces;
using BitMart.Net.Interfaces.Clients;
using BitMart.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

namespace BitMart.Net.SymbolOrderBooks
{
    /// <summary>
    /// BitMart order book factory
    /// </summary>
    public class BitMartOrderBookFactory : IBitMartOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public BitMartOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
                        
            UsdFutures = new OrderBookFactory<BitMartOrderBookOptions>(CreateUsdFutures, Create);
            Spot = new OrderBookFactory<BitMartOrderBookOptions>(CreateSpot, Create);
        }

         /// <inheritdoc />
        public IOrderBookFactory<BitMartOrderBookOptions> UsdFutures { get; }

         /// <inheritdoc />
        public IOrderBookFactory<BitMartOrderBookOptions> Spot { get; }

        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<BitMartOrderBookOptions>? options = null)
        {
            var symbolName = symbol.GetSymbol(BitMartExchange.FormatSymbol);
            if (symbol.TradingMode == TradingMode.Spot)
                return CreateSpot(symbolName, options);

            return CreateUsdFutures(symbolName, options);
        }

        /// <inheritdoc />
        public ISymbolOrderBook CreateUsdFutures(string symbol, Action<BitMartOrderBookOptions>? options = null)
            => new BitMartUsdFuturesSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<IBitMartRestClient>(),
                                                          _serviceProvider.GetRequiredService<IBitMartSocketClient>());

         /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<BitMartOrderBookOptions>? options = null)
            => new BitMartSpotSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<IBitMartRestClient>(),
                                                          _serviceProvider.GetRequiredService<IBitMartSocketClient>());


    }
}
