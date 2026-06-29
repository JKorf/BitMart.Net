# BitMart.Net AI API Quick Map

Use this file to route common user intents to the correct BitMart.Net client member. If a method name or parameter is not listed here, inspect `BitMart.Net/Interfaces/Clients/**` before generating code.

## Client Roots

| Intent | Use |
|---|---|
| REST calls | `new BitMartRestClient()` |
| WebSocket streams | `new BitMartSocketClient()` |
| API key authentication | `options.ApiCredentials = new BitMartCredentials("key", "secret", "memoOrPass")` |
| Live environment | `BitMartEnvironment.Live` |
| Dependency injection | `services.AddBitMart(options => { ... })` |
| Spot REST | `client.SpotApi` |
| USD futures REST | `client.UsdFuturesApi` |
| Spot WebSocket | `socketClient.SpotApi` |
| USD futures WebSocket | `socketClient.UsdFuturesApi` |
| Shared spot REST | `client.SpotApi.SharedClient` |
| Shared USD futures REST | `client.UsdFuturesApi.SharedClient` |
| Shared spot socket | `socketClient.SpotApi.SharedClient` |
| Shared USD futures socket | `socketClient.UsdFuturesApi.SharedClient` |
| Discover shared capabilities | `client.SpotApi.SharedClient.Discover()` / `client.UsdFuturesApi.SharedClient.Discover()` |

## Symbols

| Market | Format |
|---|---|
| Spot | `BTC_USDT`, `ETH_USDT` |
| USD futures | `BTCUSDT`, `ETHUSDT` |
| Wrong for BitMart spot | `BTCUSDT`, `BTC-USDT`, `BTC/USDT`, `tBTCUSD` |
| Wrong for BitMart USD futures | `BTC_USDT`, `BTC-USDT`, `BTC/USDT` |

## Spot Exchange Data REST

| User intent | BitMart.Net member |
|---|---|
| Get server status | `client.SpotApi.ExchangeData.GetServerStatusAsync()` |
| Get server time | `client.SpotApi.ExchangeData.GetServerTimeAsync()` |
| Get assets | `client.SpotApi.ExchangeData.GetAssetsAsync()` |
| Get symbols | `client.SpotApi.ExchangeData.GetSymbolsAsync()` |
| Get symbol names | `client.SpotApi.ExchangeData.GetSymbolNamesAsync()` |
| Get ticker | `client.SpotApi.ExchangeData.GetTickerAsync("BTC_USDT")` |
| Get all tickers | `client.SpotApi.ExchangeData.GetTickersAsync()` |
| Get asset deposit/withdraw info | `client.SpotApi.ExchangeData.GetAssetDepositWithdrawInfoAsync("USDT")` |
| Get klines | `client.SpotApi.ExchangeData.GetKlinesAsync("BTC_USDT", KlineInterval.OneMinute)` |
| Get kline history | `client.SpotApi.ExchangeData.GetKlineHistoryAsync("BTC_USDT", KlineInterval.OneMinute)` |
| Get recent trades | `client.SpotApi.ExchangeData.GetTradesAsync("BTC_USDT")` |
| Get order book | `client.SpotApi.ExchangeData.GetOrderBookAsync("BTC_USDT", limit: 20)` |

## Spot Account REST

| User intent | BitMart.Net member |
|---|---|
| Get funding balances | `client.SpotApi.Account.GetFundingBalancesAsync("USDT")` |
| Get spot balances | `client.SpotApi.Account.GetSpotBalancesAsync()` |
| Get deposit address | `client.SpotApi.Account.GetDepositAddressAsync("USDT")` |
| Get withdrawal quota | `client.SpotApi.Account.GetWithdrawalQuotaAsync("USDT")` |
| Withdraw | `client.SpotApi.Account.WithdrawAsync("USDT", quantity, targetAddress: address)` |
| Get deposit history | `client.SpotApi.Account.GetDepositHistoryAsync("USDT")` |
| Get withdrawal history | `client.SpotApi.Account.GetWithdrawalHistoryAsync("USDT")` |
| Get deposit/withdraw details | `client.SpotApi.Account.GetDepositWithdrawalAsync(id)` |
| Get isolated margin accounts | `client.SpotApi.Account.GetIsolatedMarginAccountsAsync("BTC_USDT")` |
| Isolated margin transfer | `client.SpotApi.Account.IsolatedMarginTransferAsync("BTC_USDT", "USDT", quantity, direction)` |
| Get base trade fees | `client.SpotApi.Account.GetBaseTradeFeesAsync()` |
| Get symbol trade fee | `client.SpotApi.Account.GetSymbolTradeFeeAsync("BTC_USDT")` |
| Get withdrawal addresses | `client.SpotApi.Account.GetWithdrawalAddressesAsync()` |

