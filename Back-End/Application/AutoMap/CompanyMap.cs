using Application.Models.RequestModel;
using Application.Models.ResponseModel;
using Domain.Entities;
using System.Linq;
using Utils;

namespace Application.AutoMap
{
    public static class CompanyMap
    {
        public static Company CompanyRequestToCompany(CompanyRequestModel companyRequest)
        {
            return AutoMapperFunc.ChangeValues<CompanyRequestModel, Company>(companyRequest);
        }

        public static CompanyResponseModel CompanyToCompanyResponse(Company company)
        {
            var companyModel = AutoMapperFunc.ChangeValues<Company, CompanyResponseModel>(company);

            var debits = company.Debits.Select(x => DebitMap.DebitToDebitResponse(x)).ToList();
            companyModel.SetDebits(debits);

            var invoices = company.Invoices.Select(x => InvoiceMap.InvoiceToInvoiceResponse(x)).ToList();
            companyModel.SetInvoices(invoices);

            return companyModel;
        }
    }
}
