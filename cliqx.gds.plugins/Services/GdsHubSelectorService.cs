using System.Reflection;
using cliqx.gds.contract;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.plugins.Services;
using cliqx.gds.repository.Contracts;
using cliqx.gds.services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace cliqx.gds.plugins;

public class GdsHubSelectorService : PluginCacheService
{
    private readonly IConfiguration _configuration;
    private IContractBase _contractBase;
    private readonly ILogger<IContractBase> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public GdsHubSelectorService(
        IConfiguration configuration,
        ILogger<IContractBase> logger,
        IPluginRepository repository,
        IMemoryCache cache,
        IHttpClientFactory httpClientFactory,
        IHttpContextAccessor _accessor
    ) : base(repository, cache,_accessor)
    {
        _configuration = configuration;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IContractBase> GetHubPlugin(Guid pluginId)
    {
        if (_listaPlugins == null)
            throw new Exception("Nenhum plugin configurado para o cliente autenticado");

        var plugin = _listaPlugins.FirstOrDefault(x => x.Id == pluginId);

        if (plugin == null)
            return null;

        Type pluginType = GetPluginType(plugin);

        if (pluginType == null || !typeof(IContractBase).IsAssignableFrom(pluginType))
            throw new Exception("Tipo de plugin inválido ou não implementa a interface IContractBase.");
        

        var constructor = pluginType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
        null, new[] {
            typeof(IConfiguration),
            typeof(ILogger<IContractBase>),
            typeof(Func<HttpClient>),
            typeof(PluginConfiguration),
            typeof(IShoppingCartService)
        }, null);

        if (constructor == null)
            throw new Exception($"Constructor não encontrado para a classe do plugin. {plugin.Plugin.Name}");
        

        var httpClientFactoryDelegate = (Func<HttpClient>)CreateHttpClient;

        var parameters = new object[] {
        _configuration,
        _logger,
        httpClientFactoryDelegate,
        plugin,
        new ShoppingCartService(_configuration,plugin)
        };

        _contractBase = (IContractBase)constructor.Invoke(parameters);

        return _contractBase;
    }

    public async Task<PluginConfiguration> GetHubPluginByGuid(Guid pluginId)
    {
        if (_listaPlugins == null)
            throw new Exception("Nenhum plugin configurado para o cliente autenticado");

        return _listaPlugins.FirstOrDefault(x => x.Id == pluginId);
    }

    private HttpClient CreateHttpClient()
    {
        return _httpClientFactory.CreateClient();
    }

    private Type GetPluginType(PluginConfiguration plugin)
    {
        string pluginClassName = "cliqx.gds.plugins." + plugin.Plugin.Name + "Plugin";

        Type pluginType = Type.GetType(pluginClassName);

        return pluginType;
    }
}
