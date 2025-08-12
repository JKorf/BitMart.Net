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
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects.Errors;

namespace BitMart.Net.Clients.UsdFuturesApi
{
    /// <inheritdoc cref="IBitMartRestClientUsdFuturesApi" />
    internal partial class BitMartRestClientUsdFuturesApi : RestApiClient, IBitMartRestClientUsdFuturesApi
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("UsdFutures Api");
        private readonly IBitMartRestClient _baseClient;

        internal readonly string _brokerId;

        protected override ErrorCollection ErrorMapping { get; } = new ErrorCollection(
            [
                new ErrorInfo(ErrorType.Unauthorized, false, "API key error", "30003", "30011", "30012"),
                new ErrorInfo(ErrorType.Unauthorized, false, "IP address error", "30010", "40006"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Insufficient permissions", "30019"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Restricted based on region", "40047"),

                new ErrorInfo(ErrorType.SignatureInvalid, false, "Signature invalid", "30005"),

                new ErrorInfo(ErrorType.TimestampInvalid, false, "Timestamp invalid", "30007", "30008", "40008"),

                new ErrorInfo(ErrorType.RequestRateLimited, false, "Too many requests", "30013", "30017", "40013"),

                new ErrorInfo(ErrorType.SystemError, true, "Service unavailable", "30014"),
                new ErrorInfo(ErrorType.SystemError, true, "Service maintenance", "30016"),
                new ErrorInfo(ErrorType.SystemError, true, "System error", "40012"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter error", "40007"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Order leverage too large", "40029"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Order leverage too small", "40030"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Difference between current price and trigger price too large", "40031"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Plan order life cycle too long", "40032"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Plan order life cycle too short", "40033"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Kline interval is invalid", "40038"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Timestamp is invalid", "40039"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Leverage is invalid", "40040"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OrderSide is invalid", "40041"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "OrderType is invalid", "40042"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Order precision invalid", "40043"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Order range is invalid", "40044"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Order open type is invalid", "40045"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "ClientOrderId can be a max length of 32 characters", "40049"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "ClientOrderId only allows numbers and letters", "40048"),

                new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate client order id", "40050"),

                new ErrorInfo(ErrorType.UnknownSymbol, false, "Symbol does not exist", "40034"),

                new ErrorInfo(ErrorType.UnknownOrder, false, "Order does not exist", "40035", "40037"),

                new ErrorInfo(ErrorType.SymbolNotTrading, false, "Symbol is not trading", "40014"),
                new ErrorInfo(ErrorType.SymbolNotTrading, false, "Symbol is not currently trading", "40015"),

                new ErrorInfo(ErrorType.NoPosition, false, "Position does not exist", "40021"),

                new ErrorInfo(ErrorType.BalanceInsufficient, false, "Insufficient balance", "40027", "42000"),

                new ErrorInfo(ErrorType.OrderRateLimited, false, "Order rate limit reached", "40028"),

                new ErrorInfo(ErrorType.TargetIncorrectState, false, "Order status is invalid", "40036"),

            ]
        );
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
        protected override Error ParseErrorResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor, Exception? exception)
        {
            if (!accessor.IsValid)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("message"));
            if (msg == null)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            if (code == null)
                return new ServerError(ErrorInfo.Unknown with { Message = msg }, exception);

            return new ServerError(code.Value, GetErrorInfo(code.Value, msg), exception);
        }
    }
}
