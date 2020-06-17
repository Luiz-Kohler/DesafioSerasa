using System.Collections.Generic;

namespace Application.Models.ResponseModel
{
    public class CompanyResponseModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Reliability { get; private set; }
        public virtual List<InvoiceResponseModel> Invoices { get; private set; }
        public virtual List<DebitResponseModel> Debits { get; private set; }

        public CompanyResponseModel(int id, string name, int reliability)
        {
            Id = id;
            Name = name;
            Reliability = reliability;
            Invoices = new List<InvoiceResponseModel>();
            Debits = new List<DebitResponseModel>();
        }

        public void SetInvoices(List<InvoiceResponseModel> invoices)
        {
            this.Invoices = invoices;
        }
        public void SetDebits(List<DebitResponseModel> debits)
        {
            this.Debits = debits;
        }
    }
}
