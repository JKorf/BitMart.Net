using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.JsonNet;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using BitMart.Net.Clients;
using BitMart.Net.Objects;

namespace BitMart.Net.UnitTests
{
    [TestFixture()]
    public class BitMartRestClientTests
    {
        [Test]
        public void CheckSignatureExample1()
        {
            var authProvider = new BitMartAuthenticationProvider(new BitMartApiCredentials("XXX", "XXX", "XXX"));
            var client = (RestApiClient)new BitMartRestClient().SpotApi;

            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Post,
                "/api/v3/order",
                (uriParams, bodyParams, headers) =>
                {
                    return headers["X-BM-SIGN"].ToString();
                },
                "1d022acf035f8d7f899dcaad0621fe39d351f7809e30ac8c96939a36548a6502",
                new Dictionary<string, object>
                {
                    { "symbol", "LTCBTC" },
                },
                DateTimeConverter.ParseFromLong(1499827319559),
                true,
                false);
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<BitMartRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<BitMartSocketClient>();
        }
    }
}
