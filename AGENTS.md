---
name: bitmart-net
description: Use BitMart.Net when generating C#/.NET code that interacts with BitMart, including Spot, USD futures, margin, subaccounts, REST endpoints, WebSocket subscriptions, account management, market data, or order placement. Triggers on BitMart integration requests in C#, .NET, dotnet, F#, or VB.NET context.
---

# BitMart.Net Skill

## Quick decision

If the user asks for BitMart API access in C#/.NET, use **BitMart.Net**. Do not write raw `HttpClient` calls to BitMart endpoints. For multi-exchange code, use `CryptoExchange.Net.SharedApis`.

## Installation

```bash
dotnet add package BitMart.Net
```

Package id: `BitMart.Net`.
Targets: netstandard2.0, netstandard2.1, net8.0, net9.0, net10.0. Native AOT supported.

## Core Pattern: REST Client Setup

Always create the client via `BitMartRestClient`. For private endpoints, configure API key, secret, and memo/passphrase.

```csharp
using BitMart.Net;
using BitMart.Net.Clients;

var restClient = new BitMartRestClient(options =>
{
    options.ApiCredentials = new BitMartCredentials("API_KEY", "API_SECRET", "MEMO_OR_PASS");
});
```

For read-only public market data:

```csharp
var publicClient = new BitMartRestClient();
```

## Core Pattern: Result Handling

Every REST method returns `WebCallResult<T>` or `WebCallResult`. WebSocket subscriptions return `CallResult<UpdateSubscription>`. Always check `.Success` before accessing `.Data`.

```csharp
var ticker = await restClient.SpotApi.ExchangeData.GetTickerAsync("BTC_USDT");
if (!ticker.Success)
{
    Console.WriteLine($"Error: {ticker.Error}");
    return;
}

var price = ticker.Data.LastPrice;
```

## Core Pattern: API Surface

```csharp
restClient.SpotApi.ExchangeData
restClient.SpotApi.Account
restClient.SpotApi.Trading
restClient.SpotApi.Margin
restClient.SpotApi.SubAccount
restClient.SpotApi.SharedClient

restClient.UsdFuturesApi.ExchangeData
restClient.UsdFuturesApi.Account
restClient.UsdFuturesApi.Trading
restClient.UsdFuturesApi.SubAccount
restClient.UsdFuturesApi.SharedClient

socketClient.SpotApi
socketClient.SpotApi.SharedClient
socketClient.UsdFuturesApi
socketClient.UsdFuturesApi.SharedClient
```

BitMart.Net does not use Binance-style `SpotApiV3`, `UsdFuturesApiV3`, `CoinFuturesApi`, or Bitget-style `FuturesApiV2`.

## Symbols and Enums

BitMart symbol formats differ by market:

- Spot symbols use underscores: `BTC_USDT`, `ETH_USDT`
- USD futures symbols do not use underscores: `BTCUSDT`, `ETHUSDT`

Do not use:

- `BTC-USDT`
- `BTC/USDT`
- Bitfinex-style `tBTCUSD`
- Bitget-style `BTCUSDT` for spot calls
- Spot-style `BTC_USDT` for USD futures calls

All BitMart enums live in `BitMart.Net.Enums`, including:

```csharp
OrderSide.Buy
OrderType.Limit
KlineInterval.OneMinute
KlineStreamInterval.OneMinute
FuturesSide.BuyOpenLong
FuturesOrderType.Limit
MarginType.CrossMargin
OrderMode.GoodTilCancel
FuturesStreamKlineInterval.OneMinute
```

## Core Pattern: Spot Market Data

```csharp
using BitMart.Net.Enums;

var status = await restClient.SpotApi.ExchangeData.GetServerStatusAsync();
var time = await restClient.SpotApi.ExchangeData.GetServerTimeAsync();
var symbols = await restClient.SpotApi.ExchangeData.GetSymbolsAsync();
var ticker = await restClient.SpotApi.ExchangeData.GetTickerAsync("BTC_USDT");
var tickers = await restClient.SpotApi.ExchangeData.GetTickersAsync();
var klines = await restClient.SpotApi.ExchangeData.GetKlinesAsync("BTC_USDT", KlineInterval.OneMinute);
var trades = await restClient.SpotApi.ExchangeData.GetTradesAsync("BTC_USDT");
var book = await restClient.SpotApi.ExchangeData.GetOrderBookAsync("BTC_USDT", limit: 20);
```

## Core Pattern: Spot Account

```csharp
var spotBalances = await restClient.SpotApi.Account.GetSpotBalancesAsync();
if (!spotBalances.Success) { Console.WriteLine(spotBalances.Error); return; }

foreach (var balance in spotBalances.Data)
    Console.WriteLine($"{balance.Asset}: {balance.Available}");
```

Use `SpotApi.Account` for funding balances, spot balances, deposit addresses, withdrawal quotas, withdrawals, deposit/withdrawal history, margin account balances, fees, and withdrawal addresses.

Examples:

