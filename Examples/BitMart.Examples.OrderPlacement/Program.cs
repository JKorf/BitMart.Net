using BitMart.Net;
using BitMart.Net.Clients;
using BitMart.Net.Enums;

const string spotSymbol = "BTC_USDT";
const string futuresSymbol = "ETHUSDT";

// Replace with valid credentials or order placement will always fail
var apiKey = "KEY";
var apiSecret = "SECRET";
var memo = "MEMO";

Console.WriteLine("BitMart.Net order placement example");
Console.WriteLine();
Console.WriteLine("This example can place real orders when valid credentials are configured.");
Console.WriteLine();

var client = new BitMartRestClient(options =>
{
    options.ApiCredentials = new BitMartCredentials()
        .WithHMAC(apiKey, apiSecret, memo);
});

await PlaceSpotLimitOrderAsync(client);
Console.WriteLine();
await PlaceFuturesCloseOrderExampleAsync(client);

static async Task PlaceSpotLimitOrderAsync(BitMartRestClient client)
{
    Console.WriteLine($"Placing spot limit buy order for {spotSymbol}...");

    var ticker = await client.SpotApi.ExchangeData.GetTickerAsync(spotSymbol);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get spot ticker: {ticker.Error}");
        return;
    }

    var safePrice = Math.Round(ticker.Data.LastPrice * 0.95m, 2);
    var order = await client.SpotApi.Trading.PlaceOrderAsync(
        symbol: spotSymbol,
        side: OrderSide.Buy,
        type: OrderType.Limit,
        quantity: 0.001m,
        price: safePrice);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place spot order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed spot order {order.Data.OrderId}");

    var orderStatus = await client.SpotApi.Trading.GetOrderAsync(order.Data.OrderId);
    if (orderStatus.Success)
        Console.WriteLine($"Spot order status: {orderStatus.Data.Status}, filled: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query spot order: {orderStatus.Error}");

    var cancel = await client.SpotApi.Trading.CancelOrderAsync(spotSymbol, order.Data.OrderId);
    Console.WriteLine(cancel.Success
        ? $"Cancelled spot order {order.Data.OrderId}"
        : $"Failed to cancel spot order: {cancel.Error}");
}

static async Task PlaceFuturesCloseOrderExampleAsync(BitMartRestClient client)
{
    Console.WriteLine($"Placing futures close-short limit order for {futuresSymbol}...");

    var contracts = await client.UsdFuturesApi.ExchangeData.GetContractsAsync(futuresSymbol);
    if (!contracts.Success)
    {
        Console.WriteLine($"Failed to get futures contract details: {contracts.Error}");
        return;
    }

    var lastPrice = contracts.Data.SingleOrDefault()?.LastPrice;
    if (lastPrice == null)
    {
        Console.WriteLine("Failed to get futures contract price");
        return;
    }

    var safePrice = Math.Round(lastPrice.Value * 0.95m, 2);
    var order = await client.UsdFuturesApi.Trading.PlaceOrderAsync(
        symbol: futuresSymbol,
        side: FuturesSide.BuyCloseShort,
        type: FuturesOrderType.Limit,
        quantity: 1,
        price: safePrice,
        leverage: 1,
        marginType: MarginType.CrossMargin,
        orderMode: OrderMode.GoodTilCancel);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place futures order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed futures order {order.Data.OrderId}");

    var orderStatus = await client.UsdFuturesApi.Trading.GetOrderAsync(futuresSymbol, order.Data.OrderId.ToString());
    if (orderStatus.Success)
        Console.WriteLine($"Futures order status: {orderStatus.Data.Status}, filled: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query futures order: {orderStatus.Error}");

    var cancel = await client.UsdFuturesApi.Trading.CancelOrderAsync(futuresSymbol, order.Data.OrderId.ToString());
    Console.WriteLine(cancel.Success
        ? $"Cancelled futures order {order.Data.OrderId}"
        : $"Failed to cancel futures order: {cancel.Error}");
}
