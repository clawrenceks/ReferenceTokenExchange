using Microsoft.Extensions.Logging;
using Clawrenceks.ReferenceTokenExchange.Caching;
using Clawrenceks.ReferenceTokenExchange.Models;
using Clawrenceks.ReferenceTokenExchange.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Clawrenceks.ReferenceTokenExchange.Configuration;

namespace Clawrenceks.ReferenceTokenExchange.Services
{
    public class TokenExchangeServiceWithCache : ICachingTokenExchangeService
    {
        private readonly ITokenExchangeCache _cache;
        private readonly ILogger<ReferenceTokenExchangeMiddleware> _logger;
        private readonly ITokenExchangeService _tokenExchangeService;

        public TokenExchangeServiceWithCache(ITokenExchangeService tokenExchangeService ,ITokenExchangeCache cache, ILogger<ReferenceTokenExchangeMiddleware> logger)
        {
            _logger = logger;
            _cache = cache;
            _tokenExchangeService = tokenExchangeService;
        }

        public async Task<TokenExchangeResult> ExchangeTokenAsync(string referenceToken, ReferenceTokenExchangeOptions options)
        {
            _logger.LogInformation("Checking cache for the presence of token");
            var cachedToken = _cache.FindToken(referenceToken);

            if (cachedToken == null)
            {
                _logger.LogInformation("Token not found in cache");
                var tokenResult = await PerformOnlineTokenExchange(referenceToken, options);

                AddTokenToCache(tokenResult);
                return tokenResult;
            }

            if (cachedToken.AccessTokenExpiryTime < DateTimeOffset.UtcNow)
            {
                _logger.LogInformation("Expired token found in cache");
                _cache.RemoveToken(cachedToken);

                var tokenResult = await PerformOnlineTokenExchange(referenceToken, options);

                AddTokenToCache(tokenResult);
                return tokenResult;
            }

            _logger.LogInformation("Token found in cache, returning token");
            return cachedToken;
        }

        private async Task<TokenExchangeResult> PerformOnlineTokenExchange(string referenceToken, ReferenceTokenExchangeOptions options)
        {
           return await _tokenExchangeService.ExchangeTokenAsync(referenceToken, options);
        }

        private void AddTokenToCache(TokenExchangeResult tokenExchangeResult)
        {
            if (tokenExchangeResult.AccessToken == null)
            {
                return;
            }

            _cache.AddToken(tokenExchangeResult);
        }
    }
}
