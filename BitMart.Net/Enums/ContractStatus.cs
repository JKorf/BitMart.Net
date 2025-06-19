using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitMart.Net.Enums
{
    /// <summary>
    /// Contract status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractStatus>))]
    public enum ContractStatus
    {
        /// <summary>
        /// Trading
        /// </summary>
        [Map("Trading")]
        Trading,
        /// <summary>
        /// Delisted
        /// </summary>
        [Map("Delisted")]
        Delisted
    }
}
