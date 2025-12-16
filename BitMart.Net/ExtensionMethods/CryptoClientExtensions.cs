using BitMart.Net.Clients;
using BitMart.Net.Interfaces.Clients;
using CryptoExchange.Net.Interfaces.Clients;

namespace CryptoExchange.Net.ExtensionMethods
{
    /// <summary>
    /// Extensions for the ICryptoRestClient and ICryptoSocketClient interfaces
    /// </summary>
    public static class CryptoClientExtensions
    {
        /// <summary>
        /// Get the BitMart REST Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IBitMartRestClient BitMart(this ICryptoRestClient baseClient) => baseClient.TryGet<IBitMartRestClient>(() => new BitMartRestClient());

        /// <summary>
        /// Get the BitMart Websocket Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IBitMartSocketClient BitMart(this ICryptoSocketClient baseClient) => baseClient.TryGet<IBitMartSocketClient>(() => new BitMartSocketClient());
    }
}
