using BitMart.Net.Clients;
using BitMart.Net.Objects;
using BitMart.Net.Objects.Options;
using BitMart.Net.SymbolOrderBooks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMart.Net.UnitTests
{
    [NonParallelizable]
    internal class BitMartRestIntegrationTests : RestIntegrationTest<BitMartRestClient>
    {
        public override bool Run { get; set; } = false;

        public BitMartRestIntegrationTests()
        {
        }

        public override BitMartRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");
            var pass = Environment.GetEnvironmentVariable("APIPASS");

            Authenticated = key != null && sec != null;
            return new BitMartRestClient(null, loggerFactory, Options.Create(new BitMartRestOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new BitMartCredentials(key, sec, pass) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().SpotApi.ExchangeData.GetOrderBookAsync("TST_TST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.ErrorCode, Is.EqualTo("70002"));
            Assert.That(result.Error.ErrorType, Is.EqualTo(ErrorType.UnknownSymbol));
        }

        [Test]
        public async Task TestSpotAccount()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetFundingBalancesAsync(default, default, default), true, "data.wallet");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetSpotBalancesAsync(default), true, "data.wallet");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetWithdrawalQuotaAsync("ETH", default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetBaseTradeFeesAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Account.GetSymbolTradeFeeAsync("ETH_USDT", default), true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);

            // Needs margin account
            //await RunAndCheckResult(client => client.SpotApi.Account.GetIsolatedMarginAccountsAsync(default, default), true);
        }

        [Test]
        public async Task TestSpotMargin()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.SpotApi.Margin.GetBorrowHistoryAsync("ETH_USDT", default, default, default, default, default), true, "data.records");
            await RunAndCheckResult(warnings, client => client.SpotApi.Margin.GetRepayHistoryAsync("ETH_USDT", default, default, default, default, default, default), true, "data.records");
            await RunAndCheckResult(warnings, client => client.SpotApi.Margin.GetBorrowInfoAsync(default, default), true, "data.symbols");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetAssetsAsync(default), false, "data.currencies");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetSymbolsAsync(default), false, "data.symbols");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetSymbolNamesAsync(default), false, "data.symbols");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetTickerAsync("ETH_USDT", default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetTickersAsync(default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetAssetDepositWithdrawInfoAsync(default, default), false, "data.currencies");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetServerStatusAsync(default), false, "data.service");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetKlinesAsync("ETH_USDT", Enums.KlineInterval.OneDay, default, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetKlineHistoryAsync("ETH_USDT", Enums.KlineInterval.OneDay, default, default, default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetTradesAsync("ETH_USDT", default, default), false, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETH_USDT", default, default), false, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestSpotTrading()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetOpenOrdersAsync(default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetClosedOrdersAsync(default, default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.SpotApi.Trading.GetUserTradesAsync(default, default, default, default, default, default), true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestUsdFuturesAccount()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.UsdFuturesApi.Account.GetBalancesAsync(default), true, "data");
            await RunAndCheckResult(warnings, client => client.UsdFuturesApi.Account.GetTransferHistoryAsync(default, default, default, default, default, default), true, "data.records");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestUsdFuturesExchangeData()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.UsdFuturesApi.ExchangeData.GetContractsAsync(default, default), false, "data.symbols", ignoreProperties: ["tradfi_info"]);
            await RunAndCheckResult(warnings, client => client.UsdFuturesApi.ExchangeData.GetOrderBookAsync("ETHUSDT", default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdFuturesApi.ExchangeData.GetOpenInterestAsync("ETHUSDT", default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdFuturesApi.ExchangeData.GetCurrentFundingRateAsync("ETHUSDT", default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdFuturesApi.ExchangeData.GetCurrentFundingRatesAsync(default), false, "data");
            await RunAndCheckResult(warnings, client => client.UsdFuturesApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.FuturesKlineInterval.OneDay, DateTime.UtcNow.AddDays(-3), DateTime.UtcNow.AddHours(-1), default), false, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestUsdFuturesTrading()
        {
            var warnings = new List<Exception>();
            await RunAndCheckResult(warnings, client => client.UsdFuturesApi.Trading.GetClosedOrdersAsync("ETHUSDT", default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.UsdFuturesApi.Trading.GetOpenOrdersAsync(default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.UsdFuturesApi.Trading.GetTriggerOrdersAsync(default, default, default, default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.UsdFuturesApi.Trading.GetPositionsAsync(default, default), true, "data");
            await RunAndCheckResult(warnings, client => client.UsdFuturesApi.Trading.GetPositionRiskAsync(default, default), true, "data", ignoreProperties: ["account"]);
            await RunAndCheckResult(warnings, client => client.UsdFuturesApi.Trading.GetUserTradesAsync("ETHUSDT", default, default, default, default, default), true, "data");
            foreach (var warning in warnings)
                Assert.Warn(warning.Message);
        }

        [Test]
        public async Task TestOrderBooks()
        {
            await TestOrderBook(new BitMartSpotSymbolOrderBook("ETH_USDT"));
            await TestOrderBook(new BitMartUsdFuturesSymbolOrderBook("ETHUSDT"));
        }
    }
}
