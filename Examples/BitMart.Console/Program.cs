using BitMart.Net.Clients;

// REST
var bitMartRestClient = new BitMartRestClient();
var ticker = await bitMartRestClient.SpotApi.ExchangeData.GetTickerAsync("ETH_USDT");
Console.WriteLine($"Rest client ticker price for ETHUSDT: {ticker.Data.LastPrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
var bitMartSocketClient = new BitMartSocketClient();
var subscription = await bitMartSocketClient.SpotApi.SubscribeToTickerUpdatesAsync("ETH_USDT", update =>
{
    Console.WriteLine($"Websocket client ticker price for ETHUSDT: {update.Data.LastPrice}");
});

Console.ReadLine();