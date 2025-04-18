using System;
using System.Collections.Generic;
using System.Net.Http;
using BitMart.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;

namespace BitMart.Net
{
    internal class BitMartAuthenticationProvider : AuthenticationProvider
    {
        private static IMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BitMartExchange._serializerContext));

        public BitMartAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
            if (string.IsNullOrEmpty(credentials.Pass))
                throw new ArgumentNullException(nameof(ApiCredentials.Pass), "Passphrase is required for BitMart authentication");
        }

        public override void AuthenticateRequest(
            RestApiClient apiClient,
            Uri uri,
            HttpMethod method, 
            ref IDictionary<string, object>? uriParameters,
            ref IDictionary<string, object>? bodyParameters, 
            ref Dictionary<string, string>? headers,
            bool auth, 
            ArrayParametersSerialization arraySerialization, 
            HttpMethodParameterPosition parameterPosition, 
            RequestBodyFormat requestBodyFormat)
        {
            if (!auth)
                return;

            var timestamp = GetMillisecondTimestamp(apiClient);
            var paramStr = string.Empty;
            if (uriParameters != null)
                paramStr += uriParameters.ToFormData();
            if (bodyParameters != null)
                paramStr += _serializer.Serialize(bodyParameters);

            var signStr = $"{timestamp}#{_credentials.Pass}#{paramStr}";

            headers ??= new Dictionary<string, string>();
            headers.Add("X-BM-KEY", ApiKey);
            headers.Add("X-BM-SIGN", SignHMACSHA256(signStr, SignOutputType.Hex).ToLowerInvariant());
            headers.Add("X-BM-TIMESTAMP", timestamp);
        }

        public string Sign(string data) => SignHMACSHA256(data, SignOutputType.Hex).ToLowerInvariant();
    }
}
