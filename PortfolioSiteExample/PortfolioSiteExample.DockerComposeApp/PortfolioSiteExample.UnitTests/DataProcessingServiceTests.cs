using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PortfolioSiteExample.Api.Services;
using PortfolioSiteExample.Data.Models;
using PortfolioSiteExample.Shared;
using PortfolioSiteExample.Shared.Enums;
using System;
using System.Collections.Generic;
using Xunit;
using Record = PortfolioSiteExample.Data.Models.Record;

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
                    DataFileName = "data.json",
                }
            );

            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new Context(options);

            context.Records.Add(new Record
            {
                Id = "1",
                Age = 51
            });

            var dataProcessingService = new DataProcessingService(context, settings);

            var result = dataProcessingService.GetAnswers(new Shared.Requests.AnswerRequest
            {
                Question = new List<Question>() { Question.OverAge50}
            });

            Assert.Equal("1", result.Answer[Question.OverAge50]);
        }
    }
}
