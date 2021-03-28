using PortfolioSiteExample.Data.Models;
using PortfolioSiteExample.Frontend.Services.Interfaces;

namespace PortfolioSiteExample.Frontend.Services
{
    public class DataRequestService : IDataRequestService
    {
        private readonly INetworkRequestService _networkRequestService;

        public DataRequestService(INetworkRequestService networkRequestService)
        {
            _networkRequestService = networkRequestService;
        }

        public Example GetExample()
        {
            return _networkRequestService.SendGetRequest<Example>("/ApiData/GetExample");
        }
    }
}
