namespace Clawrenceks.Middleware.ReferenceTokenExchange.Services.Interfaces
{
    public interface IReferenceTokenExchangeServiceFactory
    {
        ITokenExchangeService GetInstance(bool enableCaching);
    }
}
