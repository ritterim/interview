using PortfolioSiteExample.Shared.Requests;
using PortfolioSiteExample.Shared.Responses;

namespace PortfolioSiteExample.Frontend.Services.Interfaces
{
    public interface IDataRequestService
    {
        AnswerResponse GetAnswers(AnswerRequest answerRequest);
    }
}