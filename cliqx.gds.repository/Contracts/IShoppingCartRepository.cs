using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.PluginConfigurations;

namespace cliqx.gds.repository.Contracts;

public interface IShoppingCartRepository : IRepositoryBase<ShoppingCart>
{
    public Task<List<ShoppingCart>> GetAll();
    public Task<ShoppingCart> GetById(int id);
}
