using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReferenceTokenExchange.Caching;
using ReferenceTokenExchange.Configuration;
using ReferenceTokenExchange.Services.Interfaces;
using System;

namespace ReferenceTokenExchange.Services
{
    public class ReferenceTokenExchangeServiceFactory : IReferenceTokenExchangeServiceFactory
    {
        private readonly ILogger<ReferenceTokenExchangeMiddleware> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ReferenceTokenExchangeServiceFactory(IServiceProvider serviceProvider, ILogger<ReferenceTokenExchangeMiddleware> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public ITokenExchangeService GetInstance(bool enableCaching = false)
        {
            if (enableCaching)
            {
                var tokenCache = _serviceProvider.GetService<ITokenExchangeCache>();

                if (tokenCache == null)
                {
                    _logger.LogWarning(
                        "Caching is enabled, but no cache provider has been registered. Register a cache provider using " +
                        "UseInMemoryCache or one of the available overloads. " +
                        "Continuing without caching.");

                    return _serviceProvider.GetService<ITokenExchangeService>();
                }

                return _serviceProvider.GetService<ICachingTokenExchangeService>();
            }

            return _serviceProvider.GetService<ITokenExchangeService>();
        }
        
    }
}
