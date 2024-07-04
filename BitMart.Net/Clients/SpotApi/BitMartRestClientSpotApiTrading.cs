using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using BitMart.Net.Interfaces.Clients.SpotApi;

namespace BitMart.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class BitMartRestClientSpotApiTrading : IBitMartRestClientSpotApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly BitMartRestClientSpotApi _baseClient;
        private readonly ILogger _logger;

        internal BitMartRestClientSpotApiTrading(ILogger logger, BitMartRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }
    }
}
