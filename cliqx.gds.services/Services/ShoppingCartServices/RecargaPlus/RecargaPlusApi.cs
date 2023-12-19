using System.Net;
using System.Net.Http.Headers;
using cliqx.gds.services.Services.Base;
using Microsoft.Extensions.Configuration;
using Refit;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus
{
    public class RecargaPlusApi : ServiceBase
    {

        private readonly HttpClient _httpClient;

        public RecargaPlusApi(IConfiguration configuration, string url) : base (configuration)
        {
            var httpClient = new HttpClient(new TokenHandler(GetToken))
            {
                Timeout = TimeSpan.FromMinutes(5),
                BaseAddress = new Uri(url),
            };

            Client = RestService.For<IRecargaPlusApi>(httpClient);

        }

        public IRecargaPlusApi Client { get; }

        
    }
}