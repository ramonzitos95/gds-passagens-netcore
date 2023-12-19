using System.Security.Claims;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace cliqx.gds.plugins.Services;

public abstract class PluginCacheService
{
    private readonly IPluginRepository _repository;
    public readonly IHttpContextAccessor _accessor;
    protected List<PluginConfiguration> _listaPlugins = new List<PluginConfiguration>();
    private readonly IMemoryCache _cache;
    private const string PluginCacheKey = "Plugins";
    private long userId = 0;

    public PluginCacheService(IPluginRepository repository, IMemoryCache cache, IHttpContextAccessor accessor)
    {

        _repository = repository;
        _cache = cache;
        _accessor = accessor;
        userId = Int32.Parse(_accessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        //userId = 2;
        PopulatePluginsAsync().Wait();
    }

    private async Task PopulatePluginsAsync()
    {
        _listaPlugins = await GetAllPluginsAsync();
    }

    private async Task<List<PluginConfiguration>> GetAllPluginsAsync()
    {
        try
        {
            if (!_cache.TryGetValue(PluginCacheKey, out List<PluginConfiguration> lista))
            {

                var retorno = await _repository.GetAllByUserId(userId);

                if (retorno == null || retorno.Count == 0)
                    throw new Exception($"Nenhum plugin localizado para o usuário autenticado. UserId: {userId}");

                //_cache.Set(PluginCacheKey, retorno, TimeSpan.FromMinutes(10));
                _cache.Set(PluginCacheKey, retorno, TimeSpan.FromSeconds(10));

                return retorno;
            }

            return lista;
        }
        catch (Exception e)
        {
            Console.WriteLine($"GetAllPluginsAsync: {e.Message}");
            Console.WriteLine($"{e.StackTrace}");
            throw new Exception($"Nenhum plugin localizado para o usuário autenticado. Error: " + e.Message);
        }

    }

}
