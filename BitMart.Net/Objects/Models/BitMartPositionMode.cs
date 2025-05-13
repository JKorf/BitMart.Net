using BitMart.Net.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Position mode
    /// </summary>
    public record BitMartPositionMode
    {
        /// <summary>
        /// The position mode of the account
        /// </summary>
        [JsonPropertyName("position_mode")]
        public PositionMode PositionMode { get; set; }
    }
}
