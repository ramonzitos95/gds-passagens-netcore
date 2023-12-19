
using cliqx.gds.plugins._RjConsultoresPlugin.Api;
using Refit;

namespace cliqx.gds.plugins._RjConsultoresPlugin.Api
{
	public class RjConsultoresApi
	{
		private readonly HttpClient _httpClient;
        public IRjConsultoresApi Client { get; }
        public RjConsultoresApi(ApiUrlsBase apiUrls, AuthModel auth, HttpClient httpClient)
        {
            
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(apiUrls.BaseUrl);
            _httpClient.Timeout = TimeSpan.FromMinutes(5);
            _httpClient.DefaultRequestHeaders.Add("Authorization", auth.Authorization);
            _httpClient.DefaultRequestHeaders.Add("x-tenant-id", auth.TenantId);

            Client = RestService.For<IRjConsultoresApi>(_httpClient);
        }
    }
}
