using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        Task<ICollection<Invoice>> GetCompanyInvoices(int companyId);
    }
}
