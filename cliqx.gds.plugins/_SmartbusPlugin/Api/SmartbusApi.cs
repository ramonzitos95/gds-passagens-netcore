using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using cliqx.gds.plugins._SmartbusPlugin.Models.Request;
using cliqx.gds.plugins._SmartbusPlugin.Models.Response;
using Newtonsoft.Json;
using Refit;
using RestSharp;

namespace cliqx.gds.plugins._SmartbusPlugin.Api
{
    public class SmartbusApi
    {

        private readonly HttpClient _httpClient;
        public SmartbusApi(ApiUrlsBase apiUrls, AuthModel auth)
        {
            var modelRequest = new AuthRequest() { UserName = auth.UserName, Password = auth.Password };
            var httpClient = new HttpClient(new TokenHandler(() => GetToken(apiUrls.UrlAuth, modelRequest)))
            {
                Timeout = TimeSpan.FromSeconds(35),
                BaseAddress = new Uri(apiUrls.BaseUrl),

            };

            Client = RestService.For<ISmartbusApi>(httpClient);
        }

        public async Task<AuthResponse> GetToken(string urlBase, AuthRequest auth)
        {
            var client = new RestClient(urlBase);
            var request = new RestRequest();
            request.Method = Method.Post;
            var parameters = new Dictionary<string, string>
            {
                { "Username", auth.UserName },
                { "password", auth.Password },
                { "grant_type", auth.GrantType }
            };

            var encodedParameters = string.Join("&", parameters.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/x-www-form-urlencoded", encodedParameters, RestSharp.ParameterType.RequestBody);

            try
            {
                var response = client.Execute<AuthResponse>(request);

                if (response.StatusCode.Equals(HttpStatusCode.OK) && response.Data != null)
                {
                    var resp = JsonConvert.DeserializeObject<AuthResponse>(response.Content);
                    return resp;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Stack: " + ex.StackTrace);
                Console.WriteLine("Token inv�lido!");
                return null;
            }
        }

        public ISmartbusApi Client { get; }



    }
}