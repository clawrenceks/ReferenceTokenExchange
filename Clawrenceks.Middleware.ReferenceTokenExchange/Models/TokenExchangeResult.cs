using System;

namespace Clawrenceks.ReferenceTokenExchange.Models
{
    public class TokenExchangeResult
    {
        public string ReferenceToken { get; set; }
        public string AccessToken { get; set; }
        public DateTimeOffset AccessTokenExpiryTime { get; set; }
    }
}
