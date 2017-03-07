using System;

namespace Clawrenceks.ReferenceTokenExchange.Models
{
    public class TokenExchangeResult
    {
        public string ReferenceToken { get; internal set; }
        public string AccessToken { get; internal set; }
        public DateTimeOffset AccessTokenExpiryTime { get; internal set; }
    }
}
