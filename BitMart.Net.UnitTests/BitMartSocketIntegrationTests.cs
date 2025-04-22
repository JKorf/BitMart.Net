﻿using BitMart.Net.Clients;
using BitMart.Net.Objects.Models;
using BitMart.Net.Objects.Options;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace BitMart.Net.UnitTests
{
    [NonParallelizable]
    internal class BitMartSocketIntegrationTests : SocketIntegrationTest<BitMartSocketClient>
    {
        public override bool Run { get; set; }

        public BitMartSocketIntegrationTests()
        {
        }

        public override BitMartSocketClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");
            var pass = Environment.GetEnvironmentVariable("APIPASS");

            Authenticated = key != null && sec != null;
            return new BitMartSocketClient(Options.Create(new BitMartSocketOptions
            {
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec, pass) : null
            }), loggerFactory);
        }

        [Test]
        public async Task TestSubscriptions()
        {
            await RunAndCheckUpdate<BitMartTickerUpdate>((client, updateHandler) => client.SpotApi.SubscribeToBalanceUpdatesAsync(default , default), false, true);
            await RunAndCheckUpdate<BitMartTickerUpdate>((client, updateHandler) => client.SpotApi.SubscribeToTickerUpdatesAsync("ETH_USDT", updateHandler, default), true, false);

            await RunAndCheckUpdate<BitMartFuturesBalanceUpdate>((client, updateHandler) => client.UsdFuturesApi.SubscribeToBalanceUpdatesAsync(default, default), false, true);
            await RunAndCheckUpdate<BitMartFuturesTickerUpdate>((client, updateHandler) => client.UsdFuturesApi.SubscribeToTickerUpdatesAsync("ETHUSDT", updateHandler, default), true, false);
        } 
    }
}
