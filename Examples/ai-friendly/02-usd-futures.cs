// 02-usd-futures.cs
//
// Demonstrates: BitMart USD futures funding, balances, positions and order flow.
//
// Setup: dotnet add package BitMart.Net

using BitMart.Net;
using BitMart.Net.Clients;
using BitMart.Net.Enums;

var client = new BitMartRestClient(options =>
{
    options.ApiCredentials = new BitMartCredentials("API_KEY", "API_SECRET", "MEMO_OR_PASS");
});

const string symbol = "BTCUSDT";

// ---- 1. PUBLIC FUTURES MARKET DATA ----
var funding = await client.UsdFuturesApi.ExchangeData.GetCurrentFundingRateAsync(symbol);
if (!funding.Success)
{
    Console.WriteLine($"Funding rate failed: {funding.Error}");
    return;
}

Console.WriteLine($"{funding.Data.Symbol} previous funding rate: {funding.Data.PreviousFundingRate}");
Console.WriteLine($"{funding.Data.Symbol} expected funding rate: {funding.Data.ExpectedFundingRate}");

// ---- 2. AUTHENTICATED FUTURES ACCOUNT DATA ----
var balances = await client.UsdFuturesApi.Account.GetBalancesAsync();
if (!balances.Success)
{
    Console.WriteLine($"Futures balances failed: {balances.Error}");
    return;
}

foreach (var balance in balances.Data.Where(x => x.Asset == "USDT"))
{
    Console.WriteLine($"{balance.Asset}: available={balance.AvailableBalance}, equity={balance.Equity}");
}

var positions = await client.UsdFuturesApi.Trading.GetPositionsAsync(symbol);
if (positions.Success)
{
    foreach (var position in positions.Data)
    {
        Console.WriteLine($"{position.Symbol} {position.PositionSide}: qty={position.CurrentQuantity}, pnl={position.UnrealizedPnl}");
    }
}

// ---- 3. PLACE A SMALL FUTURES LIMIT ORDER ----
var order = await client.UsdFuturesApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    side: FuturesSide.BuyOpenLong,
    type: FuturesOrderType.Limit,
    quantity: 1,
    price: 1m,
    leverage: 5m,
    marginType: MarginType.CrossMargin,
    orderMode: OrderMode.GoodTilCancel);

if (!order.Success)
{
    Console.WriteLine($"Futures order rejected: {order.Error}");
    return;
}

Console.WriteLine($"Placed futures order {order.Data.OrderId}");

var cancel = await client.UsdFuturesApi.Trading.CancelOrderAsync(
    symbol: symbol,
    orderId: order.Data.OrderId.ToString());

Console.WriteLine(cancel.Success
    ? "Futures cancel request accepted"
    : $"Futures cancel failed: {cancel.Error}");
