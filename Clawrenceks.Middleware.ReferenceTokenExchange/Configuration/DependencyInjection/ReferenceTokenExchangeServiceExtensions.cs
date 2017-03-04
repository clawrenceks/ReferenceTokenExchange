using Microsoft.Extensions.DependencyInjection;
using ReferenceTokenExchange.Services;
using ReferenceTokenExchange.Services.Interfaces;

namespace ReferenceTokenExchange.Configuration
{
    public static class ReferenceTokenExchangeServiceExtensions
    {
        public static IReferenceTokenExchangeBuilder AddReferenceTokenExchange(this IServiceCollection services)
        {
            services.AddTransient<IReferenceTokenExchangeServiceFactory, ReferenceTokenExchangeServiceFactory>();
            services.AddTransient<ITokenExchangeService, TokenExchangeService>();
            return new ReferenceTokenExchangeBuilder(services);
        }
    }
}
