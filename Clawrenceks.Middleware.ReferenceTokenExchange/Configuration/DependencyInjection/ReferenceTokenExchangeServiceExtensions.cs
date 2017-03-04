using Microsoft.Extensions.DependencyInjection;
using Clawrenceks.Middleware.ReferenceTokenExchange.Services;
using Clawrenceks.Middleware.ReferenceTokenExchange.Services.Interfaces;

namespace Clawrenceks.Middleware.ReferenceTokenExchange.Configuration
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
