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
using System.Drawing;

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
                opts.ApiCredentials = new BitMartApiCredentials("123", "456", "XXX");
            });
            var tester = new RestRequestValidator<BitMartRestClient>(client, "Endpoints/Spot/Account", "https://api-cloud.bitmart.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.SpotApi.Account.GetFundingBalancesAsync("123"), "GetFundingBalances", nestedJsonProperty: "data.wallet");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetSpotBalancesAsync(), "GetSpotBalances", nestedJsonProperty: "data.wallet");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositAddressAsync("123"), "GetDepositAddress", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawalQuotaAsync("ETH"), "GetWithdrawalQuota", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.WithdrawAsync("123", 0.1m), "Withdraw", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositWithdrawalAsync("123"), "GetDepositWithdrawal", nestedJsonProperty: "data.record");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetIsolatedMarginAccountsAsync(), "GetIsolatedMarginAccount", nestedJsonProperty: "data.symbols");
            await tester.ValidateAsync(client => client.SpotApi.Account.IsolatedMarginTransferAsync("123", "123", 0.1m, TransferDirection.TransferIn), "IsolatedMarginTransfer", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetBaseTradeFeesAsync(), "GetTradeFees", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetSymbolTradeFeeAsync("123"), "GetSymbolTradeFee", nestedJsonProperty: "data");
        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            var client = new BitMartRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new BitMartApiCredentials("123", "456", "XXX");
            });
            var tester = new RestRequestValidator<BitMartRestClient>(client, "Endpoints/Spot/ExchangeData", "https://api-cloud.bitmart.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTickerAsync("123"), "GetTicker", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAssetDepositWithdrawInfoAsync(), "GetAssetDepositWithdrawInfo", nestedJsonProperty: "data.currencies");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetServerStatusAsync(), "GetServerStatus", nestedJsonProperty: "data.service");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetKlinesAsync("123", KlineInterval.OneDay), "GetKlines", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetKlineHistoryAsync("123", KlineInterval.OneDay), "GetKlineHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTradesAsync("123"), "GetTrades", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetOrderBookAsync("123", 5), "GetOrderBook", nestedJsonProperty: "data", ignoreProperties: new List<string> { "ts" });
        }

        [Test]
        public async Task ValidateSpotMarginDataCalls()
        {
            var client = new BitMartRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new BitMartApiCredentials("123", "456", "XXX");
            });
            var tester = new RestRequestValidator<BitMartRestClient>(client, "Endpoints/Spot/Margin", "https://api-cloud.bitmart.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.SpotApi.Margin.BorrowAsync("123", "123", 0.1m), "Borrow", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Margin.RepayAsync("123", "123", 0.1m), "Repay", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetBorrowHistoryAsync("123"), "GetBorrowHistory", nestedJsonProperty: "data.records");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetRepayHistoryAsync("123"), "GetRepayHistory", nestedJsonProperty: "data.records");
            await tester.ValidateAsync(client => client.SpotApi.Margin.GetBorrowInfoAsync(), "GetBorrowInfo", nestedJsonProperty: "data.symbols");
        }

        [Test]
        public async Task ValidateSpotSubAccountCalls()
        {
            var client = new BitMartRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new BitMartApiCredentials("123", "456", "XXX");
            });
            var tester = new RestRequestValidator<BitMartRestClient>(client, "Endpoints/Spot/SubAccount", "https://api-cloud.bitmart.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.TransferSubToMainForMainAsync("123", "123", 0.1m, "123"), "TransferSubToMainForMain");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.TransferSubToMainForSubAsync("123", "123", 0.1m), "TransferSubToMainForSub");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.TransferMainToSubAccountAsync("123", "123", 0.1m, "123"), "TransferMainToSubAccount");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.TransferSubAccountToSubAccountAsync("123", 0.1m, "123", "123", "123"), "TransferSubAccountToSubAccount");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.GetSubAccountTransferHistoryForMainAsync(123), "GetSubAccountTransferHistoryForMain", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.GetSubAccountTransferHistoryAsync(123), "GetSubAccountTransferHistory", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.GetSubAcccountBalanceAsync("123"), "GetSubAcccountBalance", nestedJsonProperty: "data.wallet");
            await tester.ValidateAsync(client => client.SpotApi.SubAccount.GetSubAccountListAsync(), "GetSubAccountList", nestedJsonProperty: "data.subAccountList");
        }

        [Test]
        public async Task ValidateSpotTradingDataCalls()
        {
            var client = new BitMartRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new BitMartApiCredentials("123", "456", "XXX");
            });
            var tester = new RestRequestValidator<BitMartRestClient>(client, "Endpoints/Spot/Trading", "https://api-cloud.bitmart.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceOrderAsync("123", OrderSide.Buy, OrderType.Market), "PlaceOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrderAsync("123"), "CancelOrder");
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceMarginOrderAsync("123", OrderSide.Buy, OrderType.Market), "PlaceMarginOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderAsync("123"), "GetOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderByClientOrderIdAsync("123", OrderQueryState.Open), "GetOrderByClientOrderId", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOpenOrdersAsync(), "GetOpenOrders", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetClosedOrdersAsync(), "GetClosedOrders", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetUserTradesAsync(), "GetUserTrades", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderTradesAsync("123"), "GetOrderTrades", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrdersAsync("123"), "CancelOrders", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelAllOrderAsync(), "CancelAllOrder");
        }

        [Test]
        public async Task ValidateUsdFuturesAccountCalls()
        {
            var client = new BitMartRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new BitMartApiCredentials("123", "456", "XXX");
            });
            var tester = new RestRequestValidator<BitMartRestClient>(client, "Endpoints/UsdFutures/Account", "https://api-cloud-v2.bitmart.com", IsAuthenticated, stjCompare: true);
            //await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetBalancesAsync(), "GetBalances", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.GetTransferHistoryAsync(), "GetTransferHistory", nestedJsonProperty: "data.records");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.TransferAsync("123", 0.1m, FuturesTransferType.ContractToSpot), "Transfer", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Account.SetLeverageAsync("123", 0.1m, MarginType.CrossMargin), "SetLeverage", nestedJsonProperty: "data");
        }

        [Test]
        public async Task ValidateUsdFuturesExchageDataCalls()
        {
            var client = new BitMartRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new BitMartApiCredentials("123", "456", "XXX");
            });
            var tester = new RestRequestValidator<BitMartRestClient>(client, "Endpoints/UsdFutures/ExchangeData", "https://api-cloud-v2.bitmart.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetContractsAsync(), "GetContracts", nestedJsonProperty: "data.symbols");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetOrderBookAsync("123"), "GetOrderBook", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetOpenInterestAsync("123"), "GetOpenInterest", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetCurrentFundingRateAsync("123"), "GetCurrentFundingRate", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.ExchangeData.GetKlinesAsync("123", FuturesKlineInterval.OneDay, DateTime.UtcNow, DateTime.UtcNow), "GetKlines", nestedJsonProperty: "data");
        }


        [Test]
        public async Task ValidateUsdFuturesSubAccountCalls()
        {
            var client = new BitMartRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new BitMartApiCredentials("123", "456", "XXX");
            });
            var tester = new RestRequestValidator<BitMartRestClient>(client, "Endpoints/UsdFutures/SubAccount", "https://api-cloud-v2.bitmart.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.UsdFuturesApi.SubAccount.TransferSubToMainForMainAsync("123", 0.1m, "123", "123"), "TransferSubToMainForMain");
            await tester.ValidateAsync(client => client.UsdFuturesApi.SubAccount.TransferMainToSubForMainAsync("123", 0.1m, "123", "123"), "TransferMainToSubForMain");
            await tester.ValidateAsync(client => client.UsdFuturesApi.SubAccount.TransferSubToMainForSubAsync("123", 0.1m, "123"), "TransferSubToMainForSub");
            await tester.ValidateAsync(client => client.UsdFuturesApi.SubAccount.GetSubAcccountBalanceAsync("123"), "GetSubAcccountBalance", nestedJsonProperty: "data.wallet");
            await tester.ValidateAsync(client => client.UsdFuturesApi.SubAccount.GetSubAccountTransferHistoryForMainAsync("123", 123), "GetSubAccountTransferHistoryForMain", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.SubAccount.GetSubAccountTransferHistoryAsync(123), "GetSubAccountTransferHistory", nestedJsonProperty: "data");
        }

        [Test]
        public async Task ValidateUsdFuturesTradingCalls()
        {
            var client = new BitMartRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new BitMartApiCredentials("123", "456", "XXX");
            });
            var tester = new RestRequestValidator<BitMartRestClient>(client, "Endpoints/UsdFutures/Trading", "https://api-cloud-v2.bitmart.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetOrderAsync("123", "123"), "GetOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetClosedOrdersAsync("123"), "GetClosedOrders", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetOpenOrdersAsync(), "GetOpenOrders", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetTriggerOrdersAsync(), "GetTriggerOrders", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetPositionsAsync(), "GetPositions", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetPositionRiskAsync(), "GetPositionRisk", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.GetUserTradesAsync("123"), "GetUserTrades", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.PlaceOrderAsync("123", FuturesSide.SellCloseLong, FuturesOrderType.Market, 1), "PlaceOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.CancelOrderAsync("123", "123"), "CancelOrder");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.CancelOrdersAsync("123"), "CancelOrders");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.PlaceTriggerOrderAsync("123", OrderType.Market, FuturesSide.BuyCloseShort, 1, 0.1m, MarginType.CrossMargin, 0.1m, PriceDirection.LongDirection, TriggerPriceType.FairPrice), "PlaceTriggerOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.CancelTriggerOrderAsync("123", "123"), "CancelTriggerOrder");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.PlaceTpSlOrderAsync("123", TplSlOrderType.StopLoss, FuturesSide.SellCloseLong, 0.1m, TriggerPriceType.FairPrice, PlanCategory.PositionTpSl), "PlaceTpSlOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.EditTpSlOrderAsync("123", 0.1m, TriggerPriceType.FairPrice, PlanCategory.PositionTpSl, OrderType.Market), "EditTpSlOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.EditTriggerOrderAsync("123", 0.1m, TriggerPriceType.FairPrice, OrderType.Market), "EditPlanOrder", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.UsdFuturesApi.Trading.EditPresetTriggerOrderAsync("123", "123", TriggerPriceType.FairPrice, TriggerPriceType.FairPrice, 0.1m, 0.1m), "EditPresetPlanOrder", nestedJsonProperty: "data");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestHeaders.Any(x => x.Key == "X-BM-SIGN");
        }
    }
}