## Spot Trading REST

| User intent | BitMart.Net member |
|---|---|
| Place spot order | `client.SpotApi.Trading.PlaceOrderAsync("BTC_USDT", OrderSide.Buy, OrderType.Limit, quantity, price)` |
| Place market buy with quote quantity | `client.SpotApi.Trading.PlaceOrderAsync("BTC_USDT", OrderSide.Buy, OrderType.Market, quoteQuantity: quoteQuantity)` |
| Place multiple orders | `client.SpotApi.Trading.PlaceMultipleOrdersAsync("BTC_USDT", orders)` |
| Cancel spot order | `client.SpotApi.Trading.CancelOrderAsync("BTC_USDT", orderId: orderId)` |
| Cancel multiple spot orders | `client.SpotApi.Trading.CancelOrdersAsync("BTC_USDT", orderIds: ids)` |
| Cancel all spot orders | `client.SpotApi.Trading.CancelAllOrderAsync("BTC_USDT")` |
| Place margin order | `client.SpotApi.Trading.PlaceMarginOrderAsync("BTC_USDT", OrderSide.Buy, OrderType.Limit, quantity, price)` |
| Get order | `client.SpotApi.Trading.GetOrderAsync(orderId)` |
| Get order by client order id | `client.SpotApi.Trading.GetOrderByClientOrderIdAsync(clientOrderId)` |
| Get open orders | `client.SpotApi.Trading.GetOpenOrdersAsync("BTC_USDT")` |
| Get closed orders | `client.SpotApi.Trading.GetClosedOrdersAsync("BTC_USDT")` |
| Get user trades | `client.SpotApi.Trading.GetUserTradesAsync("BTC_USDT")` |
| Get order trades | `client.SpotApi.Trading.GetOrderTradesAsync(orderId)` |

## Spot Margin REST

| User intent | BitMart.Net member |
|---|---|
| Borrow | `client.SpotApi.Margin.BorrowAsync("BTC_USDT", "USDT", quantity)` |
| Repay | `client.SpotApi.Margin.RepayAsync("BTC_USDT", "USDT", quantity)` |
| Get borrow history | `client.SpotApi.Margin.GetBorrowHistoryAsync("BTC_USDT")` |
| Get repay history | `client.SpotApi.Margin.GetRepayHistoryAsync("BTC_USDT", asset: "USDT")` |
| Get borrow info | `client.SpotApi.Margin.GetBorrowInfoAsync("BTC_USDT")` |

## Spot Subaccount REST

| User intent | BitMart.Net member |
|---|---|
| Transfer sub to main as main | `client.SpotApi.SubAccount.TransferSubToMainForMainAsync(clientOrderId, "USDT", quantity, subAccount)` |
| Transfer sub to main as sub | `client.SpotApi.SubAccount.TransferSubToMainForSubAsync(clientOrderId, "USDT", quantity)` |
| Transfer main to sub | `client.SpotApi.SubAccount.TransferMainToSubAccountAsync(clientOrderId, "USDT", quantity, subAccount)` |
| Transfer sub to sub | `client.SpotApi.SubAccount.TransferSubAccountToSubAccountAsync(clientOrderId, quantity, "USDT", fromAccount, toAccount)` |
| Main transfer history | `client.SpotApi.SubAccount.GetSubAccountTransferHistoryForMainAsync(limit)` |
| Sub transfer history | `client.SpotApi.SubAccount.GetSubAccountTransferHistoryAsync(limit)` |
| Get subaccount balance | `client.SpotApi.SubAccount.GetSubAcccountBalanceAsync(subAccount, "USDT")` |
| Get subaccount list | `client.SpotApi.SubAccount.GetSubAccountListAsync()` |

