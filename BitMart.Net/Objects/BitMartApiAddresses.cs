namespace BitMart.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class BitMartApiAddresses
    {
        /// <summary>
        /// The address used by the BitMartRestClient for the Spot API
        /// </summary>
        public string RestSpotClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the BitMartRestClient for the Futures API
        /// </summary>
        public string RestFuturesClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the BitMartSocketClient for the websocket Spot API
        /// </summary>
        public string SocketSpotClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the BitMartSocketClient for the websocket Perpetual Futures API
        /// </summary>
        public string SocketPerpetualFuturesClientAddress { get; set; } = "";

        /// <summary>
        /// The production environment for the BitMart API. Uses Futures V2.
        /// </summary>
        public static BitMartApiAddresses Default = new BitMartApiAddresses
        {
            RestSpotClientAddress = "https://api-cloud.bitmart.com",
            RestFuturesClientAddress = "https://api-cloud-v2.bitmart.com",
            SocketSpotClientAddress = "wss://ws-manager-compress.bitmart.com",
            SocketPerpetualFuturesClientAddress = "wss://openapi-ws-v2.bitmart.com"
        };

        /// <summary>
        /// Production environment, but uses Futures V1 instead of Futures V2.
        /// </summary>
        public static BitMartApiAddresses FuturesV1 = new BitMartApiAddresses
        {
            RestSpotClientAddress = "https://api-cloud.bitmart.com",
            RestFuturesClientAddress = "https://api-cloud.bitmart.com",
            SocketSpotClientAddress = "wss://ws-manager-compress.bitmart.com",
            SocketPerpetualFuturesClientAddress = "wss://openapi-ws.bitmart.com"
        };
    }
}