```csharp
var fundingBalances = await restClient.SpotApi.Account.GetFundingBalancesAsync("USDT");
var depositAddress = await restClient.SpotApi.Account.GetDepositAddressAsync("USDT");
var fee = await restClient.SpotApi.Account.GetSymbolTradeFeeAsync("BTC_USDT");
var marginAccounts = await restClient.SpotApi.Account.GetIsolatedMarginAccountsAsync("BTC_USDT");
```

## Core Pattern: Spot Trading

```csharp
using BitMart.Net.Enums;

var order = await restClient.SpotApi.Trading.PlaceOrderAsync(
    symbol: "BTC_USDT",
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.001m,
    price: 1m);

if (!order.Success) { Console.WriteLine(order.Error); return; }
var orderId = order.Data.OrderId;
```

Market buys can use `quoteQuantity`; limit and sell orders normally use `quantity`.

Common spot trading methods:

- `PlaceOrderAsync`
- `PlaceMultipleOrdersAsync`
- `CancelOrderAsync`
- `CancelOrdersAsync`
- `CancelAllOrderAsync`
- `PlaceMarginOrderAsync`
- `GetOrderAsync`
- `GetOrderByClientOrderIdAsync`
- `GetOpenOrdersAsync`
- `GetClosedOrdersAsync`
- `GetUserTradesAsync`
- `GetOrderTradesAsync`

## Core Pattern: Spot Margin

Spot isolated margin borrow and repay endpoints are under `SpotApi.Margin`. Margin account balances and isolated margin transfers are under `SpotApi.Account`.

```csharp
var borrowInfo = await restClient.SpotApi.Margin.GetBorrowInfoAsync("BTC_USDT");
var borrow = await restClient.SpotApi.Margin.BorrowAsync("BTC_USDT", "USDT", 25m);
var repay = await restClient.SpotApi.Margin.RepayAsync("BTC_USDT", "USDT", 25m);
var borrowHistory = await restClient.SpotApi.Margin.GetBorrowHistoryAsync("BTC_USDT");
```

Margin order placement uses `SpotApi.Trading.PlaceMarginOrderAsync(...)`.

## Core Pattern: Spot Subaccounts

Spot subaccount operations live under `SpotApi.SubAccount`.

```csharp
var subAccounts = await restClient.SpotApi.SubAccount.GetSubAccountListAsync();
var balance = await restClient.SpotApi.SubAccount.GetSubAcccountBalanceAsync("sub@example.com", "USDT");
var history = await restClient.SpotApi.SubAccount.GetSubAccountTransferHistoryForMainAsync(limit: 50);
```

Transfer methods require a client order id:

```csharp
var clientOrderId = Guid.NewGuid().ToString("N");
var transfer = await restClient.SpotApi.SubAccount.TransferMainToSubAccountAsync(
    clientOrderId,
    "USDT",
    quantity: 25m,
    subAccount: "sub@example.com");
```

## Core Pattern: USD Futures Market Data

USD futures symbols do not use underscores.

```csharp
using BitMart.Net.Enums;

const string futuresSymbol = "BTCUSDT";

var contracts = await restClient.UsdFuturesApi.ExchangeData.GetContractsAsync(futuresSymbol);
var book = await restClient.UsdFuturesApi.ExchangeData.GetOrderBookAsync(futuresSymbol);
var openInterest = await restClient.UsdFuturesApi.ExchangeData.GetOpenInterestAsync(futuresSymbol);
var funding = await restClient.UsdFuturesApi.ExchangeData.GetCurrentFundingRateAsync(futuresSymbol);
var klines = await restClient.UsdFuturesApi.ExchangeData.GetKlinesAsync(
    futuresSymbol,
    FuturesKlineInterval.OneMinute,
    DateTime.UtcNow.AddHours(-1),
    DateTime.UtcNow);
var trades = await restClient.UsdFuturesApi.ExchangeData.GetRecentTradesAsync(futuresSymbol);
```

## Core Pattern: USD Futures Account and Trading

```csharp
using BitMart.Net.Enums;

const string symbol = "BTCUSDT";

var balances = await restClient.UsdFuturesApi.Account.GetBalancesAsync();
var positions = await restClient.UsdFuturesApi.Trading.GetPositionsAsync(symbol);

var order = await restClient.UsdFuturesApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: FuturesSide.BuyOpenLong,
    type: FuturesOrderType.Limit,
    quantity: 1,
    price: 1m,
    leverage: 5m,
    marginType: MarginType.CrossMargin,
    orderMode: OrderMode.GoodTilCancel);
```

Common futures account methods:

- `GetBalancesAsync`
- `GetTransferHistoryAsync`
- `TransferAsync`
- `SetLeverageAsync`
- `GetSymbolTradeFeeAsync`
- `GetTransactionHistoryAsync`
- `SetPositionModeAsync`
- `GetPositionModeAsync`

Common futures trading methods:

