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
using BitMart.Net.Objects;
using CryptoExchange.Net.Converters.MessageParsing;
using System.Linq;
using BitMart.Net.Enums;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects.Errors;

namespace BitMart.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IBitMartRestClientSpotApi" />
    internal partial class BitMartRestClientSpotApi : RestApiClient, IBitMartRestClientSpotApi
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Spot Api");
        internal readonly string _brokerId;

        protected override ErrorCollection ErrorMapping { get; } = new ErrorCollection(
            [
                new ErrorInfo(ErrorType.Unauthorized, false, "Deposit not allowed", "60020"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Withdrawal not allowed", "60021"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Withdrawal not allowed for 24 hours", "60022"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Institutional verification required", "61007"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Account restricted based on region", "53001", "53008"),
                new ErrorInfo(ErrorType.Unauthorized, false, "KYC verification required", "53002", "53006", "53007"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Insufficient permissions", "53003", "53005"),
                new ErrorInfo(ErrorType.Unauthorized, false, "Symbol not allowed based on region", "53004"),
                new ErrorInfo(ErrorType.Unauthorized, false, "QR code certification required", "53009"),

                new ErrorInfo(ErrorType.SystemError, false, "Internal server error", "60051"),
                new ErrorInfo(ErrorType.SystemError, false, "Internal server exception", "60052"),
                new ErrorInfo(ErrorType.SystemError, true, "Service unavailable", "50022"),

                new ErrorInfo(ErrorType.InvalidParameter, false, "Maximum precision exceeded", "60006"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid address", "60011"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Withdrawal amount invalid", "60013"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Withdrawal memo invalid", "60014"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Check withdrawal target", "60053"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Asset not supported", "60055"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "StartTime and endTime should be less than 90 days", "60066", "60067"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameter error", "60000"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "StartTime value invalid", "71001"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "EndTime value invalid", "71002"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "StartTime or endTime value invalid", "71003"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Kline limit parameter error", "71004", "50004"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Limit/offset parameter error", "50013"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Limit min value is 1", "50015"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Offset min value is 1", "50017"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid parameter", "50021"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Order book size error", "50024"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Parameters do not match", "50029"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "ClientOrderId can be a max length of 32 characters", "50037"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "ClientOrderId only allows numbers and letters", "50038"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "Period out of range", "50041"),
                new ErrorInfo(ErrorType.InvalidParameter, false, "EndTime must be greater than startTime", "52004"),

                new ErrorInfo(ErrorType.MissingParameter, false, "Quantity parameter required", "50010"),
                new ErrorInfo(ErrorType.MissingParameter, false, "Price parameter required", "50011"),
                new ErrorInfo(ErrorType.MissingParameter, false, "QuoteQuantity parameter required", "50012"),
                new ErrorInfo(ErrorType.MissingParameter, false, "Limit parameter required", "50014"),
                new ErrorInfo(ErrorType.MissingParameter, false, "Offset parameter required", "50016"),
                new ErrorInfo(ErrorType.MissingParameter, false, "Missing parameters", "50028"),
                new ErrorInfo(ErrorType.MissingParameter, false, "One of orderId and clientOrderId is required", "50039"),

                new ErrorInfo(ErrorType.UnknownAsset, false, "Asset does not exist", "60002", "51000"),

                new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", "70002"),
                new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", "50000"),

                new ErrorInfo(ErrorType.SymbolNotTrading, false, "Symbol unavailable", "50040"),

                new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate client order id", "50042"),

                new ErrorInfo(ErrorType.UnknownOrder, false, "Unknown order", "50005"),

                new ErrorInfo(ErrorType.QuantityInvalid, false, "Quantity too small", "60005", "51015"),
                new ErrorInfo(ErrorType.QuantityInvalid, false, "Quantity should be greater than 0", "61000"),
                new ErrorInfo(ErrorType.QuantityInvalid, false, "Less than minimum quantity", "50006", "51009"),
                new ErrorInfo(ErrorType.QuantityInvalid, false, "More than maximum quantity", "50007"),
                new ErrorInfo(ErrorType.QuantityInvalid, false, "Less than minimum order value", "50009", "51011"),
                new ErrorInfo(ErrorType.QuantityInvalid, false, "Quantity should be between 0 and 10", "50033"),

                new ErrorInfo(ErrorType.PriceInvalid, false, "Less than minimum price", "50008", "51010"),
                new ErrorInfo(ErrorType.PriceInvalid, false, "More than maximum price", "50025"),
                new ErrorInfo(ErrorType.PriceInvalid, false, "Buy order price can't be higher than open price", "50026"),
                new ErrorInfo(ErrorType.PriceInvalid, false, "Sell order price can't be lower than open price", "50027"),

                new ErrorInfo(ErrorType.BalanceInsufficient, false, "Insufficient balance", "60008", "61001", "50020"),

                new ErrorInfo(ErrorType.TargetIncorrectState, false, "Order already canceled", "50030"),
                new ErrorInfo(ErrorType.TargetIncorrectState, false, "Order already completed", "50031"),
                new ErrorInfo(ErrorType.TargetIncorrectState, false, "Order matched or canceled", "50032"),
                new ErrorInfo(ErrorType.TargetIncorrectState, false, "Order failed to cancel", "50036"),
            ]
        );
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
        internal BitMartRestClientSpotApi(ILogger logger, HttpClient? httpClient, BitMartRestOptions options)
            : base(logger, httpClient, options.Environment.RestSpotClientAddress, options, options.SpotOptions)
        {
            Account = new BitMartRestClientSpotApiAccount(this);
            ExchangeData = new BitMartRestClientSpotApiExchangeData(logger, this);
            Margin = new BitMartRestClientSpotApiMargin(this);
            SubAccount = new BitMartRestClientSpotApiSubAccount(this);
            Trading = new BitMartRestClientSpotApiTrading(logger, this);

            _brokerId = !string.IsNullOrEmpty(options.BrokerId) ? options.BrokerId! : "EASYTRADING0001";
        }
        #endregion

        /// <inheritdoc />
        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(BitMartExchange._serializerContext));
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BitMartExchange._serializerContext));

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BitMartAuthenticationProvider(credentials);

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? additionalHeaders = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight, additionalHeaders);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null, Dictionary<string, string>? additionalHeaders = null)
        {
            if (Authenticated)
            {
                var window = (int?)((BitMartRestOptions)ClientOptions).ReceiveWindow?.TotalMilliseconds;
                parameters ??= new ParameterCollection();
                parameters.AddOptional("recvWindow", window);
            }

            var result = await base.SendAsync<BitMartResponse>(baseAddress, definition, parameters, cancellationToken, additionalHeaders, weight).ConfigureAwait(false);
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
            if (Authenticated)
            {
                var window = (int?)((BitMartRestOptions)ClientOptions).ReceiveWindow?.TotalMilliseconds;
                parameters ??= new ParameterCollection();
                parameters.AddOptional("recvWindow", window);
            }

            var result = await base.SendAsync<BitMartResponse<T>>(baseAddress, definition, parameters, cancellationToken, additionalHeaders, weight).ConfigureAwait(false);
            if (!result)
                return result.As<T>(default);

            if (result.Data.Code != 1000)
                return result.AsError<T>(new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message)));

            return result.As(result.Data.Data);
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

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

        protected override ServerRateLimitError ParseRateLimitResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor)
        {
            var error = base.ParseRateLimitResponse(httpStatusCode, responseHeaders, accessor);
            var retryAfterHeader = responseHeaders.SingleOrDefault(r => r.Key.Equals("X-BM-RateLimit-Reset", StringComparison.InvariantCultureIgnoreCase));
            if (retryAfterHeader.Value?.Any() != true)
                return error;

            var value = retryAfterHeader.Value.First();
            if (!int.TryParse(value, out var seconds))
                return error;

            error.RetryAfter = DateTime.UtcNow.AddSeconds(seconds);
            return error;
        }

        /// <inheritdoc />
        public IBitMartRestClientSpotApiShared SharedClient => this;
    }
}
