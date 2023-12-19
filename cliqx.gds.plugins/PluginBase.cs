using System;
using System.Net.Http;
using System.Threading.Tasks;
using cliqx.gds.contract;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace cliqx.gds.plugins;

public abstract class PluginBase
{
    protected readonly IConfiguration Configuration;
    protected readonly ILogger<IContractBase> Logger;
    protected readonly PluginConfiguration PluginConfiguration;
    protected readonly Func<HttpClient> NewHttpClient;
    protected readonly IShoppingCartService ShoppingCartService;
    protected PluginBase(
        IConfiguration configuration
        , ILogger<IContractBase> logger
        , Func<HttpClient> newHttpClient
        , PluginConfiguration pluginConfiguration
        , IShoppingCartService shoppingCartService)
    {
        Configuration = configuration;
        Logger = logger;
        NewHttpClient = newHttpClient;
        PluginConfiguration = pluginConfiguration;
        ShoppingCartService = shoppingCartService;
    }

}
