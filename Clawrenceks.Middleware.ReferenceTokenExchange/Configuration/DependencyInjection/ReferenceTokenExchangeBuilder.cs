using System;
using Microsoft.Extensions.DependencyInjection;

namespace Clawrenceks.ReferenceTokenExchange.Configuration
{
    public class ReferenceTokenExchangeBuilder : IReferenceTokenExchangeBuilder
    {
        public ReferenceTokenExchangeBuilder(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            Services = services;
        }
        public IServiceCollection Services { get; }
    }
}
