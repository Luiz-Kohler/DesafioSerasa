using Application.CSV.Class;
using Application.CSV.Mapper;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Application.CSV
{
    public static class CSVParserService
    {
        private static Debit_Invoice_Amount Get_Debit_Invoice_Amount(MemoryStream stream)
        {
            try
            {
                using (var reader = new StreamReader(stream))
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

        public static Debit_Invoice_Amount ReadCsvFileReturnDebit_Invoice_Amount(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                file.CopyTo(target);
                target.Seek(0, SeekOrigin.Begin);
                return Get_Debit_Invoice_Amount(target);
            }
        }
    }

}

