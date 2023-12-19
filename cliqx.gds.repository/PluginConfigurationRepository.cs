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

namespace cliqx.gds.repository;

public class PluginConfigurationRepository : RepositoryBase<Plugin>, IPluginConfigurationRepository
{
    public readonly IHttpContextAccessor _accessor;
    public PluginConfigurationRepository(DefaultContext Context, IHttpContextAccessor accessor) : base(Context)
    {
        _accessor = accessor;
    }

    public async Task<List<PluginConfiguration>> GetAll()
    {
        return await _context.PluginConfiguration
            .Include(x => x.Plugin)
            .Include(x => x.PaymentService)
            .Include(x => x.ShoppingCart)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<PluginConfiguration>> GetAllByExcludeUserId(int userId)
    {
        return await _context.PluginConfiguration
            .Include(x => x.Plugin)
            .AsNoTracking()
            .Where(x => !x.UsersPlugins.Any(up => up.UserId == userId))
            .ToListAsync();
    }

    public Task<List<PluginConfiguration>> GetAllByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<PluginConfiguration> GetById(Guid id)
    {
        return await _context.PluginConfiguration
            .Include(x => x.Plugin)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
