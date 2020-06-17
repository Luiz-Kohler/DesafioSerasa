using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDebitRepository : IGenericRepository<Debit>
    {
        Task<ICollection<Debit>> GetCompanyDebits(int companyId);
    }
}
