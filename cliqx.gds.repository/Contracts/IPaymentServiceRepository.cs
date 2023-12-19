using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cliqx.gds.contract.Models.PluginConfigurations;

namespace cliqx.gds.repository.Contracts;

public interface IPaymentServiceRepository : IRepositoryBase<PaymentService>
{
    public Task<List<PaymentService>> GetAll();
    public Task<PaymentService> GetById(int id);

}
