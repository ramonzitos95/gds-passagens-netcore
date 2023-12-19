using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;

namespace cliqx.gds.services.Services.PaymentServices.Services.DefaultPay._Treeal.Api
{
    public interface ITreealApi
    {
        [Post("/pix/dynamic")]
        public Task<ApiResponse<_Treeal.Response>> GeneratePix(_Treeal.Request request);
    }
}