## USD Futures Exchange Data REST

| User intent | BitMart.Net member |
|---|---|
| Get contracts | `client.UsdFuturesApi.ExchangeData.GetContractsAsync("BTCUSDT")` |
| Get order book | `client.UsdFuturesApi.ExchangeData.GetOrderBookAsync("BTCUSDT")` |
| Get open interest | `client.UsdFuturesApi.ExchangeData.GetOpenInterestAsync("BTCUSDT")` |
| Get current funding rate | `client.UsdFuturesApi.ExchangeData.GetCurrentFundingRateAsync("BTCUSDT")` |
| Get futures klines | `client.UsdFuturesApi.ExchangeData.GetKlinesAsync("BTCUSDT", FuturesKlineInterval.OneMinute, startTime, endTime)` |
| Get futures mark klines | `client.UsdFuturesApi.ExchangeData.GetMarkKlinesAsync("BTCUSDT", FuturesKlineInterval.OneMinute, startTime, endTime)` |
| Get funding rate history | `client.UsdFuturesApi.ExchangeData.GetFundingRateHistoryAsync("BTCUSDT")` |
| Get leverage brackets | `client.UsdFuturesApi.ExchangeData.GetLeverageBracketsAsync("BTCUSDT")` |
| Get recent trades | `client.UsdFuturesApi.ExchangeData.GetRecentTradesAsync("BTCUSDT")` |

## USD Futures Account REST

| User intent | BitMart.Net member |
|---|---|
| Get balances | `client.UsdFuturesApi.Account.GetBalancesAsync()` |
| Get transfer history | `client.UsdFuturesApi.Account.GetTransferHistoryAsync("USDT")` |
| Transfer spot/futures | `client.UsdFuturesApi.Account.TransferAsync("USDT", quantity, FuturesTransferType.SpotToContract)` |
| Set leverage | `client.UsdFuturesApi.Account.SetLeverageAsync("BTCUSDT", leverage, MarginType.CrossMargin)` |
| Get symbol trade fee | `client.UsdFuturesApi.Account.GetSymbolTradeFeeAsync("BTCUSDT")` |
| Get transaction history | `client.UsdFuturesApi.Account.GetTransactionHistoryAsync("BTCUSDT")` |
| Set position mode | `client.UsdFuturesApi.Account.SetPositionModeAsync(PositionMode.OneWayMode)` |
| Get position mode | `client.UsdFuturesApi.Account.GetPositionModeAsync()` |

## USD Futures Trading REST

| User intent | BitMart.Net member |
|---|---|
| Get order | `client.UsdFuturesApi.Trading.GetOrderAsync("BTCUSDT", orderId)` |
| Get closed orders | `client.UsdFuturesApi.Trading.GetClosedOrdersAsync("BTCUSDT")` |
| Get open orders | `client.UsdFuturesApi.Trading.GetOpenOrdersAsync("BTCUSDT")` |
| Get trigger orders | `client.UsdFuturesApi.Trading.GetTriggerOrdersAsync("BTCUSDT")` |
| Get positions | `client.UsdFuturesApi.Trading.GetPositionsAsync("BTCUSDT")` |
| Get position risk | `client.UsdFuturesApi.Trading.GetPositionRiskAsync("BTCUSDT")` |
| Get user trades | `client.UsdFuturesApi.Trading.GetUserTradesAsync("BTCUSDT")` |
| Place futures order | `client.UsdFuturesApi.Trading.PlaceOrderAsync("BTCUSDT", FuturesSide.BuyOpenLong, FuturesOrderType.Limit, quantity, price, leverage, marginType, orderMode)` |
| Place trailing order | `client.UsdFuturesApi.Trading.PlaceTrailingOrderAsync(...)` |
| Cancel trailing order | `client.UsdFuturesApi.Trading.CancelTrailingOrderAsync("BTCUSDT", orderId)` |
| Cancel order | `client.UsdFuturesApi.Trading.CancelOrderAsync("BTCUSDT", orderId: orderId)` |
| Cancel all futures orders on symbol | `client.UsdFuturesApi.Trading.CancelOrdersAsync("BTCUSDT")` |
| Place trigger order | `client.UsdFuturesApi.Trading.PlaceTriggerOrderAsync(...)` |
| Cancel trigger order | `client.UsdFuturesApi.Trading.CancelTriggerOrderAsync("BTCUSDT", orderId)` |
| Place TP/SL order | `client.UsdFuturesApi.Trading.PlaceTpSlOrderAsync(...)` |
| Edit order | `client.UsdFuturesApi.Trading.EditOrderAsync("BTCUSDT", orderId, price, quantity)` |
| Edit TP/SL order | `client.UsdFuturesApi.Trading.EditTpSlOrderAsync(...)` |
| Edit trigger order | `client.UsdFuturesApi.Trading.EditTriggerOrderAsync(...)` |
| Edit preset trigger order | `client.UsdFuturesApi.Trading.EditPresetTriggerOrderAsync(...)` |
| Cancel all after | `client.UsdFuturesApi.Trading.CancelAllAfterAsync("BTCUSDT", TimeSpan.FromSeconds(30))` |

