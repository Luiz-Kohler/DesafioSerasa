using Application.Models.RequestModel;
using Application.Models.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IInvoiceService
    {
        Task<InvoiceResponseModel> GetById(int id);
        Task<InvoiceResponseModel> Create(InvoiceRequestModel invoiceRequest);
        Task<bool> CreateAmountInvoice(int amount, InvoiceRequestModel invoiceRequest);
        Task<List<InvoiceResponseModel>> GetCompanyInvoices(int companyId);
    }
}
