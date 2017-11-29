using System.Net.Http;

namespace Clawrenceks.ReferenceTokenExchange.Configuration
{
    public class ReferenceTokenExchangeOptions
    {
        public ReferenceTokenExchangeOptions()
        {
            
        }

        public string IdentityServerRootUrl { get; set; }
        public string DelegationTokenHeaderName { get; set;}
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string GrantType { get; set; }
        public string Scope { get; set; }
        public bool EnableCaching { get; set; } = false;
        public HttpMessageHandler HttpHandler { get; set; } = new HttpClientHandler();

    }
}
