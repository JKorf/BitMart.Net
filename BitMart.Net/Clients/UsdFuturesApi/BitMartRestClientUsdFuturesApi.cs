using BitMart.Net.Clients.MessageHandlers;
using BitMart.Net.Interfaces.Clients;
using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using BitMart.Net.Objects.Internal;
using BitMart.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc cref="IBitMartRestClientUsdFuturesApi" />
    internal partial class BitMartRestClientUsdFuturesApi : RestApiClient<BitMartEnvironment, BitMartAuthenticationProvider, BitMartCredentials>, IBitMartRestClientUsdFuturesApi
    {
        #region fields 
        private readonly IBitMartRestClient _baseClient;

        public new BitMartRestOptions ClientOptions => (BitMartRestOptions)base.ClientOptions;
        protected override ErrorMapping ErrorMapping => BitMartErrors.FuturesRestErrors;
        protected override IRestMessageHandler MessageHandler { get; } = new BitMartRestMessageHandler(BitMartErrors.FuturesRestErrors);
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IBitMartRestClientUsdFuturesApiAccount Account { get; }
        /// <inheritdoc />
        public IBitMartRestClientUsdFuturesApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBitMartRestClientUsdFuturesApiSubAccount SubAccount { get; }
        /// <inheritdoc />
        public IBitMartRestClientUsdFuturesApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "BitMart";
        #endregion

        #region constructor/destructor
        internal BitMartRestClientUsdFuturesApi(ILoggerFactory? loggerFactory, IBitMartRestClient baseClient, HttpClient? httpClient, BitMartRestOptions options)
            : base(loggerFactory, BitMartExchange.Metadata.Id, httpClient, options.Environment.RestFuturesClientAddress, options, options.UsdFuturesOptions)
        {
            Account = new BitMartRestClientUsdFuturesApiAccount(this);
            SubAccount = new BitMartRestClientUsdFuturesApiSubAccount(this);
            ExchangeData = new BitMartRestClientUsdFuturesApiExchangeData(_logger, this);
            Trading = new BitMartRestClientUsdFuturesApiTrading(_logger, this);

            _baseClient = baseClient;
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BitMartExchange._serializerContext));

        public IBitMartRestClientUsdFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        protected override BitMartAuthenticationProvider CreateAuthenticationProvider(BitMartCredentials credentials)
            => new BitMartAuthenticationProvider(credentials);

        internal async Task<HttpResult> SendAsync(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<BitMartResponse>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success)
                return result;

            if (result.Data.Code != 1000)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message)));

            return result;
        }

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? additionalHeaders = null) where T : class
        {
            var result = await base.SendAsync<BitMartResponse<T>>(definition, parameters, cancellationToken, additionalHeaders, weight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<T>(result);

            if (result.Data.Code != 1000)
                return HttpResult.Fail<T>(result, new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => _baseClient.SpotApi.ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => BitMartExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);
    }
}
