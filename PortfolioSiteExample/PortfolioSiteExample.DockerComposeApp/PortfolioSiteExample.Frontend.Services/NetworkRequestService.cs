using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PortfolioSiteExample.Frontend.Services.Interfaces;
using PortfolioSiteExample.Shared;
using PortfolioSiteExample.Shared.Requests;
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

        public T SendPostRequest<T>(string endpoint, AnswerRequest answerRequest)
        {
            T result = default(T);

            var client = new RestClient(_settings.ApiBaseUrl);
            var request = new RestRequest(endpoint, DataFormat.Json);
            request.AddJsonBody(answerRequest);
            var response = client.Post(request);

            if (response.IsSuccessful)
            {
                result = JsonConvert.DeserializeObject<T>(response.Content);
            }

            return result;
        }
    }
}
