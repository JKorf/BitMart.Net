using CryptoExchange.Net.Objects;
using BitMart.Net.Clients.SpotApi;
using BitMart.Net.Interfaces.Clients.SpotApi;

namespace BitMart.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class BitMartRestClientSpotApiAccount : IBitMartRestClientSpotApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitMartRestClientSpotApi _baseClient;

        internal BitMartRestClientSpotApiAccount(BitMartRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }
    }
}
