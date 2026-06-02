using BitMart.Net;
using BitMart.Net.Interfaces.Clients;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the BitMart services
builder.Services.AddBitMart();

// OR to provide API credentials for accessing private endpoints, or setting other options:
/*
builder.Services.AddBitMart(options =>
{    
   options.ApiCredentials = new BitMartCredentials()
        .WithHMAC("<APIKEY>", "<APISECRET>", "<MEMO>");
   options.Rest.RequestTimeout = TimeSpan.FromSeconds(5);
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
    return result.Success
        ? Results.Ok(result.Data.LastPrice)
        : Results.Problem(result.Error?.Message, statusCode: 502);
})
.WithOpenApi();


app.MapGet("/Balances", async ([FromServices] IBitMartRestClient client) =>
{
    var result = await client.SpotApi.Account.GetSpotBalancesAsync();
    return result.Success
        ? Results.Ok(result.Data)
        : Results.Problem(result.Error?.Message, statusCode: 502);
})
.WithOpenApi();

app.Run();
