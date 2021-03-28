using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortfolioSiteExample.Frontend.Models;
using Microsoft.Extensions.Options;
using PortfolioSiteExample.Shared;
using PortfolioSiteExample.Frontend.Services.Interfaces;

namespace PortfolioSiteExample.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Settings _settings;
        private readonly IDataRequestService _dataRequestService;

        public HomeController(ILogger<HomeController> logger, 
            IOptions<Settings> settings, 
            IDataRequestService dataRequestService)
        {
            _logger = logger;
            _settings = settings.Value;
            _dataRequestService = dataRequestService;
        }

        public IActionResult Index()
        {
            ViewBag.Test = _dataRequestService.GetExample().Test;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
