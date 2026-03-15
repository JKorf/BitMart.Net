using CryptoExchange.Net.Authentication;
using System;

namespace BitMart.Net
{
    /// <summary>
    /// BitMart credentials
    /// </summary>
    public class BitMartCredentials : ApiCredentials
    {
        /// <summary>
        /// </summary>
        [Obsolete("Parameterless constructor is only for deserialization purposes and should not be used directly. Use parameterized constructor instead.")]
        public BitMartCredentials() { }

        /// <summary>
        /// Create credentials using an HMAC key, secret and passphrase.
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        /// <param name="pass">The API passphrase</param>
        public BitMartCredentials(string apiKey, string secret, string pass) : this(new HMACCredential(apiKey, secret, pass)) { }

        /// <summary>
        /// Create credentials using HMAC credentials
        /// </summary>
        /// <param name="credential">The HMAC credentials</param>
        public BitMartCredentials(HMACCredential credential) : base(credential) { }

        /// <inheritdoc />
#pragma warning disable CS0618 // Type or member is obsolete
        public override ApiCredentials Copy() => new BitMartCredentials { CredentialPairs = CredentialPairs };
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
