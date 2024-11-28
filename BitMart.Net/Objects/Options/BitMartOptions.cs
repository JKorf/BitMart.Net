using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitMart.Net.Objects.Options
{
    /// <summary>
    /// BitMart options
    /// </summary>
    public class BitMartOptions : LibraryOptions<BitMartRestOptions, BitMartSocketOptions, BitMartApiCredentials, BitMartEnvironment>
    {
    }
}
