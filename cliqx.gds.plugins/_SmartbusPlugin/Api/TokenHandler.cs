using System.Net.Http.Headers;
using cliqx.gds.plugins._SmartbusPlugin.Models.Response;
using Microsoft.Extensions.Logging;

namespace cliqx.gds.plugins._SmartbusPlugin.Api
{
    public class TokenHandler : HttpClientHandler
    {
        private readonly Func<Task<AuthResponse>> _getTokenFunc;

        public TokenHandler(Func<Task<AuthResponse>> getTokenFunc)
        {
            _getTokenFunc = getTokenFunc;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _getTokenFunc();
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

                return await base.SendAsync(request, cancellationToken);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro na autenticação: " + ex.GetBaseException().Message);
                return new HttpResponseMessage();
            }
          
        }

        
    }
}