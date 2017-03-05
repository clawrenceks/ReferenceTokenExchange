using Clawrenceks.ReferenceTokenExchange.Configuration;
using Clawrenceks.ReferenceTokenExchange.Models;
using System.Threading.Tasks;

namespace Clawrenceks.ReferenceTokenExchange.Services.Interfaces
{
    public interface ITokenExchangeService
    {
        Task<TokenExchangeResult> ExchangeTokenAsync(string referenceToken, ReferenceTokenExchangeOptions options);
    }
}
