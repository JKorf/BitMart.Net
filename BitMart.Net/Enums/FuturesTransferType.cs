using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Transfer type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesTransferType>))]
    public enum FuturesTransferType
    {
        /// <summary>
        /// Spot to contract
        /// </summary>
        [Map("spot_to_contract")]
        SpotToContract,
        /// <summary>
        /// Contract to spot
        /// </summary>
        [Map("contract_to_spot")]
        ContractToSpot
    }
}
