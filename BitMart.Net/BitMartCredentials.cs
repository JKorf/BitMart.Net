using CryptoExchange.Net.Authentication;
using System;

namespace BitMart.Net
{
    /// <summary>
    /// BitMart API credentials
    /// </summary>
    public class BitMartCredentials : HMACPassCredential
    {
        /// <summary>
        /// Create new credentials
        /// </summary>
        public BitMartCredentials() { }

        /// <summary>
        /// Create new credentials providing credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        /// <param name="pass">Passphrase</param>
        public BitMartCredentials(string key, string secret, string pass) : base(key, secret, pass)
        {
        }

        /// <summary>
        /// Create new credentials providing HMAC credentials
        /// </summary>
        /// <param name="credential">HMAC credentials</param>
        public BitMartCredentials(HMACPassCredential credential) : base(credential.Key, credential.Secret, credential.Pass)
        {
        }

        /// <summary>
        /// Specify the HMAC credentials
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        /// <param name="pass">Passphrase</param>
        public BitMartCredentials WithHMAC(string key, string secret, string pass)
        {
            if (!string.IsNullOrEmpty(Key)) throw new InvalidOperationException("Credentials already set");

            Key = key;
            Secret = secret;
            Pass = pass;
            return this;
        }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new BitMartCredentials(this);
    }
}
