// 01-spot-quickstart.cs
//
// Demonstrates: BitMart spot market data, balances and order flow.
//
// Setup: dotnet add package BitMart.Net

using BitMart.Net;
using BitMart.Net.Clients;
using BitMart.Net.Enums;

var client = new BitMartRestClient(options =>
{
    options.ApiCredentials = new BitMartCredentials("API_KEY", "API_SECRET", "MEMO_OR_PASS");
});

const string symbol = "BTC_USDT";

// ---- 1. PUBLIC MARKET DATA ----
var ticker = await client.SpotApi.ExchangeData.GetTickerAsync(symbol);
if (!ticker.Success)
{
    Console.WriteLine($"Ticker failed: {ticker.Error}");
    return;
}

Console.WriteLine($"{ticker.Data.Symbol} last price: {ticker.Data.LastPrice}");
Console.WriteLine($"{ticker.Data.Symbol} 24h base volume: {ticker.Data.Volume24h}");

// ---- 2. AUTHENTICATED ACCOUNT DATA ----
var balances = await client.SpotApi.Account.GetSpotBalancesAsync();
if (!balances.Success)
{
    Console.WriteLine($"Balances failed: {balances.Error}");
    return;
}

foreach (var balance in balances.Data.Where(x => x.Asset is "BTC" or "USDT"))
{
    Console.WriteLine($"{balance.Asset}: available={balance.Available}, frozen={balance.Frozen}");
}

// ---- 3. PLACE A SMALL LIMIT ORDER ----
var order = await client.SpotApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.001m,
    price: 1m);

if (!order.Success)
{
    Console.WriteLine($"Order rejected: {order.Error}");
    return;
}

Console.WriteLine($"Placed spot order {order.Data.OrderId}");

var openOrders = await client.SpotApi.Trading.GetOpenOrdersAsync(symbol);
if (openOrders.Success)
{
    Console.WriteLine($"Open spot orders on {symbol}: {openOrders.Data.Length}");
}

var cancel = await client.SpotApi.Trading.CancelOrderAsync(
    symbol: symbol,
    orderId: order.Data.OrderId);

Console.WriteLine(cancel.Success
    ? "Cancel request accepted"
    : $"Cancel failed: {cancel.Error}");
