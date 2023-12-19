using cliqx.gds.contract.Extensions;
using cliqx.gds.contract.Models;
using cliqx.gds.contract.Models.PluginConfigurations;
using cliqx.gds.contract.Util;
using cliqx.gds.repository.Contexts;
using cliqx.gds.repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace cliqx.gds.repository
{
    public class PaymentServiceRepository : RepositoryBase<PaymentService>, IPaymentServiceRepository
    {
        public PaymentServiceRepository(DefaultContext Context) : base(Context)
        {
        }

        public async Task<List<PaymentService>> GetAll()
        {
            return await _context.PaymentServices
            .AsQueryable()
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<PaymentService> GetById(int id)
        {
            return await _context.PaymentServices
            .AsQueryable()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}