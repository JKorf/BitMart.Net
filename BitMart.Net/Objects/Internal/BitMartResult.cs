using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BitMart.Net.Objects.Internal
{
    internal record BitMartResult
    {
        /// <summary>
        /// Result
        /// </summary>
        [JsonPropertyName("result")]
        public bool Result { get; set; }
    }


}
