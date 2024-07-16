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
    internal class BitMartAuthenticationProvider : AuthenticationProvider<BitMartApiCredentials>
    {
        private static IMessageSerializer _serializer = new SystemTextJsonMessageSerializer();
        public string GetApiKey() => _credentials.Key!.GetString();
        public string GetMemo() => _credentials.Memo!.GetString();

        public BitMartAuthenticationProvider(BitMartApiCredentials credentials) : base(credentials)
        {
        }

        public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, IDictionary<string, object> uriParameters, IDictionary<string, object> bodyParameters, Dictionary<string, string> headers, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, RequestBodyFormat requestBodyFormat)
        {
            if (!auth)
                return;

            var timestamp = GetMillisecondTimestamp(apiClient);
            var paramStr = uriParameters.ToFormData() + _serializer.Serialize(bodyParameters);
            var signStr = $"{timestamp}#{GetMemo()}#{paramStr}";

            headers.Add("X-BM-KEY", GetApiKey());
            headers.Add("X-BM-SIGN", SignHMACSHA256(signStr, SignOutputType.Hex).ToLowerInvariant());
            headers.Add("X-BM-TIMESTAMP", timestamp);
        }

        public string Sign(string data) => SignHMACSHA256(data, SignOutputType.Hex).ToLowerInvariant();
    }
}