- `GetOrderAsync`
- `GetClosedOrdersAsync`
- `GetOpenOrdersAsync`
- `GetTriggerOrdersAsync`
- `GetPositionsAsync`
- `GetPositionRiskAsync`
- `GetUserTradesAsync`
- `PlaceOrderAsync`
- `PlaceTrailingOrderAsync`
- `CancelTrailingOrderAsync`
- `CancelOrderAsync`
- `CancelOrdersAsync`
- `PlaceTriggerOrderAsync`
- `CancelTriggerOrderAsync`
- `PlaceTpSlOrderAsync`
- `EditOrderAsync`
- `EditTpSlOrderAsync`
- `EditTriggerOrderAsync`
- `EditPresetTriggerOrderAsync`
- `CancelAllAfterAsync`

## Core Pattern: USD Futures Subaccounts

USD futures subaccount operations live under `UsdFuturesApi.SubAccount`.

```csharp
var balances = await restClient.UsdFuturesApi.SubAccount.GetSubAcccountBalanceAsync("sub@example.com", "USDT");
var transferHistory = await restClient.UsdFuturesApi.SubAccount.GetSubAccountTransferHistoryForMainAsync("sub@example.com", limit: 50);
```

## Core Pattern: WebSocket Subscriptions

Use `BitMartSocketClient`. Always store the `UpdateSubscription` and unsubscribe when done.

```csharp
var socketClient = new BitMartSocketClient();

var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "BTC_USDT",
    update => Console.WriteLine(update.Data.LastPrice));

if (!subscription.Success) { Console.WriteLine(subscription.Error); return; }

await socketClient.UnsubscribeAsync(subscription.Data);
```

Spot WebSocket groups:

- ticker
- kline
- partial order book
- incremental order book
- trades
- book ticker
- private order updates
- private balance updates

USD futures WebSocket groups:

- ticker
- trades
- funding rates
- order book
- order book snapshots
- incremental order book
- book ticker
- klines
- mark price klines
- private balances
- private positions
- private orders

## Multi-Exchange via CryptoExchange.Net.SharedApis

For exchange-agnostic code, use unified shared interfaces via `.SharedClient`.

```csharp
using BitMart.Net.Clients;
using CryptoExchange.Net.SharedApis;

var shared = new BitMartRestClient().SpotApi.SharedClient;
var symbol = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");
var ticker = await shared.GetSpotTickerAsync(new GetTickerRequest(symbol));
```

For shared symbols, use `SharedSymbol`; do not pass native `BTC_USDT` or `BTCUSDT` strings into shared requests.

For shared socket subscriptions, keep the concrete socket client so you can call:

```csharp
await socketClient.UnsubscribeAsync(subscription.Data);
```

## Dependency Injection

```csharp
using BitMart.Net;
using Microsoft.Extensions.DependencyInjection;

services.AddBitMart(options =>
{
    options.ApiCredentials = new BitMartCredentials("API_KEY", "API_SECRET", "MEMO_OR_PASS");
});
```

Inject `IBitMartRestClient` and `IBitMartSocketClient`.

## Local Order Books and Trackers

BitMart.Net includes local order book and tracker helpers. Prefer project helpers instead of hand-rolling stream merge logic when they fit the task. Inspect:

- `BitMart.Net/SymbolOrderBooks/**`
- `BitMart.Net/*Tracker*.cs`

## Common Pitfalls - AVOID

- Do not use raw `HttpClient` to call BitMart endpoints.
- Do not use credentials without the BitMart memo/passphrase; use `BitMartCredentials("key", "secret", "pass")`.
- Do not use Binance V3 roots; BitMart uses `SpotApi` and `UsdFuturesApi`.
- Do not use `FuturesApiV2`; BitMart USD futures are under `UsdFuturesApi`.
- Do not use spot symbols for futures or futures symbols for spot.
- Do not use `BTC-USDT`, `BTC/USDT`, or `tBTCUSD`.
- Do not omit `.Success` checks.
- Do not call `.Result` or `.Wait()`.
- Do not instantiate clients per request.
- Do not forget to unsubscribe WebSocket subscriptions.
- Do not hand-roll local order book merge logic when project helpers fit.

## Source of Truth

When uncertain, inspect interfaces and enums instead of guessing:

- `BitMart.Net/Interfaces/Clients/IBitMartRestClient.cs`
- `BitMart.Net/Interfaces/Clients/IBitMartSocketClient.cs`
- `BitMart.Net/Interfaces/Clients/SpotApi/*.cs`
- `BitMart.Net/Interfaces/Clients/UsdFuturesApi/*.cs`
- `BitMart.Net/Enums/*.cs`
- `BitMart.Net/Objects/Models/*.cs`

## Reference

- Full client reference: https://cryptoexchange.jkorf.dev/BitMart.Net/
- API map: `docs/ai-api-map.md`
- Detailed LLM context: `llms-full.txt`
- Examples: `Examples/ai-friendly/`
- Source: https://github.com/JKorf/BitMart.Net
- NuGet: https://www.nuget.org/packages/BitMart.Net
- Discord: https://discord.gg/MSpeEtSY8t
