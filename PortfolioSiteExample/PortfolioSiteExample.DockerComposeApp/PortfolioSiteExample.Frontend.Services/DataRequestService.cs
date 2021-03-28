using PortfolioSiteExample.Frontend.Services.Interfaces;
using PortfolioSiteExample.Shared.Requests;
using PortfolioSiteExample.Shared.Responses;

namespace PortfolioSiteExample.Frontend.Services
{
    public class DataRequestService : IDataRequestService
    {
        private readonly INetworkRequestService _networkRequestService;

        public DataRequestService(INetworkRequestService networkRequestService)
        {
            _networkRequestService = networkRequestService;
        }

        public AnswerResponse GetAnswers(AnswerRequest answerRequest)
        {
            return _networkRequestService.SendGetRequest<AnswerResponse>("/ApiData/GetAnswers", answerRequest);
        }
    }
}
