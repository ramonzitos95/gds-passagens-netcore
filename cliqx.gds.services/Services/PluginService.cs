using AutoMapper;
using cliqx.gds.contract.Dtos;
using cliqx.gds.contract.Models.Identity;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cliqx.gds.services.Services;

public class PluginService : IPluginService
{
    private readonly IPluginRepository _pluginRepository;
    private readonly IMapper _mapper;

    public PluginService(IPluginRepository pluginRepository,
        IMapper _mapper)
    {
        this._pluginRepository = pluginRepository;
        this._mapper = _mapper;
    }

    public Task<List<Plugin>> GetAll()
    {
        return _pluginRepository.GetAll();
    }

    public Task<PluginConfiguration> GetById(Guid id)
    {
        return _pluginRepository.GetById(id);
    }

     public Task<PluginConfiguration> GetByStoreId(int storeId)
    {
        return _pluginRepository.GetByStoreId(storeId);
    }

    public async Task<Plugin> CreateAsync(CreatePlugin plugin, int userId)
    {
        var createMappedItem = _mapper.Map<Plugin>(plugin);

        createMappedItem.UserCreatedId = userId;

        _pluginRepository.Add(createMappedItem);

        if (await _pluginRepository.SaveChangesAsync())
            return createMappedItem;

        return new Plugin();
    }

    public async Task<Plugin> UpdatePlugin(Plugin plugin, int userId)
    {
        plugin.UserUpdatedId = userId;
        _pluginRepository.Update(plugin);

        if (await _pluginRepository.SaveChangesAsync())
            return plugin;

        return new Plugin();
    }

    public async Task<PluginConfiguration> DeletePluginById(Guid id)
    {
        var itemFound = await _pluginRepository.GetById(id);

        if (itemFound == null)
            throw new Exception($"Plugin não encontrado pelo ID {id}");

        _pluginRepository.Delete(itemFound);

        if (await _pluginRepository.SaveChangesAsync())
            return itemFound;

        return new PluginConfiguration();
    }
}

