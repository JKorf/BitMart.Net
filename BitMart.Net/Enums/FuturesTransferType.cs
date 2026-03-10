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
        /// ["<c>spot_to_contract</c>"] Spot to contract
        /// </summary>
        [Map("spot_to_contract")]
        SpotToContract,
        /// <summary>
        /// ["<c>contract_to_spot</c>"] Contract to spot
        /// </summary>
        [Map("contract_to_spot")]
        ContractToSpot
    }
}
