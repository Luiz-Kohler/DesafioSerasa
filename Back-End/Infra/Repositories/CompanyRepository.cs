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
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(MainContext context) : base(context)
        {

        }

        public async Task<bool> CompanyExists(string companyName)
        {
            return await _dbSet.AnyAsync(x => x.Name == companyName);
        }

        public async Task<List<Company>> GetOrderByDescending()
        {
            return await _dbSet.OrderByDescending(x => x.Reliability).ToListAsync();
        }
    }
}
