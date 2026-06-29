# BitMart.Net AI-friendly examples

These examples are compact, copyable console snippets for AI coding assistants and quick onboarding. They are compiled by the unit test suite so API names stay aligned with the project.

## Install

```bash
dotnet add package BitMart.Net
```

## API shape

Use BitMart surfaces:

- Spot REST: `BitMartRestClient().SpotApi`
- USD futures REST: `BitMartRestClient().UsdFuturesApi`
- Spot WebSocket: `BitMartSocketClient().SpotApi`
- USD futures WebSocket: `BitMartSocketClient().UsdFuturesApi`

Do not use Binance/Bitget-style roots such as `SpotApiV3`, `FuturesApiV2`, `UsdFuturesApiV3`, `CoinFuturesApi`, or `PerpetualFuturesApi`.

## Symbols

BitMart symbol format depends on the market:

- Spot: `BTC_USDT`, `ETH_USDT`
- USD futures: `BTCUSDT`, `ETHUSDT`

Do not use `BTC-USDT`, `BTC/USDT`, or Bitfinex-style `tBTCUSD`.

## Result pattern

Most REST calls return `HttpResult<T>` or `HttpResult`. Shared non-I/O symbol/cache helpers return `ExchangeCallResult<T>`. Always check `.Success` before using `.Data`; use `.Error` for exchange, validation, network and rate-limit failures.

```csharp
var result = await client.SpotApi.ExchangeData.GetTickerAsync("BTC_USDT");
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}

Console.WriteLine(result.Data.LastPrice);
```

Socket subscription calls return `WebSocketResult<UpdateSubscription>`. Keep the concrete socket client so you can unsubscribe:

```csharp
var sub = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync("BTC_USDT", update => { });
if (sub.Success)
    await socketClient.UnsubscribeAsync(sub.Data);
```

## Examples

- `01-spot-quickstart.cs` - public market data, balances, open orders and spot order placement.
- `02-usd-futures.cs` - USD futures funding, balances, positions and order flow.
- `03-websocket.cs` - spot and USD futures public WebSocket subscriptions and unsubscribe pattern.
- `04-multi-exchange.cs` - CryptoExchange.Net shared API usage, capability discovery and shared subscriptions.
- `05-error-handling.cs` - `HttpResult`, `WebSocketResult` and `ExchangeCallResult` handling, transient retry shape and order error categorization.

## Common routing

- Spot market data: `client.SpotApi.ExchangeData`
- Spot account: `client.SpotApi.Account`
- Spot orders: `client.SpotApi.Trading`
- Spot margin borrow/repay: `client.SpotApi.Margin`
- Spot subaccounts: `client.SpotApi.SubAccount`
- USD futures market data: `client.UsdFuturesApi.ExchangeData`
- USD futures account: `client.UsdFuturesApi.Account`
- USD futures positions and orders: `client.UsdFuturesApi.Trading`
- USD futures subaccounts: `client.UsdFuturesApi.SubAccount`
- Shared APIs: `client.SpotApi.SharedClient`, `client.UsdFuturesApi.SharedClient`; call `.Discover()` before routing optional shared features

For detailed endpoint routing, see `docs/ai-api-map.md`. For fuller assistant context, see `llms-full.txt`.
