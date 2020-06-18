using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<List<Company>> GetOrderByDescending();
        Task<bool> CompanyExists(string companyName);
    }
}
