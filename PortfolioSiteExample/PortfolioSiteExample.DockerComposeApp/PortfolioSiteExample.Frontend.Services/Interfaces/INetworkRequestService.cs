using PortfolioSiteExample.Shared.Requests;

namespace PortfolioSiteExample.Frontend.Services.Interfaces
{
    public interface INetworkRequestService
    {
        T SendGetRequest<T>(string endpoint, AnswerRequest answerRequest);
    }
}