using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<List<Company>> GetOrderByDescending(int currentPage = 0);
        Task<List<Company>> GetOrderByCrescent(int currentPage = 0);
        Task<bool> CompanyExists(string companyName);
    }
}
