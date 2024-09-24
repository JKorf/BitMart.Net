using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;
using BitMart.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using BitMart.Net.Objects;
using BitMart.Net.Interfaces.Clients;
using BitMart.Net.Objects.Internal;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Converters.MessageParsing;
using BitMart.Net.Interfaces.Clients.SpotApi;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc cref="IBitMartRestClientUsdFuturesApi" />
    internal partial class BitMartRestClientUsdFuturesApi : RestApiClient, IBitMartRestClientUsdFuturesApi
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("UsdFutures Api");
        private readonly IBitMartRestClient _baseClient;

        internal readonly string _brokerId;
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
            _brokerId = !string.IsNullOrEmpty(options.BrokerId) ? options.BrokerId! : "EASYTRADING0001";
        }
        #endregion

        /// <inheritdoc />
        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor();
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

        public IBitMartRestClientUsdFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BitMartAuthenticationProvider((BitMartApiCredentials)credentials);

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<BitMartResponse>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 1000)
                return result.AsDatalessError(new ServerError(result.Data.Code, result.Data.Message));

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
                return result.AsError<T>(new ServerError(result.Data.Code, result.Data.Message));

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
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode apiType, DateTime? deliverTime = null)
            => baseAsset + quoteAsset;

        /// <inheritdoc />
        protected override Error ParseErrorResponse(int httpStatusCode, IEnumerable<KeyValuePair<string, IEnumerable<string>>> responseHeaders, IMessageAccessor accessor)
        {
            if (!accessor.IsJson)
                return new ServerError(accessor.GetOriginalString());

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("message"));
            if (msg == null)
                return new ServerError(accessor.GetOriginalString());

            if (code == null)
                return new ServerError(msg);

            return new ServerError(code.Value, msg);
        }
    }
}
