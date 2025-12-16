using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;

namespace BitMart.Net
{
    internal class BitMartAuthenticationProvider : AuthenticationProvider
    {
        private static IStringMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BitMartExchange._serializerContext));

        public override ApiCredentialsType[] SupportedCredentialTypes => [ApiCredentialsType.Hmac];
        public BitMartAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
            if (string.IsNullOrEmpty(credentials.Pass))
                throw new ArgumentNullException(nameof(ApiCredentials.Pass), "Passphrase is required for BitMart authentication");
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            if (!request.Authenticated)
                return;

            var timestamp = GetMillisecondTimestamp(apiClient);
            var queryParams = request.GetQueryString(false);
            var bodyParams = GetSerializedBody(_serializer, request.BodyParameters ?? new Dictionary<string, object>());
            var signStr = $"{timestamp}#{_credentials.Pass}#{queryParams}{bodyParams}";

            request.Headers ??= new Dictionary<string, string>();
            request.Headers.Add("X-BM-KEY", ApiKey);
            request.Headers.Add("X-BM-SIGN", SignHMACSHA256(signStr, SignOutputType.Hex).ToLowerInvariant());
            request.Headers.Add("X-BM-TIMESTAMP", timestamp);

            request.SetBodyContent(bodyParams);
            request.SetQueryString(queryParams);
        }

        public string Sign(string data) => SignHMACSHA256(data, SignOutputType.Hex).ToLowerInvariant();
    }
}
