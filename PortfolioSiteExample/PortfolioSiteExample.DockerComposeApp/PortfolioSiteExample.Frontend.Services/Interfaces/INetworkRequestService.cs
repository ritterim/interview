namespace PortfolioSiteExample.Frontend.Services.Interfaces
{
    public interface INetworkRequestService
    {
        T SendGetRequest<T>(string endpoint);
    }
}