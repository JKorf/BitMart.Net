using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Objects.Models;

namespace BitMart.Net.Clients.SpotApi
{
    /// <inheritdoc />
    public class BitMartRestClientSpotApiExchangeData : IBitMartRestClientSpotApiExchangeData
    {
        private readonly BitMartRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal BitMartRestClientSpotApiExchangeData(ILogger logger, BitMartRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "XXX", BitMartExchange.RateLimiter.BitMart, 1, true);
            var result = await _baseClient.SendAsync<BitMartModel>(request, null, ct).ConfigureAwait(false);
            throw new NotImplementedException();
        }

        #endregion

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartAsset>>> GetAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();

            var request = _definitions.GetOrCreate(HttpMethod.Get, "spot/v1/currencies", , 1);
            var result = await _baseClient.SendAsync<BitMartAssetWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartAsset>>(result.Data?.Currencies);
        }



        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<BitMartSymbol>>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/spot/v1/symbols/details", null, 0);
            var result = await _baseClient.SendAsync<BitMartSymbolWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<BitMartSymbol>>(result.Data?.Symbols);
        }



    }
}
