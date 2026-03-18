using CryptoExchange.Net.Authentication;

namespace BitMart.Net
{
    /// <summary>
    /// BitMart API credentials
    /// </summary>
    public class BitMartCredentials : HMACCredential
    {
        /// <summary>
        /// Create new credentials providing only credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        /// <param name="pass">Passphrase</param>
        public BitMartCredentials(string key, string secret, string pass) : base(key, secret, pass)
        {
        }
    }
}
