using Application.CSV.Class;
using CsvHelper.Configuration;

namespace Application.CSV.Mapper
{
    internal class Debit_Invoice_AmountMap : ClassMap<Debit_Invoice_Amount>
    {
        public Debit_Invoice_AmountMap()
        {
            Map(x => x.AmountDebits).Name("AmountDebits");
            Map(x => x.AmountInvoices).Name("AmountInvoices");
        }
    }
}
