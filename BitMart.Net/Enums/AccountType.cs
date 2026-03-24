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
    /// Account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountType>))]
    public enum AccountType
    {
        /// <summary>
        /// BitMart
        /// </summary>
        [Map("BITMART")]
        BitMart,
        /// <summary>
        /// Copper
        /// </summary>
        [Map("COPPER")]
        Copper,
        /// <summary>
        /// Cobo
        /// </summary>
        [Map("COBO")]
        Cobo
    }
}
