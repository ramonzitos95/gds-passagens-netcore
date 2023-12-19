using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.PluginConfigurations;
using Refit;

namespace cliqx.gds.services.Services.PaymentServices.Services.DefaultPay._Treeal.Api
{
    public class TreealApi
    {

        private readonly HttpClient _httpClient;
        public ITreealApi Client { get; }

        public TreealApi(ApiUrlsBase service, AuthModel auth)
        {
            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30);
            httpClient.BaseAddress = new Uri(service.UrlBaseTreeal.TrimEnd('/'));
            httpClient.DefaultRequestHeaders.Add("x-access-token", auth.TokenTreeal);

            Client = RestService.For<ITreealApi>(httpClient);


        }

    }
}