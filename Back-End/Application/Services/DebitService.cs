using Application.AutoMap;
using Application.IServices;
using Application.Models.RequestModel;
using Application.Models.ResponseModel;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DebitService : BaseService, IDebitService
    {
        private readonly IDebitRepository _debitRepository;
        private readonly ICompanyRepository _companyRepository;

        public DebitService(IDebitRepository debitRepository, ICompanyRepository companyRepository)
        {
            _debitRepository = debitRepository;
            _companyRepository = companyRepository;
        }

        public async Task<DebitResponseModel> Create(DebitRequestModel debitRequest)
        {
            var debit = DebitMap.DebitRequestToDebit(debitRequest);

            if (!debit.IsValid())
                AddErrors(debit.GetErrors());

            var company = await _companyRepository.GetById(debit.CompanyId);

            if (company == null)
                AddError("Company not found");

            HandlerErrors();

            await _debitRepository.Create(debit);
            await _debitRepository.Save();

            company.CalculateReliability();

            return DebitMap.DebitToDebitResponse(debit);
        }

        public async Task<DebitResponseModel> GetById(int id)
        {
            if (id <= 0)
                AddError("Id invalid");

            HandlerErrors();

            var debit = await _debitRepository.GetById(id);

            if (debit == null)
                AddError("Debit not found");

            HandlerErrors();

            return DebitMap.DebitToDebitResponse(debit);
        }

        public async Task<List<DebitResponseModel>> GetCompanyDebits(int companyId)
        {
            if (companyId <= 0)
                AddError("The company id is Invalid");

            var debits = await _debitRepository.GetCompanyDebits(companyId);
            return debits.Select(x => DebitMap.DebitToDebitResponse(x)).ToList();
        }
    }
}
