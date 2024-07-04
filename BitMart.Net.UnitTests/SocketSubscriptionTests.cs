using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System.Threading.Tasks;
using BitMart.Net.Clients;
using BitMart.Net.Objects.Models;

namespace BitMart.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateSpotExchangeDataSubscriptions()
        {
            var client = new BitMartSocketClient(opts =>
            {
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<BitMartSocketClient>(client, "Subscriptions/Spot/ExchangeData", "XXX", "data", stjCompare: false);
            //await tester.ValidateAsync<BitMartModel>((client, handler) => client.SpotApi.SubscribeToXXXUpdatesAsync(handler), "XXX");
        }
    }
}
