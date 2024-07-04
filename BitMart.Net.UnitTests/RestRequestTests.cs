using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitMart.Net.Clients;

namespace BitMart.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateSpotAccountCalls()
        {
            var client = new BitMartRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<BitMartRestClient>(client, "Endpoints/Spot/Account", "XXX", IsAuthenticated, stjCompare: false);
            //await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetServerTimeAsync(), "GetServerTime");
           
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestUrl?.Contains("signature") == true || result.RequestBody?.Contains("signature=") == true;
        }
    }
}
