using Clawrenceks.Middleware.ReferenceTokenExchange.Models;

namespace Clawrenceks.Middleware.ReferenceTokenExchange.Caching
{
    public interface ITokenExchangeCache
    {
        TokenExchangeResult FindToken(string referenceToken);

        void AddToken(TokenExchangeResult tokenResult);

        void RemoveToken(TokenExchangeResult tokenResult);
    }
}
