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
    public class CompanyService : BaseService, ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        public async Task<CompanyResponseModel> Create(CompanyRequestModel companyRequest)
        {
            var company = CompanyMap.CompanyRequestToCompany(companyRequest);

            company.FormatProps();

            if (!company.IsValid())
                AddErrors(company.GetErrors());

            if (await _companyRepository.CompanyExists(company.Name))
                AddError("This company name already exists");

            HandlerErrors();

            await _companyRepository.Create(company);
            await _companyRepository.Save();

            return CompanyMap.CompanyToCompanyResponse(company);
        }

        public async Task<CompanyResponseModel> GetById(int id)
        {
            if (id <= 0)
                AddError("Id invalid");

            HandlerErrors();

            var company = await _companyRepository.GetById(id);

            if (company == null)
                AddError("Company not found");

            HandlerErrors();

            return CompanyMap.CompanyToCompanyResponse(company);
        }

        public async Task<List<CompanyResponseModel>> GetOrderByCrescent(int currentPage = 0)
        {
            var companies = await _companyRepository.GetOrderByCrescent(currentPage);
            return companies.Select(x => CompanyMap.CompanyToCompanyResponse(x)).ToList();
        }

        public async Task<List<CompanyResponseModel>> GetOrderByDescending(int currentPage = 0)
        {
            var companies = await _companyRepository.GetOrderByDescending(currentPage);
            return companies.Select(x => CompanyMap.CompanyToCompanyResponse(x)).ToList();
        }
    }
}
