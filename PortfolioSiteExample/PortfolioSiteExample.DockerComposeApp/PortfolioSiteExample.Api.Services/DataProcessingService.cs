using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PortfolioSiteExample.Api.Services.Interfaces;
using PortfolioSiteExample.Data.Models;
using PortfolioSiteExample.Shared;
using System.Linq;

namespace PortfolioSiteExample.Api.Services
{
    public class DataProcessingService : IDataProcessingService
    {
        private readonly Context _context;
        private readonly Settings _settings;

        public DataProcessingService(Context context, IOptions<Settings> settings)
        {
            _context = context;
            _settings = settings.Value;
        }

        public Example GetExample()
        {
            Example result;

            result = _context.Examples.FirstOrDefault(x => x.ExampleId == 1);
            if (result == null)
            {
                result = JsonConvert.DeserializeObject<Example>("{\"ExampleId\": 1, \"Test\": \"Test123\"}");
                _context.Examples.Add(result);
                _context.SaveChanges();
            }            

            return result;
        }
    }
}
