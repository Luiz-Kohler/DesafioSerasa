using Application.CSV;
using Application.CSV.Class;
using NUnit.Framework;

namespace UnitTests.Application.Csv
{
    public class CSVParserServiceTest
    {
        [Test]
        public void ShouldReturnDebit_Invoice_Amount()
        {
            var expectedResponse = new Debit_Invoice_Amount()
            {
                AmountInvoices = 3,
                AmountDebits = 1
            };

            var service = new CSVParserService();

            var path = @"C:\Users\Luiz\Excel\DesafioSerasa.csv";

            var response = service.ReadCsvFileReturnDebit_Invoice_Amount(path);

            Assert.AreEqual(expectedResponse.AmountDebits, response.AmountDebits);
            Assert.AreEqual(expectedResponse.AmountInvoices, response.AmountInvoices);
        }
    }
}
