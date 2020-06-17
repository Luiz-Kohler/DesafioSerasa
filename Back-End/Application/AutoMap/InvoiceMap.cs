using Application.Models.RequestModel;
using Application.Models.ResponseModel;
using Domain.Entities;
using Utils;

namespace Application.AutoMap
{
    public static class InvoiceMap
    {
        public static Invoice InvoiceRequestToInvoice(InvoiceRequestModel invoiceRequest)
        {
            return AutoMapperFunc.ChangeValues<InvoiceRequestModel, Invoice>(invoiceRequest);
        }

        public static InvoiceResponseModel InvoiceToInvoiceResponse(Invoice invoice)
        {
            return AutoMapperFunc.ChangeValues<Invoice, InvoiceResponseModel>(invoice);
        }
    }
}
