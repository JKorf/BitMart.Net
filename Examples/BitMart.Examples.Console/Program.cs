using BitMart.Net.Clients;

// REST
var bitMartRestClient = new BitMartRestClient();
var ticker = await bitMartRestClient.SpotApi.ExchangeData.GetTickerAsync("ETH_USDT");
if (!ticker.Success)
{
    Console.WriteLine($"Failed to get ticker: {ticker.Error}");
    return;
}

Console.WriteLine($"Rest client ticker price for ETHUSDT: {ticker.Data.LastPrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
var bitMartSocketClient = new BitMartSocketClient();
var subscription = await bitMartSocketClient.SpotApi.SubscribeToTickerUpdatesAsync("ETH_USDT", update =>
{
    Console.WriteLine($"Websocket client ticker price for ETH_USDT: {update.Data.LastPrice}");
});

if (!subscription.Success)
{
    Console.WriteLine($"Failed to subscribe to ticker updates: {subscription.Error}");
    return;
}

Console.ReadLine();
