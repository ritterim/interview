using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PortfolioSiteExample.Frontend.Models;
using Microsoft.Extensions.Options;
using PortfolioSiteExample.Shared;
using PortfolioSiteExample.Frontend.Services.Interfaces;
using PortfolioSiteExample.Shared.Enums;
using PortfolioSiteExample.Shared.Requests;
using PortfolioSiteExample.Shared.Responses;

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
            var answerRequest = new AnswerRequest();
            answerRequest.Question.Add(Question.OverAge50);
            answerRequest.Question.Add(Question.LastRegisteredActive);
            answerRequest.Question.Add(Question.DistinctFavoriteFruitCounts);
            answerRequest.Question.Add(Question.MostCommonEyeColor);
            answerRequest.Question.Add(Question.TotalBalance);
            answerRequest.Question.Add(Question.UniqueIndividual);
            var answerResponse = _dataRequestService.GetAnswers(answerRequest);
            if (answerResponse == null)
            {
                answerResponse = new AnswerResponse();
            }

            return View(answerResponse);
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
