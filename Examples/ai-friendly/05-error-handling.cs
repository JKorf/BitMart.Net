// 05-error-handling.cs
//
// Demonstrates: HttpResult patterns, retry logic, common error scenarios.
//
// Setup: dotnet add package BitMart.Net

using BitMart.Net;
using BitMart.Net.Clients;
using BitMart.Net.Enums;
using CryptoExchange.Net.Objects;

var client = new BitMartRestClient(options =>
{
    options.ApiCredentials = new BitMartCredentials("API_KEY", "API_SECRET", "MEMO_OR_PASS");
});

// ---- 1. THE BASIC PATTERN ----
// Every REST method returns HttpResult<T> or HttpResult.
// .Success is true/false. .Data is valid only when .Success is true.
// .Error contains structured error info when .Success is false.

var result = await client.SpotApi.ExchangeData.GetTickerAsync("BTC_USDT");

if (result.Success)
{
    Console.WriteLine($"Price: {result.Data.LastPrice}");
}
else
{
    Console.WriteLine($"Code:      {result.Error?.Code}");
    Console.WriteLine($"Message:   {result.Error?.Message}");
    Console.WriteLine($"Type:      {result.Error?.ErrorType}");
    Console.WriteLine($"Transient: {result.Error?.IsTransient}");
}

// ---- 2. SIMPLE RETRY WITH BACKOFF ----
// Retry only on transient errors such as rate limits, network issues, or server overload.
// Do not retry validation errors or insufficient balance errors.

async Task<HttpResult<T>> WithRetry<T>(
    Func<Task<HttpResult<T>>> call,
    int maxAttempts = 3)
{
    HttpResult<T> last = default!;
    for (var attempt = 1; attempt <= maxAttempts; attempt++)
    {
        last = await call();
        if (last.Success) return last;
        if (last.Error?.IsTransient != true) return last;

        await Task.Delay(TimeSpan.FromMilliseconds(250 * Math.Pow(2, attempt)));
    }
    return last;
}

var ticker = await WithRetry(
    () => client.SpotApi.ExchangeData.GetTickerAsync("BTC_USDT"));

if (!ticker.Success)
{
    Console.WriteLine($"Ticker still failed after retry: {ticker.Error}");
}

// ---- 3. ORDER PLACEMENT WITH RESULT CATEGORIZATION ----
var order = await client.SpotApi.Trading.PlaceOrderAsync(
    symbol: "BTC_USDT",
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.001m,
    price: 1m);

if (!order.Success)
{
    var category = order.Error?.IsTransient == true
        ? "Transient - retry may be appropriate"
        : "Permanent - surface to user";

    Console.WriteLine($"{category}: {order.Error?.Code} {order.Error?.Message}");
}

// ---- 4. EXCEPTIONS VS ERROR RESULTS ----
// BitMart.Net returns API/network/rate-limit errors via HttpResult.Error.
// Exceptions are for misconfiguration, disposal, cancellation, or programmer errors.

// Common variations:
//   With CancellationToken:       pass `ct: cancellationToken` to any method
//   With timeout per request:     options.RequestTimeout = TimeSpan.FromSeconds(10)
//   Spot margin endpoints:       client.SpotApi.Margin.*
//   USD futures endpoints:       client.UsdFuturesApi.*
