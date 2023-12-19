using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using cliqx.gds.contract;
using cliqx.gds.plugins;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace cliqx.gds.api.Controllers;

public class MyBaseController : Controller
{
    protected readonly IConfiguration _configuration;
    protected IContractBase Hub { get; private set; }
    protected readonly GdsHubSelectorService _hubSelector;

    public MyBaseController(IConfiguration configuration, GdsHubSelectorService hubSelector)
    {
        _configuration = configuration;
        _hubSelector = hubSelector;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        var endpoint = context.ActionDescriptor.DisplayName;
        var isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
        var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();
        var parameters = context.ActionArguments;
        var httpMethod = context.HttpContext.Request.Method;

        if (context.HttpContext.User.Identity is ClaimsIdentity claimsIdentity)
        {
            var userIdClaim = claimsIdentity.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;
            }
        }

        Console.WriteLine("ENTREI NO ON ACTION EXECUTING");
        await next();
    }

    protected Dictionary<string, string> DecodeJsonDictionary(string jsonDictionary)
    {
        jsonDictionary ??= "";
        try
        {
            var b64b = Convert.FromBase64String(jsonDictionary);
            var s = Encoding.UTF8.GetString(b64b);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(s);
        }
        catch
        {
            return new Dictionary<string, string>();
        }
    }

}
