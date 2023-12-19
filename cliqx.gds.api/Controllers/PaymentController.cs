using cliqx.gds.plugins;
using cliqx.gds.services.Services.PaymentServices.Services;
using cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay;
using cliqx.gds.services.Services.PaymentServices.Services.FacilitaPay.Models;
using cliqx.gds.services.Services.PaymentServices.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cliqx.gds.api.Controllers;

[ApiController]
[Authorize("Bearer", Roles = "admin")]
[Route("api/[controller]/[action]")]
public class PaymentController : ControllerBase
{
    public readonly FacilitaPayApi _facilitaPayApi;
    private readonly GdsHubSelectorService _hubSelector;

    public PaymentController(IConfiguration configuration,
        GdsHubSelectorService hubSelector)
    {
        _hubSelector = hubSelector;
    }

    [HttpPost]
    public async Task<ActionResult<PagamentoCartaoResponse>> GeneratePaymentTransparent([FromBody] PaymentPayloadRequest data)
    {
        try
        {
            if (data == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new PagamentoCartaoResponse() { Message = "Objeto n�o informado para emiss�o do pagamento por cart�o", Status = "400" });
            }

            var plugin = await _hubSelector.GetHubPluginByGuid(data.PluginId);
            if (plugin == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new PagamentoCartaoResponse()
                {
                    Message = "Plugin n�o encontrado!"
                });
            }

            var servicePayment = new PaymentServiceImp(plugin);

            var result = await servicePayment.GerarCheckoutTransparente(data);

            if (result.Status != "200")
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }

            return Ok(result);
        }
        catch (System.Exception)
        {
            return BadRequest();
        }
    }





}
