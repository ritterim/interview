using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PortfolioSiteExample.Api.Services;
using PortfolioSiteExample.Data.Models;
using PortfolioSiteExample.Shared;
using System;
using Xunit;

namespace PortfolioSiteExample.UnitTests
{
    public class DataProcessingServiceTests
    {
        [Fact]
        public void Validate_GetExample_Returns_Valid_Result()
        {
            var settings = Options.Create(
                new Settings
                {
                    ApiBaseUrl = "http://test",
                    DataFileName = "test.json",
                }
            );

            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new Context(options);

            var dataProcessingService = new DataProcessingService(context, settings);

            var result = dataProcessingService.GetExample();

            Assert.Equal(1, result.ExampleId);
            Assert.Equal("Test123", result.Test);
        }
    }
}
