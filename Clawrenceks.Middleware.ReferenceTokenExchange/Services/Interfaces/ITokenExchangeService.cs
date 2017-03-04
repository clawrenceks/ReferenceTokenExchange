using Clawrenceks.Middleware.ReferenceTokenExchange.Configuration;
using Clawrenceks.Middleware.ReferenceTokenExchange.Models;
using System.Threading.Tasks;

namespace Clawrenceks.Middleware.ReferenceTokenExchange.Services.Interfaces
{
    public interface ITokenExchangeService
    {
        Task<TokenExchangeResult> ExchangeTokenAsync(string referenceToken, ReferenceTokenExchangeOptions options);
    }
}
