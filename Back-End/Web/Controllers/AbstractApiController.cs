using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Utils.Exceptions;

namespace Web.Controllers
{
    public abstract class AbstractApiController : ControllerBase
    {
        protected IActionResult HandleControllerErrors(Exception ex)
        {
            if (ex is BadRequestException)
                return BadRequest(ex as BadRequestException);

            return RaiseInternalServerError(ex);
        }
        private IActionResult BadRequest(BadRequestException ex)
        {
            return BadRequest(ex.Errors);
        }
        private IActionResult RaiseInternalServerError(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}