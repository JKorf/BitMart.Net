using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using System.Security;

namespace BitMart.Net.Objects
{
    /// <inheritdoc />
    public class BitMartApiCredentials : ApiCredentials
    {
        /// <summary>
        /// API key memo/passphrase
        /// </summary>
        public string PassPhrase { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="key">The API key</param>
        /// <param name="secret">The API secret</param>
        /// <param name="passPhrase">API key memo/passphrase</param>
        public BitMartApiCredentials(string key, string secret, string passPhrase) : base(key, secret)
        {
            PassPhrase = passPhrase;
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <returns></returns>
        public override ApiCredentials Copy()
        {
            return new BitMartApiCredentials(Key, Secret, PassPhrase);
        }
    }
}
