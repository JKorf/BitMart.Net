﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Models
{
    internal record BitMartSpotBalanceWrapper
    {
        /// <summary>
        /// Wallet
        /// </summary>
        [JsonPropertyName("wallet")]
        public IEnumerable<BitMartSpotBalance> Wallet { get; set; } = Array.Empty<BitMartSpotBalance>();
    }

    /// <summary>
    /// 
    /// </summary>
    public record BitMartSpotBalance
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Available
        /// </summary>
        [JsonPropertyName("available")]
        public decimal Available { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Frozen
        /// </summary>
        [JsonPropertyName("frozen")]
        public decimal Frozen { get; set; }
    }


}