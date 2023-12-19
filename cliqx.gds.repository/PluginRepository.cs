using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.repository.Contexts;
using cliqx.gds.repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace cliqx.gds.repository;

public class PluginRepository : RepositoryBase<Plugin>, IPluginRepository
{
    protected readonly IConfiguration Configuration;
    public PluginRepository(DefaultContext Context, IConfiguration configuration) : base(Context)
    {
        Configuration = configuration;
    }

    public async Task<List<Plugin>> GetAll()
    {
        return await _context.Plugin
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<PluginConfiguration>> GetAllByUserId(long userId)
    {

        IQueryable<UserPlugin> query = _context.UsersPlugins
        .Include(x => x.PluginConfigurations)
            .ThenInclude(x => x.Plugin)
        .Include(x => x.PluginConfigurations)
            .ThenInclude(x => x.PaymentService)
        .Where(x => x.UserId == userId && x.PluginConfigurations.IsActive)
        .AsNoTracking();

        query = query.OrderBy(x => x.Index);

        var retorno = await query.ToListAsync();


        return retorno.Select(x => x.PluginConfigurations).ToList();
    }

    public async Task<PluginConfiguration> GetById(Guid id)
    {
        IQueryable<PluginConfiguration> query = _context
            .PluginConfiguration
            .Include(x => x.Plugin)
            .AsNoTracking()
            .Where(x => x.Id == id);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<PluginConfiguration> GetByStoreId(int storeId)
    {
        IQueryable<PluginConfiguration> query = _context
           .PluginConfiguration
           .Include(x => x.Plugin)
           .AsNoTracking()
           .Where(x => x.StoreId == storeId);

        return await query.FirstOrDefaultAsync();
    }
}
