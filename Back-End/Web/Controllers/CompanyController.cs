using Application.IServices;
using Application.Models.RequestModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DesafioSerasa.Controllers
{
    [ApiController]
    [Route("companies")]
    public class CompanyController : AbstractApiController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService service)
        {
            _companyService = service;
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
        [Route("GetOrderByDescending/{currentPage}")]
        public async Task<IActionResult> GetOrderByDescending([FromRoute] int currentPage)
        {
            try
            {
                var response = await _companyService.GetOrderByDescending(currentPage);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleControllerErrors(ex);
            }
        }

        [HttpGet]
        [Route("OrderByCrescent/{currentPage}")]
        public async Task<IActionResult> GetOrderByCrescent([FromRoute] int currentPage)
        {
            try
            {
                var response = await _companyService.GetOrderByCrescent(currentPage);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return HandleControllerErrors(ex);
            }
        }
    }
}