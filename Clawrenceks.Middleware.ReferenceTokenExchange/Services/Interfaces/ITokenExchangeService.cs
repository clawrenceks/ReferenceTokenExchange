using ReferenceTokenExchange.Configuration;
using ReferenceTokenExchange.Models;
using System.Threading.Tasks;

namespace ReferenceTokenExchange.Services.Interfaces
{
    public interface ITokenExchangeService
    {
        Task<TokenExchangeResult> ExchangeTokenAsync(string referenceToken, ReferenceTokenExchangeOptions options);
    }
}
