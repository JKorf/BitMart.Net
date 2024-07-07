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
            var authProvider = new BitMartAuthenticationProvider(new BitMartApiCredentials("XXX", "XXX"));
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
                "2C75A72F6582D24AECD79F313BBF2F4D8690FAA7BF628230BD31D3DE9C48A23D",
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
