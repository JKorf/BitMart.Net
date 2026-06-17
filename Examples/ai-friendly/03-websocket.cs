// 03-websocket.cs
//
// Demonstrates: BitMart public websocket subscriptions.
//
// Setup: dotnet add package BitMart.Net

using BitMart.Net.Clients;
using BitMart.Net.Enums;

var socketClient = new BitMartSocketClient();

// Subscription methods return WebSocketResult<UpdateSubscription>.
var spotTickerSubscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "BTC_USDT",
    update =>
    {
        Console.WriteLine($"Spot BTC_USDT ticker: {update.Data.LastPrice}");
    });

if (!spotTickerSubscription.Success)
{
    Console.WriteLine($"Spot ticker subscription failed: {spotTickerSubscription.Error}");
    return;
}

var futuresKlineSubscription = await socketClient.UsdFuturesApi.SubscribeToKlineUpdatesAsync(
    "ETHUSDT",
    FuturesStreamKlineInterval.OneMinute,
    update =>
    {
        var candle = update.Data.Klines.FirstOrDefault();
        if (candle != null)
            Console.WriteLine($"Futures ETHUSDT 1m candle: open={candle.OpenPrice}, close={candle.ClosePrice}");
    });

if (!futuresKlineSubscription.Success)
{
    Console.WriteLine($"Futures kline subscription failed: {futuresKlineSubscription.Error}");
    await socketClient.UnsubscribeAsync(spotTickerSubscription.Data);
    return;
}

Console.WriteLine("Listening. Press Enter to unsubscribe.");
Console.ReadLine();

await socketClient.UnsubscribeAsync(spotTickerSubscription.Data);
await socketClient.UnsubscribeAsync(futuresKlineSubscription.Data);
