namespace BitMart.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class BitMartApiAddresses
    {
        /// <summary>
        /// The address used by the BitMartRestClient for the API
        /// </summary>
        public string RestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the BitMartSocketClient for the websocket Spot API
        /// </summary>
        public string SocketSpotClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the BitMartSocketClient for the websocket Perpetual Futures API
        /// </summary>
        public string SocketPerpetualFuturesClientAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the BitMart API
        /// </summary>
        public static BitMartApiAddresses Default = new BitMartApiAddresses
        {
            RestClientAddress = "https://api-cloud.bitmart.com",
            SocketSpotClientAddress = "wss://ws-manager-compress.bitmart.com",
            SocketPerpetualFuturesClientAddress = "wss://openapi-ws.bitmart.com"
        };
    }
}
