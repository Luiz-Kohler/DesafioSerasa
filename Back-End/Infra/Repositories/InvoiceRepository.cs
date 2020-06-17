using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Infra.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(MainContext context) : base(context)
        {
        }

        public async Task<ICollection<Invoice>> GetCompanyInvoices(int companyId)
        {
            return await _dbSet.Where(x => x.CompanyId == companyId).ToListAsync();
        }
    }
}
