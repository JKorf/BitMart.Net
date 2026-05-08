// 04-multi-exchange.cs
//
// Demonstrates: writing exchange-agnostic code using CryptoExchange.Net.SharedApis.
// Same code works against BitMart and other exchanges from the CryptoExchange.Net family.
//
// Setup:
//   dotnet add package BitMart.Net
//   dotnet add package Binance.Net  // optional, for comparison

using BitMart.Net.Clients;
using CryptoExchange.Net.SharedApis;

// BitMart exposes SharedClient on both SpotApi and UsdFuturesApi.
ISpotTickerRestClient bitMartSpotShared = new BitMartRestClient().SpotApi.SharedClient;

var btcusdt = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");

await PrintTicker(bitMartSpotShared, btcusdt);

async Task PrintTicker(ISpotTickerRestClient client, SharedSymbol symbol)
{
    var result = await client.GetSpotTickerAsync(new GetTickerRequest(symbol));
    if (!result.Success)
    {
        Console.WriteLine($"[{client.Exchange}] Failed: {result.Error}");
        return;
    }

    Console.WriteLine($"[{client.Exchange}] {result.Data.Symbol}: {result.Data.LastPrice}");
}

// Common REST shared interfaces:
//   ISpotTickerRestClient, ISpotSymbolRestClient, ISpotOrderRestClient
//   IFuturesTickerRestClient, IFuturesOrderRestClient, IFuturesSymbolRestClient
//   IBalanceRestClient, IFeeRestClient, IOrderBookRestClient

// ---- WEBSOCKET EXAMPLE - SHARED SUBSCRIPTION ----
var bitMartSocket = new BitMartSocketClient();
ITickerSocketClient bitMartTickerSocket = bitMartSocket.SpotApi.SharedClient;

var sub = await bitMartTickerSocket.SubscribeToTickerUpdatesAsync(
    new SubscribeTickerRequest(btcusdt),
    update => Console.WriteLine($"[{bitMartTickerSocket.Exchange}] {update.Data.Symbol}: {update.Data.LastPrice}"));

if (!sub.Success)
{
    Console.WriteLine($"Subscribe failed: {sub.Error}");
    return;
}

Console.WriteLine("Press Enter to exit");
Console.ReadLine();

await bitMartSocket.UnsubscribeAsync(sub.Data);

// Note: shared socket interfaces do not expose UnsubscribeAsync.
// Keep the concrete socket client and call concreteClient.UnsubscribeAsync(sub.Data).
