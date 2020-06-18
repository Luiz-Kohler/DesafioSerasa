using Application.CSV;
using Application.IServices;
using Application.Models.RequestModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace Web.Controllers
{
    [ApiController]
    [Route("companies")]
    [EnableCors("MyPolicy")]
    public class CompanyController : AbstractApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IDebitService _debitService;
        private readonly IInvoiceService _invoiceService;

        public CompanyController(ICompanyService companyService, IDebitService debitService, IInvoiceService invoiceService)
        {
            _companyService = companyService;
            _debitService = debitService;
            _invoiceService = invoiceService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyRequestModel model)
        {
            try
            {
                var response = await _companyService.Create(model);
                return Created("companies", response);
            }
            catch (Exception ex)
            {
                return HandleControllerErrors(ex);
            }
        }

        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> CreateCategoriesAndDebits([FromRoute] int id)
        {
            try
            {
                var responseCsv = CSVParserService.ReadCsvFileReturnDebit_Invoice_Amount(Request.Form.Files[0]);

                var debit = new DebitRequestModel()
                {
                    CompanyId = id
                };

                var invoice = new InvoiceRequestModel()
                {
                    CompanyId = id
                };

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _invoiceService.CreateAmountInvoice(responseCsv.AmountInvoices, invoice);
                    await _debitService.CreateAmountDebits(responseCsv.AmountDebits, debit);
                    scope.Complete();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleControllerErrors(ex);
            }

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var response = await _companyService.GetById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleControllerErrors(ex);
            }
        }

        [HttpGet]
        [Route("get-order-by-descending")]
        public async Task<IActionResult> GetOrderByDescending([FromRoute] int currentPage)
        {
            try
            {
                var response = await _companyService.GetOrderByDescending();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleControllerErrors(ex);
            }
        }
    }
}