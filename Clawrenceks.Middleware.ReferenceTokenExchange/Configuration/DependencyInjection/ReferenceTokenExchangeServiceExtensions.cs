using Microsoft.Extensions.DependencyInjection;
using Clawrenceks.ReferenceTokenExchange.Services;
using Clawrenceks.ReferenceTokenExchange.Services.Interfaces;

namespace Clawrenceks.ReferenceTokenExchange.Configuration
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
