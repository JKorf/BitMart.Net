using Bitget.Net.Clients.MessageHandlers;
using BitMart.Net.Interfaces.Clients;
using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using BitMart.Net.Objects;
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
    internal partial class BitMartRestClientUsdFuturesApi : RestApiClient, IBitMartRestClientUsdFuturesApi
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("UsdFutures Api");
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
        internal BitMartRestClientUsdFuturesApi(ILogger logger, IBitMartRestClient baseClient, HttpClient? httpClient, BitMartRestOptions options)
            : base(logger, httpClient, options.Environment.RestFuturesClientAddress, options, options.UsdFuturesOptions)
        {
            Account = new BitMartRestClientUsdFuturesApiAccount(this);
            SubAccount = new BitMartRestClientUsdFuturesApiSubAccount(this);
            ExchangeData = new BitMartRestClientUsdFuturesApiExchangeData(logger, this);
            Trading = new BitMartRestClientUsdFuturesApiTrading(logger, this);

            _baseClient = baseClient;
        }
        #endregion

        /// <inheritdoc />
        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(BitMartExchange._serializerContext));
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BitMartExchange._serializerContext));

        public IBitMartRestClientUsdFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BitMartAuthenticationProvider(credentials);

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<BitMartResponse>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 1000)
                return result.AsDatalessError(new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message)));

            return result.AsDataless();
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? additionalHeaders = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight, additionalHeaders);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? additionalHeaders = null) where T : class
        {
            var result = await base.SendAsync<BitMartResponse<T>>(baseAddress, definition, parameters, cancellationToken, additionalHeaders, weight).ConfigureAwait(false);
            if (!result)
                return result.As<T>(default);

            if (result.Data.Code != 1000)
                return result.AsError<T>(new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message)));

            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => _baseClient.SpotApi.ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp, ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval, _timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => _timeSyncState.TimeOffset;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => BitMartExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        protected override Error ParseErrorResponse(int httpStatusCode, HttpResponseHeaders responseHeaders, IMessageAccessor accessor, Exception? exception)
        {
            if (!accessor.IsValid)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("message")) ?? accessor.GetValue<string>(MessagePath.Get().Property("msg"));
            if (msg == null)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            if (code == null)
                return new ServerError(ErrorInfo.Unknown with { Message = msg }, exception);

            return new ServerError(code.Value, GetErrorInfo(code.Value, msg), exception);
        }
    }
}
