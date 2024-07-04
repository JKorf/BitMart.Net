using System;
using System.Collections.Generic;
using System.Net.Http;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;

namespace BitMart.Net
{
    internal class BitMartAuthenticationProvider : AuthenticationProvider
    {
        public string GetApiKey() => _credentials.Key!.GetString();

        public BitMartAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, IDictionary<string, object> uriParameters, IDictionary<string, object> bodyParameters, Dictionary<string, string> headers, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, RequestBodyFormat requestBodyFormat)
        {
            headers = new Dictionary<string, string>() { };

            if (!auth)
                return;

            // Auth
        }
    }
}
