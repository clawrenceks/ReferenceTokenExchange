using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Clawrenceks.ReferenceTokenExchange.Models;
using Clawrenceks.ReferenceTokenExchange.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace Clawrenceks.ReferenceTokenExchange.Configuration
{
    public class ReferenceTokenExchangeMiddleware
    {
        private readonly ILogger<ReferenceTokenExchangeMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly ReferenceTokenExchangeOptions _options;

        public ReferenceTokenExchangeMiddleware(RequestDelegate next, ReferenceTokenExchangeOptions options,
            ILogger<ReferenceTokenExchangeMiddleware> logger)
        {
            _next = next;
            _options = options;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, IReferenceTokenExchangeServiceFactory tokenExchangeServiceFactory)
        {
            _logger.LogInformation("Starting reference token exchange");

            var tokenExchangeService = tokenExchangeServiceFactory.GetInstance(_options.EnableCaching);

            var tokenParseResult = ParseBearerTokenFromHttpHeaders(context.Request.Headers);

            if (!tokenParseResult.IsError)
            {
                var referenceToken = tokenParseResult.Result;
                var tokenResponse = await tokenExchangeService.ExchangeTokenAsync(referenceToken, _options);

                context.Request.Headers.Add(_options.DelegationTokenHeaderName, tokenResponse.AccessToken);
                
                if (_options.UpdateAuthorizationHeader)
                {
                    context.Request.Headers.Remove("Authorization");

                    var authorizationHeader = "Bearer " + tokenResponse.AccessToken;                
                    context.Request.Headers.Add("Authorization", new StringValues(authorizationHeader));
                }

            }

            await _next.Invoke(context);            
        }

        private OperationResult ParseBearerTokenFromHttpHeaders(IHeaderDictionary headers)
        {
            var result = new OperationResult();            
            var authHeader = headers["Authorization"];

            if (authHeader.Count < 1)
            {
                var error = "No Authorization header found - token exchange cannot be perfored";
                _logger.LogWarning(error);

                result.IsError = true;
                result.Errors.Add(error);
                return result;
            }

            authHeader = authHeader.ToString().Split(' ');
            if (authHeader[0] != "Bearer" || authHeader.Count != 2)
            {
                var error = "Authorization header is not of type Bearer, or the header could not be parsed";
                _logger.LogWarning(error);

                result.IsError = true;
                result.Errors.Add(error);
                return result;
            }

            var bearerToken = authHeader[1];
            result.Result = bearerToken;
            result.IsError = false;

            _logger.LogDebug("Authorization header parsed successfully");
            return result;
        }
    }
}
