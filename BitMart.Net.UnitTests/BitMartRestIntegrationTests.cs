using BitMart.Net.Clients;
using BitMart.Net.Objects;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitMart.Net.UnitTests
{
    [NonParallelizable]
    internal class BitMartRestIntegrationTests : RestIntergrationTest<BitMartRestClient>
    {
        public override bool Run { get; set; }

        public BitMartRestIntegrationTests()
        {
        }

        public override BitMartRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");
            var pass = Environment.GetEnvironmentVariable("APIPASS");

            Authenticated = key != null && sec != null;
            return new BitMartRestClient(null, loggerFactory, opts =>
            {
                opts.OutputOriginalData = true;
                opts.ApiCredentials = Authenticated ? new BitMartApiCredentials(key, sec, pass) : null;
            });
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().SpotApi.ExchangeData.GetOrderBookAsync("TST_TST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.Code, Is.EqualTo(70002));
        }

        [Test]
        public async Task TestSpotAccount()
        {
            await RunAndCheckResult(client => client.SpotApi.Account.GetFundingBalancesAsync(default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetSpotBalancesAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetWithdrawalQuotaAsync("ETH", default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetBaseTradeFeesAsync(default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetSymbolTradeFeeAsync("ETH_USDT", default), true);
            
            // Needs margin account
            //await RunAndCheckResult(client => client.SpotApi.Account.GetIsolatedMarginAccountsAsync(default, default), true);
        }

        [Test]
        public async Task TestSpotMargin()
        {
            await RunAndCheckResult(client => client.SpotApi.Margin.GetBorrowHistoryAsync("ETH_USDT", default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Margin.GetRepayHistoryAsync("ETH_USDT", default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Margin.GetBorrowInfoAsync(default, default), true);
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAssetsAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetSymbolsAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetSymbolNamesAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTickerAsync("ETH_USDT", default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTickersAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAssetDepositWithdrawInfoAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetServerStatusAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetKlinesAsync("ETH_USDT", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetKlineHistoryAsync("ETH_USDT", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTradesAsync("ETH_USDT", default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETH_USDT", default, default), false);
        }

        [Test]
        public async Task TestSpotTrading()
        {
            await RunAndCheckResult(client => client.SpotApi.Trading.GetOpenOrdersAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetClosedOrdersAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetUserTradesAsync(default, default, default, default, default, default), true);
        }

        [Test]
        public async Task TestUsdFuturesAccount()
        {
            await RunAndCheckResult(client => client.UsdFuturesApi.Account.GetBalancesAsync(default), true);
            await RunAndCheckResult(client => client.UsdFuturesApi.Account.GetTransferHistoryAsync(default, default, default, default, default, default), true);
        }

        [Test]
        public async Task TestUsdFuturesExchangeData()
        {
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetContractsAsync(default, default), false);
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetOrderBookAsync("ETHUSDT", default), false);
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetOpenInterestAsync("ETHUSDT", default), false);
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetCurrentFundingRateAsync("ETHUSDT", default), false);
            await RunAndCheckResult(client => client.UsdFuturesApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.FuturesKlineInterval.OneDay, DateTime.UtcNow.AddDays(-3), DateTime.UtcNow.AddHours(-1), default), false);
        }

        [Test]
        public async Task TestUsdFuturesTrading()
        {
            await RunAndCheckResult(client => client.UsdFuturesApi.Trading.GetClosedOrdersAsync("ETHUSDT", default, default, default), true);
            await RunAndCheckResult(client => client.UsdFuturesApi.Trading.GetOpenOrdersAsync(default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdFuturesApi.Trading.GetTriggerOrdersAsync(default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdFuturesApi.Trading.GetPositionsAsync(default, default), true);
            await RunAndCheckResult(client => client.UsdFuturesApi.Trading.GetPositionRiskAsync(default, default), true);
            await RunAndCheckResult(client => client.UsdFuturesApi.Trading.GetUserTradesAsync("ETHUSDT", default, default, default), true);
        }
    }
}
