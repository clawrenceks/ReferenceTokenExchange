using Microsoft.AspNetCore.Builder;

namespace Clawrenceks.ReferenceTokenExchange.Configuration
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseReferenceTokenExchange(this IApplicationBuilder builder, ReferenceTokenExchangeOptions options)
        {
            return builder.UseMiddleware<ReferenceTokenExchangeMiddleware>(options);
        }

    }
}
