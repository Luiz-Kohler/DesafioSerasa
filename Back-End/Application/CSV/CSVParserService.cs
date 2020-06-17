using Application.CSV.Class;
using Application.CSV.Mapper;
using CsvHelper;
using System;
using System.IO;
using System.Text;

namespace Application.CSV
{
    public class CSVParserService
    {
        public Debit_Invoice_Amount ReadCsvFileReturnDebit_Invoice_Amount(string path)
        {
            try
            {
                using (var reader = new StreamReader(path, Encoding.Default))
                using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.CreateSpecificCulture("pt-br")))
                {
                    csv.Configuration.RegisterClassMap<Debit_Invoice_AmountMap>();
                    csv.Read();
                    var records = csv.GetRecord<Debit_Invoice_Amount>();
                    return records;
                }
            }
            catch (Exception)
            {
                throw new Exception("Formatação incorreta do arquivo .csv");
            }
        }
    }
}
