using PortfolioSiteExample.Shared.Requests;
using PortfolioSiteExample.Shared.Responses;

namespace PortfolioSiteExample.Api.Services.Interfaces
{
    public interface IDataProcessingService
    {
        AnswerResponse GetAnswers(AnswerRequest answerRequest);
    }
}