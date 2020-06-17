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
    public class InvoiceService : BaseService, IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICompanyRepository _companyRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository, ICompanyRepository companyRepository)
        {
            _invoiceRepository = invoiceRepository;
            _companyRepository = companyRepository;
        }

        public async Task<InvoiceResponseModel> Create(InvoiceRequestModel invoiceRequest)
        {
            var invoice = InvoiceMap.InvoiceRequestToInvoice(invoiceRequest);

            if (!invoice.IsValid())
                AddErrors(invoice.GetErrors());

            HandlerErrors();

            var company = await _companyRepository.GetById(invoice.CompanyId);

            if (company == null)
                AddError("Company not found");

            HandlerErrors();

            await _invoiceRepository.Create(invoice);
            await _invoiceRepository.Save();

            company.CalculateReliability();

            return InvoiceMap.InvoiceToInvoiceResponse(invoice);
        }

        public async Task<InvoiceResponseModel> GetById(int id)
        {
            if (id <= 0)
                AddError("Id invalid");

            HandlerErrors();

            var invoice = await _invoiceRepository.GetById(id);

            if (invoice == null)
                AddError("Invoice not found");

            HandlerErrors();

            return InvoiceMap.InvoiceToInvoiceResponse(invoice);
        }

        public async Task<List<InvoiceResponseModel>> GetCompanyInvoices(int companyId)
        {
            if (companyId <= 0)
                AddError("The company id is Invalid");

            var invoices = await _invoiceRepository.GetCompanyInvoices(companyId);
            return invoices.Select(x => InvoiceMap.InvoiceToInvoiceResponse(x)).ToList();
        }
    }
}
