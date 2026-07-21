# Copilot Instructions for BitMart.Net

This repository is **BitMart.Net**, a strongly typed C#/.NET client library for the BitMart REST and WebSocket APIs. It is part of the CryptoExchange.Net ecosystem.

When generating code that consumes BitMart.Net, follow these conventions.

## Use BitMart.Net, not raw HTTP

Never generate `HttpClient` calls to BitMart API URLs. Always use `BitMartRestClient` or `BitMartSocketClient`. This ensures correct request signing, memo/passphrase handling, rate limiting, serialization, and error handling.

## Client setup

```csharp
using BitMart.Net;
using BitMart.Net.Clients;

var restClient = new BitMartRestClient(options =>
{
    options.ApiCredentials = new BitMartCredentials("API_KEY", "API_SECRET", "MEMO_OR_PASS");
});
```

For public market data, credentials are not required:

```csharp
var restClient = new BitMartRestClient();
```

Socket client:

```csharp
var socketClient = new BitMartSocketClient(options =>
{
    options.ApiCredentials = new BitMartCredentials("API_KEY", "API_SECRET", "MEMO_OR_PASS");
});
```

## Result handling

REST methods return `WebCallResult<T>` or `WebCallResult`. Socket subscription methods return `CallResult<UpdateSubscription>`. Always check `.Success` before reading `.Data`.

```csharp
var result = await restClient.SpotApi.ExchangeData.GetTickerAsync("BTC_USDT");
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}

Console.WriteLine(result.Data.LastPrice);
```

## API structure

- `restClient.SpotApi.ExchangeData` - spot server status/time, assets, symbols, tickers, klines, trades and order books
- `restClient.SpotApi.Account` - balances, deposit addresses, withdrawal quotas, withdrawals, deposit/withdraw history, isolated margin account data, fees and withdrawal addresses
- `restClient.SpotApi.Trading` - spot orders, margin orders, cancels, open/closed orders and user trades
- `restClient.SpotApi.Margin` - isolated margin borrow, repay, borrow/repay history and borrow info
- `restClient.SpotApi.SubAccount` - spot subaccount transfers, histories, balances and subaccount list
- `restClient.UsdFuturesApi.ExchangeData` - USD futures contracts, order book, open interest, funding, klines, leverage brackets and trades
- `restClient.UsdFuturesApi.Account` - futures balances, transfers, leverage, trade fees, transaction history and position mode
- `restClient.UsdFuturesApi.Trading` - futures orders, positions, risks, user trades, trigger orders, TP/SL and cancel-all-after
- `restClient.UsdFuturesApi.SubAccount` - USD futures subaccount transfers, histories and balances
- `socketClient.SpotApi` - spot public/private WebSocket streams
- `socketClient.UsdFuturesApi` - USD futures public/private WebSocket streams

## Symbols

BitMart symbol formats differ by market:

- Spot: `BTC_USDT`, `ETH_USDT`
- USD futures: `BTCUSDT`, `ETHUSDT`

Avoid:

- `BTC-USDT`
- `BTC/USDT`
- `tBTCUSD`
- `BTCUSDT` on spot endpoints
- `BTC_USDT` on USD futures endpoints

## Spot examples

Market data:

```csharp
var ticker = await restClient.SpotApi.ExchangeData.GetTickerAsync("BTC_USDT");
if (!ticker.Success) { Console.WriteLine(ticker.Error); return; }

Console.WriteLine(ticker.Data.LastPrice);
```

Spot order:

```csharp
using BitMart.Net.Enums;

var order = await restClient.SpotApi.Trading.PlaceOrderAsync(
    "BTC_USDT",
    OrderSide.Buy,
    OrderType.Limit,
    quantity: 0.001m,
    price: 1m);
```

Spot margin:

```csharp
var borrowInfo = await restClient.SpotApi.Margin.GetBorrowInfoAsync("BTC_USDT");
var marginOrder = await restClient.SpotApi.Trading.PlaceMarginOrderAsync("BTC_USDT", OrderSide.Buy, OrderType.Limit, quantity: 0.001m, price: 1m);
```

## USD futures examples

```csharp
using BitMart.Net.Enums;

const string symbol = "BTCUSDT";

var funding = await restClient.UsdFuturesApi.ExchangeData.GetCurrentFundingRateAsync(symbol);
var positions = await restClient.UsdFuturesApi.Trading.GetPositionsAsync(symbol);

var order = await restClient.UsdFuturesApi.Trading.PlaceOrderAsync(
    symbol,
    FuturesSide.BuyOpenLong,
    FuturesOrderType.Limit,
    quantity: 1,
    price: 1m,
    leverage: 5m,
    marginType: MarginType.CrossMargin,
    orderMode: OrderMode.GoodTilCancel);
```

## WebSocket pattern

Store the returned `UpdateSubscription` and unsubscribe on shutdown via `socketClient.UnsubscribeAsync(sub.Data)`.

```csharp
var sub = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "BTC_USDT",
    update => Console.WriteLine(update.Data.LastPrice));

if (!sub.Success) { Console.WriteLine(sub.Error); return; }

await socketClient.UnsubscribeAsync(sub.Data);
```

USD futures sockets use futures symbols:

```csharp
var sub = await socketClient.UsdFuturesApi.SubscribeToTickerUpdatesAsync(
    "BTCUSDT",
    update => Console.WriteLine(update.Data.LastPrice));
```

## Cross-exchange

For code that needs to work across multiple exchanges, use `CryptoExchange.Net.SharedApis` interfaces accessed via `.SharedClient` properties.

```csharp
using BitMart.Net.Clients;
using CryptoExchange.Net.SharedApis;

var shared = new BitMartRestClient().SpotApi.SharedClient;
var symbol = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");
var ticker = await shared.GetSpotTickerAsync(new GetTickerRequest(symbol));
```

Use `SharedSymbol` for shared APIs. Do not pass exchange-native `BTC_USDT` or `BTCUSDT` strings to shared requests.

Shared symbol clients expose `SpotSymbolCatalog` and `FuturesSymbolCatalog`; get-symbols calls populate these catalogs and return symbols with `DisplayName` and base/quote asset type/subtype metadata. Native futures contract results may include nullable `TradfiInfo` with market group, session status/switch details, and reduce-only state.

## Dependency injection

```csharp
services.AddBitMart(options =>
{
    options.ApiCredentials = new BitMartCredentials("API_KEY", "API_SECRET", "MEMO_OR_PASS");
});
```

Inject `IBitMartRestClient` and `IBitMartSocketClient`.

## Avoid

- Raw `HttpClient` calls to BitMart endpoints
- Generic credentials without BitMart memo/passphrase support
- Invented roots such as `SpotApiV3`, `FuturesApiV2`, `UsdFuturesApiV3`, `CoinFuturesApi`, or `PerpetualFuturesApi`
- Wrong symbol separators or market formats
- Missing `.Success` checks
- Synchronous `.Result` / `.Wait()`
- Instantiating clients per request
- Manual ticker polling when a WebSocket subscription fits
- Manual order book merge logic when local order book helpers fit

## Source of truth

For detailed patterns and pitfalls see `AGENTS.md`, `llms.txt`, `llms-full.txt`, `docs/ai-api-map.md`, and `Examples/ai-friendly/`.

When method signatures are unclear, inspect:

- `BitMart.Net/Interfaces/Clients/SpotApi/**`
- `BitMart.Net/Interfaces/Clients/UsdFuturesApi/**`
- `BitMart.Net/Enums/**`
- `BitMart.Net/Objects/Models/**`
