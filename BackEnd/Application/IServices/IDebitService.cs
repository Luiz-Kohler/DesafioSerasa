using Application.Models.RequestModel;
using Application.Models.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IDebitService
    {
        Task<DebitResponseModel> GetById(int id);
        Task<bool> CreateAmountDebits(int amount, DebitRequestModel debitRequest);
        Task<DebitResponseModel> Create(DebitRequestModel debitRequest);
        Task<List<DebitResponseModel>> GetCompanyDebits(int companyId);
    }
}
