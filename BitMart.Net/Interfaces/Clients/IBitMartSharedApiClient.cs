using BitMart.Net.Interfaces.Clients.SpotApi;
using BitMart.Net.Interfaces.Clients.UsdFuturesApi;

namespace BitMart.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of BitMart
    /// </summary>
    public interface IBitMartSharedApiClient
    {
        /// <summary>
        /// Spot REST shared API implementations
        /// </summary>
        IBitMartRestClientSpotSharedApi SpotRest { get; }

        /// <summary>
        /// Futures REST shared API implementations
        /// </summary>
        IBitMartRestClientUsdFuturesSharedApi FuturesRest { get; }

        /// <summary>
        /// Spot WebSocket shared API implementations
        /// </summary>
        IBitMartSocketClientSpotSharedApi SpotSocket { get; }

        /// <summary>
        /// Futures WebSocket shared API implementations
        /// </summary>
        IBitMartSocketClientUsdFuturesSharedApi FuturesSocket { get; }
    }
}
