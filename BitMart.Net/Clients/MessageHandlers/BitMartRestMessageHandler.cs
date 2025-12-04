using BitMart.Net;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageConverters;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bitget.Net.Clients.MessageHandlers
{
    internal class BitMartRestMessageHandler : JsonRestMessageHandler
    {
        private readonly ErrorMapping _errorMapping;

        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(BitMartExchange._serializerContext);

        public BitMartRestMessageHandler(ErrorMapping errorMapping)
        {
            _errorMapping = errorMapping;
        }

        public override async ValueTask<Error> ParseErrorResponse(
            int httpStatusCode,
            HttpResponseHeaders responseHeaders,
            Stream responseStream)
        {
            var (error, document) = await GetJsonDocument(responseStream).ConfigureAwait(false);
            if (error != null)
                return error;

            int? errorCode = document!.RootElement.TryGetProperty("code", out var codeProp) ? codeProp.GetInt32() : null;
            var errorMsg = document!.RootElement.TryGetProperty("msg", out var msgProp) ? msgProp.GetString() : null;
            errorMsg ??= document!.RootElement.TryGetProperty("message", out var messageProp) ? messageProp.GetString() : null;

            if (errorMsg == null)
                return new ServerError(ErrorInfo.Unknown);

            if (errorCode == null)
                return new ServerError(ErrorInfo.Unknown with { Message = errorMsg });

            return new ServerError(errorCode.Value, _errorMapping.GetErrorInfo(errorCode.Value.ToString(), errorMsg));
        }
    }
}
