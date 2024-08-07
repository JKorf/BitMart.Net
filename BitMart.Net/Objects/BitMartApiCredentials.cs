using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using System.Security;

namespace BitMart.Net.Objects
{
    /// <inheritdoc />
    public class BitMartApiCredentials : ApiCredentials
    {
        /// <summary>
        /// Memo
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="key">The API key</param>
        /// <param name="secret">The API secret</param>
        /// <param name="memo">API key memo</param>
        public BitMartApiCredentials(string key, string secret, string memo) : base(key, secret)
        {
            Memo = memo;
        }

        /// <summary>
        /// Copy
        /// </summary>
        /// <returns></returns>
        public override ApiCredentials Copy()
        {
            return new BitMartApiCredentials(Key, Secret, Memo);
        }
    }
}
