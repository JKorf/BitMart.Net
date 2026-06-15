using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using BitMart.Net.Objects.Internal;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Converters.MessageParsing;
using System.Linq;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using BitMart.Net.Clients.MessageHandlers;
using System.Net.Http.Headers;

namespace BitMart.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IBitMartRestClientSpotApi" />
    internal partial class BitMartRestClientSpotApi : RestApiClient<BitMartEnvironment, BitMartAuthenticationProvider, BitMartCredentials>, IBitMartRestClientSpotApi
    {
        #region fields 
        
        public new BitMartRestOptions ClientOptions => (BitMartRestOptions)base.ClientOptions;

        protected override ErrorMapping ErrorMapping => BitMartErrors.SpotRestErrors;

        protected override IRestMessageHandler MessageHandler { get; } = new BitMartRestMessageHandler(BitMartErrors.SpotRestErrors);
        #endregion

        #region Api clients
            /// <inheritdoc />
        public IBitMartRestClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IBitMartRestClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IBitMartRestClientSpotApiMargin Margin { get; }
        /// <inheritdoc />
        public IBitMartRestClientSpotApiSubAccount SubAccount { get; }
        /// <inheritdoc />
        public IBitMartRestClientSpotApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "BitMart";
        #endregion

        #region constructor/destructor
        internal BitMartRestClientSpotApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, BitMartRestOptions options)
            : base(loggerFactory, BitMartExchange.Metadata.Id, httpClient, options.Environment.RestSpotClientAddress, options, options.SpotOptions)
        {
            Account = new BitMartRestClientSpotApiAccount(this);
            ExchangeData = new BitMartRestClientSpotApiExchangeData(_logger, this);
            Margin = new BitMartRestClientSpotApiMargin(this);
            SubAccount = new BitMartRestClientSpotApiSubAccount(this);
            Trading = new BitMartRestClientSpotApiTrading(_logger, this);
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BitMartExchange._serializerContext));

        /// <inheritdoc />
        protected override BitMartAuthenticationProvider CreateAuthenticationProvider(BitMartCredentials credentials)
            => new BitMartAuthenticationProvider(credentials);

        internal async Task<HttpResult> SendAsync(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? additionalHeaders = null)
        {
            if (Authenticated)
            {
                var window = (int?)ClientOptions.ReceiveWindow?.TotalMilliseconds;
                parameters ??= new Parameters(BitMartExchange._parameterSerializationSettings);
                parameters.Add("recvWindow", window);
            }

            var result = await base.SendAsync<BitMartResponse>(definition, parameters, cancellationToken, additionalHeaders, weight).ConfigureAwait(false);
            if (!result.Success)
                return result;

            if (result.Data.Code != 1000)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message)));

            return result;
        }

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? additionalHeaders = null) where T : class
        {
            if (Authenticated)
            {
                var window = (int?)ClientOptions.ReceiveWindow?.TotalMilliseconds;
                parameters ??= new Parameters(BitMartExchange._parameterSerializationSettings);
                parameters.Add("recvWindow", window);
            }

            var result = await base.SendAsync<BitMartResponse<T>>(definition, parameters, cancellationToken, additionalHeaders, weight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<T>(result);

            if (result.Data.Code != 1000)
                return HttpResult.Fail<T>(result, new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result, result.Data.Data);
        }

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => BitMartExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        public IBitMartRestClientSpotApiShared SharedClient => this;
    }
}
