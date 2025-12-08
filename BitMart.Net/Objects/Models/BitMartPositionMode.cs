using BitMart.Net.Enums;
using System.Text.Json.Serialization;

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
