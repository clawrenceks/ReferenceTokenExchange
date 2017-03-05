using Clawrenceks.ReferenceTokenExchange.Models;

namespace Clawrenceks.ReferenceTokenExchange.Caching
{
    public interface ITokenExchangeCache
    {
        TokenExchangeResult FindToken(string referenceToken);

        void AddToken(TokenExchangeResult tokenResult);

        void RemoveToken(TokenExchangeResult tokenResult);
    }
}
