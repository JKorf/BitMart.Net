using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Transfer type
    /// </summary>
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
