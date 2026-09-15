using BitMart.Net;
using BitMart.Net.Clients;
using BitMart.Net.Interfaces;
using BitMart.Net.Interfaces.Clients;
using BitMart.Net.Objects.Options;
using BitMart.Net.SymbolOrderBooks;
using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add services such as the IBitMartRestClient and IBitMartSocketClient. Configures the services based on the provided configuration.<br />
        /// See <see href="https://github.com/JKorf/BitMart.Net/blob/main/Examples/example-config.json" /> for an example of how to set up the configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddBitMart(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = BitMartOptions.CreateFromConfiguration(configuration);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddBitMartCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// Add services such as the IBitMartRestClient and IBitMartSocketClient. Services will be configured based on the provided options.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="optionsDelegate">Set options for the BitMart services</param>
        /// <returns></returns>
        public static IServiceCollection AddBitMart(
            this IServiceCollection services,
            Action<BitMartOptions>? optionsDelegate = null)
        {
            var options = BitMartOptions.Create(optionsDelegate);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddBitMartCore(services, options.SocketClientLifeTime);
        }

        private static IServiceCollection AddBitMartCore(
            this IServiceCollection services,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<IBitMartRestClient, BitMartRestClient>((client, serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<BitMartRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
                return new BitMartRestClient(client, serviceProvider.GetRequiredService<ILoggerFactory>(), serviceProvider.GetRequiredService<IOptions<BitMartRestOptions>>());
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) => {
                var options = serviceProvider.GetRequiredService<IOptions<BitMartRestOptions>>().Value;
                return LibraryHelpers.CreateHttpClientMessageHandler(options);
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
            services.Add(new ServiceDescriptor(typeof(IBitMartSocketClient), x => { return new BitMartSocketClient(x.GetRequiredService<IOptions<BitMartSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<IBitMartOrderBookFactory, BitMartOrderBookFactory>();
            services.AddTransient<IBitMartTrackerFactory, BitMartTrackerFactory>();
            services.AddTransient<ITrackerFactory, BitMartTrackerFactory>();
            services.AddSingleton<IBitMartUserClientProvider, BitMartUserClientProvider>(x =>
            new BitMartUserClientProvider(
                x.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(IBitMartRestClient).Name),
                x.GetRequiredService<ILoggerFactory>(),
                x.GetRequiredService<IOptions<BitMartRestOptions>>(),
                x.GetRequiredService<IOptions<BitMartSocketOptions>>()));

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IBitMartRestClient>().SpotApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IBitMartSocketClient>().SpotApi.SharedClient);
            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IBitMartRestClient>().UsdFuturesApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IBitMartSocketClient>().UsdFuturesApi.SharedClient);

            services.RegisterSharedApiClient<
                IBitMartSharedApiClient,
                BitMartSharedApiClient>(sharedApis => sharedApis
                    .Add(client => client.SpotRest)
                    .Add(client => client.SpotSocket)
                    .Add(client => client.UsdFuturesRest)
                    .Add(client => client.UsdFuturesSocket)
                    );

            return services;
        }
    }
}
