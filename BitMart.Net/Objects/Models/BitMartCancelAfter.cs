﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BitMart.Net.Objects.Models
{
    /// <summary>
    /// Cancel after info
    /// </summary>
    public record BitMartCancelAfter
    {
        /// <summary>
        /// Result
        /// </summary>
        [JsonPropertyName("result")]
        public bool Result { get; set; }
        /// <summary>
        /// Time of setting
        /// </summary>
        [JsonPropertyName("set_time")]
        public DateTime SetTime { get; set; }
        /// <summary>
        /// Time of cancelling
        /// </summary>
        [JsonPropertyName("cancel_time")]
        public DateTime CancelTime { get; set; }
    }
}
