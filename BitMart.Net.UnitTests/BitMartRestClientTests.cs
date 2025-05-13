using CryptoExchange.Net.Clients;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using BitMart.Net.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CryptoExchange.Net.Objects;
using BitMart.Net.Interfaces.Clients;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace BitMart.Net.UnitTests
{
    [TestFixture()]
    public class BitMartRestClientTests
    {
        [Test]
        public void CheckSignatureExample1()
        {
            var authProvider = new BitMartAuthenticationProvider(new ApiCredentials("XXX", "XXX", "XXX"));
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
                DateTimeConverter.ParseFromDouble(1499827319559),
                true,
                false);
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<BitMartRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<BitMartSocketClient>();
        }

        [Test]
        [TestCase(TradeEnvironmentNames.Live, "https://api-cloud.bitmart.com")]
        [TestCase("", "https://api-cloud.bitmart.com")]
        public void TestConstructorEnvironments(string environmentName, string expected)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "BitMart:Environment:Name", environmentName },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddBitMart(configuration.GetSection("BitMart"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IBitMartRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo(expected));
        }

        [Test]
        public void TestConstructorNullEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "BitMart", null },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddBitMart(configuration.GetSection("BitMart"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IBitMartRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api-cloud.bitmart.com"));
        }

        [Test]
        public void TestConstructorApiOverwriteEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "BitMart:Environment:Name", "test" },
                    { "BitMart:Rest:Environment:Name", "live" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddBitMart(configuration.GetSection("BitMart"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<IBitMartRestClient>();

            var address = client.SpotApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api-cloud.bitmart.com"));
        }

        [Test]
        public void TestConstructorConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ApiCredentials:Key", "123" },
                    { "ApiCredentials:Secret", "456" },
                    { "ApiCredentials:Pass", "000" },
                    { "Socket:ApiCredentials:Key", "456" },
                    { "Socket:ApiCredentials:Secret", "789" },
                    { "Socket:ApiCredentials:Pass", "xxx" },
                    { "Rest:OutputOriginalData", "true" },
                    { "Socket:OutputOriginalData", "false" },
                    { "Rest:Proxy:Host", "host" },
                    { "Rest:Proxy:Port", "80" },
                    { "Socket:Proxy:Host", "host2" },
                    { "Socket:Proxy:Port", "81" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddBitMart(configuration);
            var provider = collection.BuildServiceProvider();

            var restClient = provider.GetRequiredService<IBitMartRestClient>();
            var socketClient = provider.GetRequiredService<IBitMartSocketClient>();

            Assert.That(((BaseApiClient)restClient.SpotApi).OutputOriginalData, Is.True);
            Assert.That(((BaseApiClient)socketClient.SpotApi).OutputOriginalData, Is.False);
            Assert.That(((BaseApiClient)restClient.SpotApi).AuthenticationProvider.ApiKey, Is.EqualTo("123"));
            Assert.That(((BaseApiClient)socketClient.SpotApi).AuthenticationProvider.ApiKey, Is.EqualTo("456"));
            Assert.That(((BaseApiClient)restClient.SpotApi).ClientOptions.Proxy.Host, Is.EqualTo("host"));
            Assert.That(((BaseApiClient)restClient.SpotApi).ClientOptions.Proxy.Port, Is.EqualTo(80));
            Assert.That(((BaseApiClient)socketClient.SpotApi).ClientOptions.Proxy.Host, Is.EqualTo("host2"));
            Assert.That(((BaseApiClient)socketClient.SpotApi).ClientOptions.Proxy.Port, Is.EqualTo(81));
        }
    }
}
