using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PortfolioSiteExample.Frontend.Services.Interfaces;
using PortfolioSiteExample.Shared;
using RestSharp;

namespace PortfolioSiteExample.Frontend.Services
{
    public class NetworkRequestService : INetworkRequestService
    {
        private readonly Settings _settings;

        public NetworkRequestService(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        public T SendGetRequest<T>(string endpoint)
        {
            T result = default(T);

            var client = new RestClient(_settings.ApiBaseUrl);
            var request = new RestRequest(endpoint, DataFormat.Json);
            var response = client.Get(request);

            if (response.IsSuccessful)
            {
                result = JsonConvert.DeserializeObject<T>(response.Content);
            }

            return result;
        }
    }
}
