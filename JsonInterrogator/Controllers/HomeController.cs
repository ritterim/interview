using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JsonInterrogator.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonInterrogator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<HomeController> _logger;

        private IEnumerable<Person> People;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _environment = environment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            string filePath = Path.Combine(_environment.WebRootPath, "data.json");
            using (StreamReader file = System.IO.File.OpenText(filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                People = (IEnumerable<Person>)serializer.Deserialize(file, typeof(IEnumerable<Person>));
            }
            var viewModel = new AppViewModel(People);
            return View(viewModel);
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
