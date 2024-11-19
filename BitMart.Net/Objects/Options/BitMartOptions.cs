using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Objects.Options
{
    /// <summary>
    /// BitMart options
    /// </summary>
    public class BitMartOptions
    {
        /// <summary>
        /// Rest client options
        /// </summary>
        public BitMartRestOptions Rest { get; set; } = new BitMartRestOptions();

        /// <summary>
        /// Socket client options
        /// </summary>
        public BitMartSocketOptions Socket { get; set; } = new BitMartSocketOptions();

        /// <summary>
        /// Trade environment. Contains info about URL's to use to connect to the API. Use `BitMartEnvironment` to swap environment, for example `Environment = BitMartEnvironment.Live`
        /// </summary>
        public BitMartEnvironment? Environment { get; set; }

        /// <summary>
        /// The api credentials used for signing requests.
        /// </summary>
        public BitMartApiCredentials? ApiCredentials { get; set; }

        /// <summary>
        /// The DI service lifetime for the IBitMartSocketClient
        /// </summary>
        public ServiceLifetime? SocketClientLifeTime { get; set; }
    }
}
