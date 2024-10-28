using Microsoft.AspNetCore.Mvc;
using BitMart.Net.Objects;
using BitMart.Net.Interfaces.Clients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the BitMart services
builder.Services.AddBitMart();

// OR to provide API credentials for accessing private endpoints, or setting other options:
/*
builder.Services.AddBitMart(restOptions =>
{
    restOptions.ApiCredentials = new BitMartApiCredentials("<APIKEY>", "<APISECRET>", "<APIMEMO>");
    restOptions.RequestTimeout = TimeSpan.FromSeconds(5);
}, socketOptions =>
{
    socketOptions.ApiCredentials = new BitMartApiCredentials("<APIKEY>", "<APISECRET>", "<APIMEMO>");
});
*/

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Map the endpoint and inject the rest client
app.MapGet("/{Symbol}", async ([FromServices] IBitMartRestClient client, string symbol) =>
{
    var result = await client.SpotApi.ExchangeData.GetTickerAsync(symbol);
    return result.Data.LastPrice;
})
.WithOpenApi();


app.MapGet("/Balances", async ([FromServices] IBitMartRestClient client) =>
{
    var result = await client.SpotApi.Account.GetSpotBalancesAsync();
    return (object)(result.Success ? result.Data : result.Error!);
})
.WithOpenApi();

app.Run();