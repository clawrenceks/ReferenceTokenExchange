using ReferenceTokenExchange.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReferenceTokenExchange.Caching
{
    public class InMemoryTokenExchangeCache : ITokenExchangeCache
    {
        private readonly List<TokenExchangeResult> _tokens = new List<TokenExchangeResult>();

        public TokenExchangeResult FindToken(string referenceToken)
        {
            return _tokens.FirstOrDefault(t => t.ReferenceToken == referenceToken);
        }

        public void AddToken(TokenExchangeResult tokenResult)
        {
            _tokens.Add(tokenResult);
        }

        public void RemoveToken(TokenExchangeResult tokenResult)
        {
            _tokens.Remove(tokenResult);
        }



    }
}
