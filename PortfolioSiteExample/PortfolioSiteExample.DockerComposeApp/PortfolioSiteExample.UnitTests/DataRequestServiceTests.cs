using Moq;
using PortfolioSiteExample.Frontend.Services;
using PortfolioSiteExample.Frontend.Services.Interfaces;
using PortfolioSiteExample.Shared.Enums;
using PortfolioSiteExample.Shared.Requests;
using PortfolioSiteExample.Shared.Responses;
using System.Collections.Generic;
using Xunit;

namespace PortfolioSiteExample.UnitTests
{
    public class DataRequestServiceTests
    {
        [Fact]
        public void Validate_GetExample_Returns_Valid_Result()
        {
            var mockNetworkRequestService = new Mock<INetworkRequestService>();
            mockNetworkRequestService.Setup(x => x.SendGetRequest<AnswerResponse>(It.IsAny<string>(), It.IsAny<AnswerRequest>()))
                .Returns(new AnswerResponse
                {
                    Answer = new Dictionary<Question, string>() { { Question.OverAge50, "123"} }
                });

            var dataRequestService = new DataRequestService(mockNetworkRequestService.Object);

            var result = dataRequestService.GetAnswers(new AnswerRequest
            {
                Question = new List<Question>()
                {
                    Question.OverAge50
                }
            });

            Assert.Equal("123", result.Answer[Question.OverAge50]);
        }
    }
}
