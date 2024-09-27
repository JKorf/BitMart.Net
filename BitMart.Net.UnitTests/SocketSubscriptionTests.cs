using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System.Threading.Tasks;
using BitMart.Net.Clients;
using BitMart.Net.Objects.Models;
using BitMart.Net.Objects;
using System.Collections.Generic;

namespace BitMart.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateSpotSubscriptions()
        {
            var client = new BitMartSocketClient(opts =>
            {
                opts.ApiCredentials = new BitMartApiCredentials("123", "456", "XXX");
            });
            var tester = new SocketSubscriptionValidator<BitMartSocketClient>(client, "Subscriptions/Spot", "wss://ws-manager-compress.bitmart.com", "data", stjCompare: true);
            await tester.ValidateAsync<BitMartTickerUpdate>((client, handler) => client.SpotApi.SubscribeToTickerUpdatesAsync("ETH_USDT", handler), "Ticker", useFirstUpdateItem: true, ignoreProperties: new List<string> { "s_t" });
            await tester.ValidateAsync<IEnumerable<BitMartKlineUpdate>>((client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETH_USDT", Enums.KlineStreamInterval.OneMonth, handler), "Kline");
            await tester.ValidateAsync<BitMartOrderBookUpdate>((client, handler) => client.SpotApi.SubscribeToPartialOrderBookUpdatesAsync("ETH_USDT", 5, handler), "PartialBook", useFirstUpdateItem: true);
            await tester.ValidateAsync<BitMartOrderBookIncrementalUpdate>((client, handler) => client.SpotApi.SubscribeToOrderBookUpdatesAsync("ETH_USDT", handler), "OrderBook", useFirstUpdateItem: true);
            await tester.ValidateAsync<IEnumerable<BitMartTradeUpdate>>((client, handler) => client.SpotApi.SubscribeToTradeUpdatesAsync("ETH_USDT", handler), "Trades", ignoreProperties: new List<string> { "s_t" });
            await tester.ValidateAsync<BitMartOrderUpdate>((client, handler) => client.SpotApi.SubscribeToOrderUpdatesAsync(handler), "Order");
            await tester.ValidateAsync<BitMartBalanceUpdate>((client, handler) => client.SpotApi.SubscribeToBalanceUpdatesAsync(handler), "Balance");
        }

        [Test]
        public async Task ValidateFuturesSubscriptions()
        {
            var client = new BitMartSocketClient(opts =>
            {
                opts.ApiCredentials = new BitMartApiCredentials("123", "456", "XXX");
            });
            var tester = new SocketSubscriptionValidator<BitMartSocketClient>(client, "Subscriptions/Futures", "wss://openapi-ws.bitmart.com", "data", stjCompare: true);
            await tester.ValidateAsync<BitMartFuturesTickerUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToTickerUpdatesAsync(handler), "Ticker");
            await tester.ValidateAsync<IEnumerable<BitMartFuturesTradeUpdate>>((client, handler) => client.UsdFuturesApi.SubscribeToTradeUpdatesAsync("ETHUSDT", handler), "Trades");
            await tester.ValidateAsync<BitMartFuturesOrderBookUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToOrderBookUpdatesAsync("ETHUSDT", 5, handler), "OrderBook");
            await tester.ValidateAsync<BitMartFuturesKlineUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.FuturesStreamKlineInterval.OneDay, handler), "Klines");
            await tester.ValidateAsync<BitMartFuturesBalanceUpdate>((client, handler) => client.UsdFuturesApi.SubscribeToBalanceUpdatesAsync(handler), "Balances");
            await tester.ValidateAsync<IEnumerable<BitMartPositionUpdate>>((client, handler) => client.UsdFuturesApi.SubscribeToPositionUpdatesAsync(handler), "Positions");
            await tester.ValidateAsync<IEnumerable<BitMartFuturesOrderUpdateEvent>>((client, handler) => client.UsdFuturesApi.SubscribeToOrderUpdatesAsync(handler), "Orders");
        }
    }
}
