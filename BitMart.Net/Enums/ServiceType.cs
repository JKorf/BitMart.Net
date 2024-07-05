using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// System type
    /// </summary>
    public enum ServiceType
    {
        /// <summary>
        /// Spot API service
        /// </summary>
        [Map("spot")]
        SpotApiService,
        /// <summary>
        /// Contract API service
        /// </summary>
        [Map("contract")]
        ContractApiService,
        /// <summary>
        /// Account API service
        /// </summary>
        [Map("account")]
        AccountApiService,
    }
}
