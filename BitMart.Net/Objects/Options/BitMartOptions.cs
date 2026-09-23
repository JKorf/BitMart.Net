using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Configuration;
using System;

namespace BitMart.Net.Objects.Options
{
    /// <summary>
    /// BitMart options
    /// </summary>
    public class BitMartOptions : LibraryOptions<BitMartRestOptions, BitMartSocketOptions, BitMartCredentials, BitMartEnvironment>
    {
        /// <summary>
        /// Options for Shared API usage
        /// </summary>
        public SharedApiOptions SharedApi { get; set; } = new();

        /// <summary>
        /// Create options using the provided configuration action
        /// </summary>
        public static BitMartOptions Create(Action<BitMartOptions>? configure = null)
        {
            var options = CreateUnconfigured();
            configure?.Invoke(options);
            return Normalize(options);
        }

        /// <summary>
        /// Create options using the provided configuration
        /// </summary>
        public static BitMartOptions CreateFromConfiguration(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var options = CreateUnconfigured();

            try
            {
                configuration.Bind(options);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Invalid BitMart configuration provided", ex);
            }

            if (options.Environment != null)
                options.Environment = BitMartEnvironment.GetEnvironmentByName(options.Environment.Name) ?? options.Environment;
            if (options.Rest?.Environment != null)
                options.Rest.Environment = BitMartEnvironment.GetEnvironmentByName(options.Rest.Environment.Name) ?? options.Rest.Environment;
            if (options.Socket?.Environment != null)
                options.Socket.Environment = BitMartEnvironment.GetEnvironmentByName(options.Socket.Environment.Name) ?? options.Socket.Environment;

            return Normalize(options);
        }

        private static BitMartOptions CreateUnconfigured()
        {
            var options = new BitMartOptions();
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            return options;
        }

        private static BitMartOptions Normalize(BitMartOptions options)
        {
            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            options.Rest.Environment ??= options.Environment ?? BitMartEnvironment.Live;
            options.Rest.ApiCredentials ??= options.ApiCredentials;
            options.Socket.Environment ??= options.Environment ?? BitMartEnvironment.Live;
            options.Socket.ApiCredentials ??= options.ApiCredentials;
            return options;
        }
    }
}