## USD Futures Subaccount REST

| User intent | BitMart.Net member |
|---|---|
| Transfer sub to main as main | `client.UsdFuturesApi.SubAccount.TransferSubToMainForMainAsync("USDT", quantity, subAccount, clientOrderId)` |
| Transfer main to sub as main | `client.UsdFuturesApi.SubAccount.TransferMainToSubForMainAsync("USDT", quantity, subAccount, clientOrderId)` |
| Transfer sub to main as sub | `client.UsdFuturesApi.SubAccount.TransferSubToMainForSubAsync("USDT", quantity, clientOrderId)` |
| Get subaccount balance | `client.UsdFuturesApi.SubAccount.GetSubAcccountBalanceAsync(subAccount, "USDT")` |
| Main transfer history | `client.UsdFuturesApi.SubAccount.GetSubAccountTransferHistoryForMainAsync(subAccount, limit)` |
| Sub transfer history | `client.UsdFuturesApi.SubAccount.GetSubAccountTransferHistoryAsync(limit)` |

## WebSocket

| User intent | BitMart.Net member |
|---|---|
| Spot ticker updates | `socketClient.SpotApi.SubscribeToTickerUpdatesAsync("BTC_USDT", handler)` |
| Spot kline updates | `socketClient.SpotApi.SubscribeToKlineUpdatesAsync("BTC_USDT", KlineStreamInterval.OneMinute, handler)` |
| Spot partial order book | `socketClient.SpotApi.SubscribeToPartialOrderBookUpdatesAsync("BTC_USDT", 20, handler)` |
| Spot incremental order book | `socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync("BTC_USDT", handler)` |
| Spot trades | `socketClient.SpotApi.SubscribeToTradeUpdatesAsync("BTC_USDT", handler)` |
| Spot book ticker | `socketClient.SpotApi.SubscribeToBookTickerUpdatesAsync("BTC_USDT", handler)` |
| Spot private orders | `socketClient.SpotApi.SubscribeToOrderUpdatesAsync(handler)` |
| Spot private balances | `socketClient.SpotApi.SubscribeToBalanceUpdatesAsync(handler)` |
| Futures ticker updates | `socketClient.UsdFuturesApi.SubscribeToTickerUpdatesAsync("BTCUSDT", handler)` |
| Futures all ticker updates | `socketClient.UsdFuturesApi.SubscribeToTickerUpdatesAsync(handler)` |
| Futures trades | `socketClient.UsdFuturesApi.SubscribeToTradeUpdatesAsync("BTCUSDT", handler)` |
| Futures funding rates | `socketClient.UsdFuturesApi.SubscribeToFundingRateUpdatesAsync("BTCUSDT", handler)` |
| Futures order book | `socketClient.UsdFuturesApi.SubscribeToOrderBookUpdatesAsync("BTCUSDT", 20, handler)` |
| Futures order book snapshot | `socketClient.UsdFuturesApi.SubscribeToOrderBookSnapshotUpdatesAsync("BTCUSDT", 20, handler)` |
| Futures incremental order book | `socketClient.UsdFuturesApi.SubscribeToOrderBookIncrementalUpdatesAsync("BTCUSDT", 20, handler)` |
| Futures book ticker | `socketClient.UsdFuturesApi.SubscribeToBookTickerUpdatesAsync("BTCUSDT", handler)` |
| Futures kline | `socketClient.UsdFuturesApi.SubscribeToKlineUpdatesAsync("BTCUSDT", FuturesStreamKlineInterval.OneMinute, handler)` |
| Futures mark kline | `socketClient.UsdFuturesApi.SubscribeToMarkKlineUpdatesAsync("BTCUSDT", FuturesStreamKlineInterval.OneMinute, handler)` |
| Futures private balances | `socketClient.UsdFuturesApi.SubscribeToBalanceUpdatesAsync(handler)` |
| Futures private positions | `socketClient.UsdFuturesApi.SubscribeToPositionUpdatesAsync(handler)` |
| Futures private orders | `socketClient.UsdFuturesApi.SubscribeToOrderUpdatesAsync(handler)` |

