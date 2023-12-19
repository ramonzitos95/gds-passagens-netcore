using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace cliqx.gds.services.Services.ShoppingCartServices.RecargaPlus
{
    public class TokenHandler : HttpClientHandler
    {
        private readonly Func<Task<string>> _getTokenFunc;

        public TokenHandler(Func<Task<string>> getTokenFunc)
        {
            _getTokenFunc = getTokenFunc;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token = await _getTokenFunc();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}