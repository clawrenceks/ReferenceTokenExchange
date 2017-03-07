using Microsoft.Extensions.DependencyInjection;
using Clawrenceks.ReferenceTokenExchange.Caching;
using Clawrenceks.ReferenceTokenExchange.Services;
using Clawrenceks.ReferenceTokenExchange.Services.Interfaces;

namespace Clawrenceks.ReferenceTokenExchange.Configuration
{
    public static class ReferenceTokenExchangeBuilderExtensions
    {
        public static IReferenceTokenExchangeBuilder AddCaching(this IReferenceTokenExchangeBuilder builder)
        {
            AddCaching<InMemoryTokenExchangeCache>(builder, ServiceLifetime.Singleton);
            return builder;
        }

        public static IReferenceTokenExchangeBuilder AddCaching<T>(this IReferenceTokenExchangeBuilder builder, ServiceLifetime lifetime )
            where T: class, ITokenExchangeCache
        {
            builder.Services.AddTransient<ICachingTokenExchangeService, TokenExchangeServiceWithCache>();

            var service = new ServiceDescriptor(typeof(ITokenExchangeCache), typeof(T), lifetime);
            builder.Services.Add(service);
            return builder;
        }
    }
}
