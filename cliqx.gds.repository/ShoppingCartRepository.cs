using cliqx.gds.contract.Extensions;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.contract.Util;
using cliqx.gds.repository.Contexts;
using cliqx.gds.repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace cliqx.gds.repository
{
    public class ShoppingCartRepository : RepositoryBase<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(DefaultContext Context) : base(Context)
        {
        }

        public async Task<List<ShoppingCart>> GetAll()
        {
            return await _context.ShoppingCartServices
            .AsQueryable()
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<ShoppingCart> GetById(int id)
        {
            return await _context.ShoppingCartServices
            .AsQueryable()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}