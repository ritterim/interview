using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortfolioSiteExample.Api.Services.Interfaces;
using PortfolioSiteExample.Shared.Requests;
using PortfolioSiteExample.Shared.Responses;

namespace PortfolioSiteExample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiDataController : ControllerBase
    {
        private readonly ILogger<ApiDataController> _logger;
        private readonly IDataProcessingService _dataProcessingService;

        public ApiDataController(ILogger<ApiDataController> logger,
            IDataProcessingService dataProcessingService)
        {
            _logger = logger;
            _dataProcessingService = dataProcessingService;
        }

        [HttpGet("HealthCheck")]
        public string HealthCheck()
        {
            return "PortfolioSiteExample.Api is Healthy";
        }

        [HttpPost("GetAnswers")]
        public AnswerResponse GetAnswers(AnswerRequest answerRequest)
        {
            return _dataProcessingService.GetAnswers(answerRequest);
        }
    }
}
