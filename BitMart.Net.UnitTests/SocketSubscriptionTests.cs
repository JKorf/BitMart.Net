using BitMart.Net.Clients;
using BitMart.Net.Enums;
using BitMart.Net.Objects;
using BitMart.Net.Objects.Models;
using BitMart.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitMart.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [TestCase(false)]
        [TestCase(true)]
        public async Task ValidateConcurrentSpotSubscriptions(bool newDeserialization)
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new BitMartSocketClient(Options.Create(new BitMartSocketOptions
            {
                OutputOriginalData = true,
                UseUpdatedDeserialization = newDeserialization
            }), logger);

            var tester = new SocketSubscriptionValidator<BitMartSocketClient>(client, "Subscriptions/Spot", "wss://ws-manager-compress.bitmart.com", "data");
            await tester.ValidateConcurrentAsync<BitMartKlineUpdate[]>(
                (client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETH_USDT", KlineStreamInterval.OneDay, handler),
                (client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETH_USDT", KlineStreamInterval.OneHour, handler),
                "Concurrent");
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task ValidateSpotSubscriptions(bool newDeserialization)
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new BitMartSocketClient(Options.Create(new BitMartSocketOptions
            {
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456", "789"),
                OutputOriginalData = true,
                UseUpdatedDeserialization = newDeserialization
            }), logger);
            var tester = new SocketSubscriptionValidator<BitMartSocketClient>(client, "Subscriptions/Spot", "wss://ws-manager-compress.bitmart.com", "data");
            await tester.ValidateAsync<BitMartTickerUpdate>((client, handler) => client.SpotApi.SubscribeToTickerUpdatesAsync("ETH_USDT", handler), "Ticker", useFirstUpdateItem: true, ignoreProperties: new List<string> { "s_t" });
            await tester.ValidateAsync<BitMartKlineUpdate[]>((client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETH_USDT", Enums.KlineStreamInterval.OneMonth, handler), "Kline");
            await tester.ValidateAsync<BitMartOrderBookUpdate>((client, handler) => client.SpotApi.SubscribeToPartialOrderBookUpdatesAsync("ETH_USDT", 5, handler), "PartialBook", useFirstUpdateItem: true);
            await tester.ValidateAsync<BitMartOrderBookIncrementalUpdate>((client, handler) => client.SpotApi.SubscribeToOrderBookUpdatesAsync("ETH_USDT", handler), "OrderBook", useFirstUpdateItem: true);
            await tester.ValidateAsync<BitMartTradeUpdate[]>((client, handler) => client.SpotApi.SubscribeToTradeUpdatesAsync("ETH_USDT", handler), "Trades", ignoreProperties: new List<string> { "s_t" });
            await tester.ValidateAsync<BitMartOrderUpdate>((client, handler) => client.SpotApi.SubscribeToOrderUpdatesAsync(handler), "Order");
            await tester.ValidateAsync<BitMartBalanceUpdate>((client, handler) => client.SpotApi.SubscribeToBalanceUpdatesAsync(handler), "Balance");
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task ValidateConcurrentFuturesSubscriptions(bool newDeserialization)
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new BitMartSocketClient(Options.Create(new BitMartSocketOptions
            {
                OutputOriginalData = true,
                UseUpdatedDeserialization = newDeserialization
            }), logger);

            var tester = new SocketSubscriptionValidator<BitMartSocketClient>(client, "Subscriptions/Futures", "wss://openapi-ws.bitmart.com", "data");
            await tester.ValidateConcurrentAsync<BitMartFuturesKlineUpdate>(
                (client, handler) => client.UsdFuturesApi.SubscribeToKlineUpdatesAsync("ETHUSDT", FuturesStreamKlineInterval.OneDay, handler),
                (client, handler) => client.UsdFuturesApi.SubscribeToKlineUpdatesAsync("ETHUSDT", FuturesStreamKlineInterval.OneHour, handler),
                "Concurrent");
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task ValidateFuturesSubscriptions(bool newDeserialization)
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new BitMartSocketClient(Options.Create(new BitMartSocketOptions
            {
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456", "789"),
                OutputOriginalData = true,
                UseUpdatedDeserialization = newDeserialization
            }), logger);
            var tester = new SocketSubscriptionValidator<BitMartSocketClient>(client, "Subscriptions/Futures", "wss://openapi-ws.bitmart.com", "data");
            await tester.ValidateAsync<BitMartFuturesTickerUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToTickerUpdatesAsync(handler), "Ticker");
            await tester.ValidateAsync<BitMartFuturesTradeUpdate[]>((client, handler) => client.UsdFuturesApi.SubscribeToTradeUpdatesAsync("ETHUSDT", handler), "Trades");
            await tester.ValidateAsync<BitMartFuturesOrderBookUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToOrderBookUpdatesAsync("ETHUSDT", 5, handler), "OrderBook");
            await tester.ValidateAsync<BitMartFuturesKlineUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.FuturesStreamKlineInterval.OneDay, handler), "Klines");
            await tester.ValidateAsync<BitMartFuturesBalanceUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToBalanceUpdatesAsync(handler), "Balances");
            await tester.ValidateAsync<BitMartPositionUpdate[]>((client, handler) => client.UsdFuturesApi.SubscribeToPositionUpdatesAsync(handler), "Positions");
            await tester.ValidateAsync<BitMartFuturesOrderUpdateEvent[]>((client, handler) => client.UsdFuturesApi.SubscribeToOrderUpdatesAsync(handler), "Orders");
        }
    }
}
