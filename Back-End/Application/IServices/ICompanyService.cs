using Application.Models.RequestModel;
using Application.Models.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface ICompanyService
    {
        Task<List<CompanyResponseModel>> GetOrderByDescending();
        Task<CompanyResponseModel> GetById(int id);
        Task<CompanyResponseModel> Create(CompanyRequestModel companyRequest);
    }
}
