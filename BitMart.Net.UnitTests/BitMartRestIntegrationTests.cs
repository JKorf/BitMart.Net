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
        public override bool Run { get; set; } = true;

        public BitMartRestIntegrationTests()
        {
        }

        public override BitMartRestClient GetClient(ILoggerFactory loggerFactory, bool useUpdatedDeserialization)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");
            var pass = Environment.GetEnvironmentVariable("APIPASS");

            Authenticated = key != null && sec != null;
            return new BitMartRestClient(null, loggerFactory, Options.Create(new BitMartRestOptions
            {
                OutputOriginalData = true,
                UseUpdatedDeserialization = useUpdatedDeserialization,
                ApiCredentials = Authenticated ? new ApiCredentials(key, sec, pass) : null
            }));
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestErrorResponseParsing(bool useUpdatedDeserialization)
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient(useUpdatedDeserialization).SpotApi.ExchangeData.GetOrderBookAsync("TST_TST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.ErrorCode, Is.EqualTo("70002"));
            Assert.That(result.Error.ErrorType, Is.EqualTo(ErrorType.UnknownSymbol));
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSpotAccount(bool useUpdatedDeserialization)
        {
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.Account.GetFundingBalancesAsync(default, default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.Account.GetSpotBalancesAsync(default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.Account.GetWithdrawalQuotaAsync("ETH", default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.Account.GetBaseTradeFeesAsync(default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.Account.GetSymbolTradeFeeAsync("ETH_USDT", default), true);
            
            // Needs margin account
            //await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.Account.GetIsolatedMarginAccountsAsync(default, default), true);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSpotMargin(bool useUpdatedDeserialization)
        {
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.Margin.GetBorrowHistoryAsync("ETH_USDT", default, default, default, default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.Margin.GetRepayHistoryAsync("ETH_USDT", default, default, default, default, default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.Margin.GetBorrowInfoAsync(default, default), true);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSpotExchangeData(bool useUpdatedDeserialization)
        {
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.ExchangeData.GetAssetsAsync(default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.ExchangeData.GetSymbolsAsync(default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.ExchangeData.GetSymbolNamesAsync(default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.ExchangeData.GetTickerAsync("ETH_USDT", default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.ExchangeData.GetTickersAsync(default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.ExchangeData.GetAssetDepositWithdrawInfoAsync(default, default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.ExchangeData.GetServerStatusAsync(default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.ExchangeData.GetKlinesAsync("ETH_USDT", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.ExchangeData.GetKlineHistoryAsync("ETH_USDT", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.ExchangeData.GetTradesAsync("ETH_USDT", default, default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETH_USDT", default, default), false);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSpotTrading(bool useUpdatedDeserialization)
        {
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.Trading.GetOpenOrdersAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.Trading.GetClosedOrdersAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.SpotApi.Trading.GetUserTradesAsync(default, default, default, default, default, default), true);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestUsdFuturesAccount(bool useUpdatedDeserialization)
        {
            await RunAndCheckResult(useUpdatedDeserialization, client => client.UsdFuturesApi.Account.GetBalancesAsync(default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.UsdFuturesApi.Account.GetTransferHistoryAsync(default, default, default, default, default, default), true);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestUsdFuturesExchangeData(bool useUpdatedDeserialization)
        {
            await RunAndCheckResult(useUpdatedDeserialization, client => client.UsdFuturesApi.ExchangeData.GetContractsAsync(default, default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.UsdFuturesApi.ExchangeData.GetOrderBookAsync("ETHUSDT", default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.UsdFuturesApi.ExchangeData.GetOpenInterestAsync("ETHUSDT", default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.UsdFuturesApi.ExchangeData.GetCurrentFundingRateAsync("ETHUSDT", default), false);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.UsdFuturesApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.FuturesKlineInterval.OneDay, DateTime.UtcNow.AddDays(-3), DateTime.UtcNow.AddHours(-1), default), false);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestUsdFuturesTrading(bool useUpdatedDeserialization)
        {
            await RunAndCheckResult(useUpdatedDeserialization, client => client.UsdFuturesApi.Trading.GetClosedOrdersAsync("ETHUSDT", default, default, default, default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.UsdFuturesApi.Trading.GetOpenOrdersAsync(default, default, default, default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.UsdFuturesApi.Trading.GetTriggerOrdersAsync(default, default, default, default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.UsdFuturesApi.Trading.GetPositionsAsync(default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.UsdFuturesApi.Trading.GetPositionRiskAsync(default, default), true);
            await RunAndCheckResult(useUpdatedDeserialization, client => client.UsdFuturesApi.Trading.GetUserTradesAsync("ETHUSDT", default, default, default), true);
        }

        [Test]
        public async Task TestOrderBooks()
        {
            await TestOrderBook(new BitMartSpotSymbolOrderBook("ETH_USDT"));
            await TestOrderBook(new BitMartUsdFuturesSymbolOrderBook("ETHUSDT"));
        }
    }
}
