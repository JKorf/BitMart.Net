using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitMart.Net.Clients;
using BitMart.Net.Objects;
using System.Linq;
using BitMart.Net.Enums;

namespace BitMart.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateSpotAccountDataCalls()
        {
            var client = new BitMartRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new BitMartApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BitMartRestClient>(client, "Endpoints/Spot/ACcount", "https://api-cloud.bitmart.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.SpotApi.Account.GetFundingBalancesAsync("123"), "GetFundingBalances", nestedJsonProperty: "data.wallet");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetSpotBalancesAsync(), "GetSpotBalances", nestedJsonProperty: "data.wallet");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositAddressAsync("123"), "GetDepositAddress", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawalQuotaAsync("ETH"), "GetWithdrawalQuota", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.WithdrawAsync("123", 0.1m), "Withdraw", nestedJsonProperty: "data");
        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            var client = new BitMartRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new BitMartApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BitMartRestClient>(client, "Endpoints/Spot/ExchangeData", "https://api-cloud.bitmart.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTickerAsync("123"), "GetTicker", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAssetDepositWithdrawInfoAsync(), "GetAssetDepositWithdrawInfo", nestedJsonProperty: "data.currencies");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetServerStatusAsync(), "GetServerStatus", nestedJsonProperty: "data.service");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetKlinesAsync("123", KlineInterval.OneDay), "GetKlines", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetKlineHistoryAsync("123", KlineInterval.OneDay), "GetKlineHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTradesAsync("123"), "GetTrades", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetOrderBookAsync("123", 5), "GetOrderBook", nestedJsonProperty: "data");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestHeaders.Any(x => x.Key == "X-BM-SIGN");
        }
    }
}
