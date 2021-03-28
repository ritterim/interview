using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortfolioSiteExample.Api.Services.Interfaces;
using PortfolioSiteExample.Data.Models;

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

        [HttpGet("GetExample")]
        public Example GetExample()
        {
            return _dataProcessingService.GetExample();
        }
    }
}