## SharedApis

| User intent | BitMart.Net member or interface |
|---|---|
| Shared spot REST client | `new BitMartRestClient().SpotApi.SharedClient` |
| Shared futures REST client | `new BitMartRestClient().UsdFuturesApi.SharedClient` |
| Shared spot socket client | `new BitMartSocketClient().SpotApi.SharedClient` |
| Shared futures socket client | `new BitMartSocketClient().UsdFuturesApi.SharedClient` |
| Discover shared capabilities | `client.SpotApi.SharedClient.Discover()` / `client.UsdFuturesApi.SharedClient.Discover()` |
| Shared spot ticker REST | `ISpotTickerRestClient.GetSpotTickerAsync(new GetTickerRequest(symbol))` |
| Shared futures ticker REST | `IFuturesTickerRestClient.GetFuturesTickerAsync(new GetTickerRequest(symbol))` |
| Shared ticker socket | `ITickerSocketClient.SubscribeToTickerUpdatesAsync(...)` |
| Shared order book socket | `IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(...)` |

Shared REST calls return `HttpResult<T>` / `HttpResult`. Shared socket subscriptions return `WebSocketResult<UpdateSubscription>`. Shared non-I/O symbol/cache helpers such as symbol support checks return `ExchangeCallResult<T>`.

For shared socket subscriptions, keep the concrete socket client and unsubscribe with `await socketClient.UnsubscribeAsync(subscription.Data)`.

## Result Handling

| Situation | Pattern |
|---|---|
| REST success check | `if (!result.Success) { Console.WriteLine(result.Error); return; }` |
| Socket subscription success check | `WebSocketResult<UpdateSubscription> sub = await ...; if (!sub.Success) { Console.WriteLine(sub.Error); return; }` |
| Read REST data | Read `result.Data` only after `result.Success` |
| Shared helper data | Read `ExchangeCallResult<T>.Data` only after `result.Success` |

## Common Routing Pitfalls

| Do not use | Use instead |
|---|---|
| Raw `HttpClient` | `BitMartRestClient` / `BitMartSocketClient` |
| Credentials without passphrase | `BitMartCredentials("key", "secret", "memoOrPass")` |
| `SpotApiV3` | `SpotApi` |
| `FuturesApiV2` | `UsdFuturesApi` |
| `CoinFuturesApi` | Not available in this library |
| Spot symbol `BTCUSDT` | `BTC_USDT` |
| Futures symbol `BTC_USDT` | `BTCUSDT` |
| `BTC-USDT`, `BTC/USDT`, `tBTCUSD` | BitMart-native symbol format |
| `.Data` without `.Success` check | Check `.Success` first |
| Shared socket `UnsubscribeAsync(...)` | Keep the concrete socket client and call `socketClient.UnsubscribeAsync(subscription.Data)` |
