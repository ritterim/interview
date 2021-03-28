using PortfolioSiteExample.Shared.Requests;

namespace PortfolioSiteExample.Frontend.Services.Interfaces
{
    public interface INetworkRequestService
    {
        T SendPostRequest<T>(string endpoint, AnswerRequest answerRequest);
    }
}