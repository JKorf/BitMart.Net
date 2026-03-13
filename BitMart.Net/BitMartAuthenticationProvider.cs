using BitMart.Net.Clients.SpotApi;
using BitMart.Net.Objects.Sockets;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitMart.Net
{
    internal class BitMartAuthenticationProvider : AuthenticationProvider<BitMartCredentials, HMACCredential>
    {
        private static IStringMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(BitMartExchange._serializerContext));

        public override ApiCredentialsType[] SupportedCredentialTypes => [ApiCredentialsType.Hmac];
        public BitMartAuthenticationProvider(BitMartCredentials credentials) : base(credentials)
        {
            if (string.IsNullOrEmpty(credentials.Hmac!.Pass))
                throw new ArgumentNullException(nameof(ApiCredentials.Hmac.Pass), "Passphrase is required for BitMart authentication");
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            if (!request.Authenticated)
                return;

            var timestamp = GetMillisecondTimestamp(apiClient);
            var queryParams = request.GetQueryString(false);
            var bodyParams = GetSerializedBody(_serializer, request.BodyParameters ?? new Dictionary<string, object>());
            var signStr = $"{timestamp}#{Credential.Pass}#{queryParams}{bodyParams}";

            request.Headers ??= new Dictionary<string, string>();
            request.Headers.Add("X-BM-KEY", Credential.PublicKey);
            request.Headers.Add("X-BM-SIGN", SignHMACSHA256(signStr, SignOutputType.Hex).ToLowerInvariant());
            request.Headers.Add("X-BM-TIMESTAMP", timestamp);

            request.SetBodyContent(bodyParams);
            request.SetQueryString(queryParams);
        }

        public override Query? GetAuthenticationQuery(SocketApiClient apiClient, SocketConnection connection, Dictionary<string, object?>? context = null)
        {
            var timestamp = GetMillisecondTimestamp(apiClient);
            var key = Credential.PublicKey;
            var memo = Credential.Pass;
            var sign = SignHMACSHA256($"{timestamp}#{memo}#bitmart.WebSocket", SignOutputType.Hex).ToLowerInvariant();

            if (apiClient is BitMartSocketClientSpotApi)
            {
                return new BitMartLoginQuery(apiClient, key, timestamp!, sign);
            }
            else
            {
                return new BitMartFuturesLoginQuery(key, timestamp!, sign);
            }
        }
    }
}
