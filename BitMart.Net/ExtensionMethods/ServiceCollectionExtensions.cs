using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using BitMart.Net.Clients;
using BitMart.Net.Interfaces;
using BitMart.Net.Interfaces.Clients;
using BitMart.Net.Objects.Options;
using BitMart.Net.SymbolOrderBooks;
using CryptoExchange.Net;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the IBitMartRestClient and IBitMartSocketClient to the sevice collection so they can be injected
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="defaultRestOptionsDelegate">Set default options for the rest client</param>
        /// <param name="defaultSocketOptionsDelegate">Set default options for the socket client</param>
        /// <param name="socketClientLifeTime">The lifetime of the IBitMartSocketClient for the service collection. Defaults to Singleton.</param>
        /// <returns></returns>
        public static IServiceCollection AddBitMart(
            this IServiceCollection services,
            Action<BitMartRestOptions>? defaultRestOptionsDelegate = null,
            Action<BitMartSocketOptions>? defaultSocketOptionsDelegate = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            var restOptions = BitMartRestOptions.Default.Copy();

            if (defaultRestOptionsDelegate != null)
            {
                defaultRestOptionsDelegate(restOptions);
                BitMartRestClient.SetDefaultOptions(defaultRestOptionsDelegate);
            }

            if (defaultSocketOptionsDelegate != null)
                BitMartSocketClient.SetDefaultOptions(defaultSocketOptionsDelegate);

            services.AddHttpClient<IBitMartRestClient, BitMartRestClient>(options =>
            {
                options.Timeout = restOptions.RequestTimeout;
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                if (restOptions.Proxy != null)
                {
                    handler.Proxy = new WebProxy
                    {
                        Address = new Uri($"{restOptions.Proxy.Host}:{restOptions.Proxy.Port}"),
                        Credentials = restOptions.Proxy.Password == null ? null : new NetworkCredential(restOptions.Proxy.Login, restOptions.Proxy.Password)
                    };
                }
                return handler;
            });

            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddSingleton<ICryptoSocketClient, CryptoSocketClient>();
            services.AddSingleton<IBitMartOrderBookFactory, BitMartOrderBookFactory>();
            services.AddTransient(x => x.GetRequiredService<IBitMartRestClient>().SpotApi.CommonSpotClient);

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IBitMartRestClient>().SpotApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IBitMartSocketClient>().SpotApi.SharedClient);
            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<IBitMartRestClient>().UsdFuturesApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<IBitMartSocketClient>().UsdFuturesApi.SharedClient);

            if (socketClientLifeTime == null)
                services.AddSingleton<IBitMartSocketClient, BitMartSocketClient>();
            else
                services.Add(new ServiceDescriptor(typeof(IBitMartSocketClient), typeof(BitMartSocketClient), socketClientLifeTime.Value));
            return services;
        }
    }
}
