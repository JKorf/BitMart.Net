using CryptoExchange.Net.Authentication;

namespace BitMart.Net
{
    /// <summary>
    /// BitMart credentials
    /// </summary>
    public class BitMartCredentials : ApiCredentials
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        /// <param name="pass">The API passphrase</param>
        public BitMartCredentials(string apiKey, string secret, string pass) : this(new HMACCredential(apiKey, secret, pass)) { }
       
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="credential">The HMAC credentials</param>
        public BitMartCredentials(HMACCredential credential) : base(credential) { }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new BitMartCredentials(Hmac!);
    }
}
