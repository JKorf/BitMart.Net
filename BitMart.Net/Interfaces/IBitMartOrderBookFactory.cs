using CryptoExchange.Net.Interfaces;
using System;
using BitMart.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

namespace BitMart.Net.Interfaces
{
    /// <summary>
    /// BitMart local order book factory
    /// </summary>
    public interface IBitMartOrderBookFactory : IExchangeService
    {
        /// <summary>
        /// UsdFutures order book factory methods
        /// </summary>
        IOrderBookFactory<BitMartOrderBookOptions> UsdFutures { get; }

        /// <summary>
        /// Spot order book factory methods
        /// </summary>
        IOrderBookFactory<BitMartOrderBookOptions> Spot { get; }

        /// <summary>
        /// Create a SymbolOrderBook for the symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook Create(SharedSymbol symbol, Action<BitMartOrderBookOptions>? options = null);

        /// <summary>
        /// Create a new UsdFutures local order book instance
        /// </summary>
        ISymbolOrderBook CreateUsdFutures(string symbol, Action<BitMartOrderBookOptions>? options = null);

        /// <summary>
        /// Create a new Spot local order book instance
        /// </summary>
        ISymbolOrderBook CreateSpot(string symbol, Action<BitMartOrderBookOptions>? options = null);

    }
}