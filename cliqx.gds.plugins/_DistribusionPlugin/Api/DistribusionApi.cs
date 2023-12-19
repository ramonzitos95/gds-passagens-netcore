using Refit;

namespace distribusion.api.client.Api
{
    public class DistribusionApi
    {
        private readonly HttpClient _httpClient;

        public DistribusionApi(HttpClient httpClient, string url, string token)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromMinutes(5);
            _httpClient.BaseAddress = new Uri(url.TrimEnd('/'));
            _httpClient.DefaultRequestHeaders.Add("Api-Key", token);
            Client = RestService.For<IDistribusionApi>(_httpClient);
        }

        public IDistribusionApi Client { get; }
    }
}