using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Clawrenceks.ReferenceTokenExchange.Configuration;
using Clawrenceks.ReferenceTokenExchange.Models;
using Clawrenceks.ReferenceTokenExchange.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Clawrenceks.ReferenceTokenExchange.Services
{
    public class TokenExchangeService : ITokenExchangeService
    {
        private readonly ILogger<ReferenceTokenExchangeMiddleware> _logger;

        public TokenExchangeService(ILogger<ReferenceTokenExchangeMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task<TokenExchangeResult> ExchangeTokenAsync(string referenceToken, ReferenceTokenExchangeOptions options)
        {
            if (referenceToken == null)
            {
                referenceToken = string.Empty;
            }

            var payload = new
            {
                token = referenceToken
            };

            var identityServer = await DiscoverIdentityServerAsync(options.IdentityServerRootUrl);
            var client = new TokenClient(identityServer.TokenEndpoint, options.ClientId, options.ClientSecret);

            _logger.LogInformation($"Requesting token exchange from Identity Server: {identityServer.TokenEndpoint}");
            var tokenResponse = await client.RequestCustomGrantAsync(options.GrantType, options.Scope, payload);

            if (tokenResponse.IsError)
            {
                _logger.LogWarning($"An error occured during reference token exchange: {tokenResponse.Error}");
            }

            var result = new TokenExchangeResult
            {
                ReferenceToken = referenceToken,
                AccessToken = tokenResponse.AccessToken,
                AccessTokenExpiryTime = DateTimeOffset.UtcNow.AddSeconds(tokenResponse.ExpiresIn)
            };

            _logger.LogInformation("Token exchange with Identity Server completed successfully");
            return result;
        }

        private async Task<DiscoveryResponse> DiscoverIdentityServerAsync(string identityServerRootUrl)
        {
            var client = new DiscoveryClient(identityServerRootUrl);

            _logger.LogDebug($"Discovering Identity Server metadata from {client.Url}");
            var response = await client.GetAsync();

            if (response.IsError)
            {
                _logger.LogError($"An error occured during metadata discovery: {response.Exception}");
                throw new InvalidOperationException(response.Error, response.Exception);
            }

            return response;
        }
    }
}
