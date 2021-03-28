using Moq;
using PortfolioSiteExample.Data.Models;
using PortfolioSiteExample.Frontend.Services;
using PortfolioSiteExample.Frontend.Services.Interfaces;
using Xunit;

namespace PortfolioSiteExample.UnitTests
{
    public class DataRequestServiceTests
    {
        [Fact]
        public void Validate_GetExample_Returns_Valid_Result()
        {
            var mockNetworkRequestService = new Mock<INetworkRequestService>();
            mockNetworkRequestService.Setup(x => x.SendGetRequest<Example>(It.IsAny<string>()))
                .Returns(new Example 
                { 
                    ExampleId = 1, 
                    Test = "Test123" 
                });

            var dataRequestService = new DataRequestService(mockNetworkRequestService.Object);

            var result = dataRequestService.GetExample();

            Assert.Equal(1, result.ExampleId);
            Assert.Equal("Test123", result.Test);
        }
    }
}
