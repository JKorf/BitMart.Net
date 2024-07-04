using CryptoExchange.Net.Objects;
using BitMart.Net.Clients.UsdFuturesApi;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc />
    public class BitMartRestClientUsdFuturesApiAccount : IBitMartRestClientUsdFuturesApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitMartRestClientUsdFuturesApi _baseClient;

        internal BitMartRestClientUsdFuturesApiAccount(BitMartRestClientUsdFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }
    }
}
