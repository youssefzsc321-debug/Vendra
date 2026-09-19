using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vendra.Core.Common;

namespace Vendra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult Success<T>(T Data, string? message = null)
        {
            var response = new APIResponse<T>(StatusCode: 200, IsSuccess: true, Data: Data, ErrorMessage: message);
            return Ok(response);
        }

        protected IActionResult Created<T>(string actionName, object value, T Data, string? message = null)
        {
            var response = new APIResponse<T>(StatusCode: 201, IsSuccess: true, Data: Data, ErrorMessage: message);
            return CreatedAtAction(actionName, value, response);
        }
        protected IActionResult BadRequestResponse(string message, List<string>? errors = null)
        {
            var response = new APIResponse<object>(StatusCode: 400, IsSuccess: false, ErrorMessage: message, Errors: errors);
            return BadRequest(response);

        }
        protected IActionResult NotFoundResponse(string message)
        {
            var response = new APIResponse<object>(StatusCode: 404, IsSuccess: false, ErrorMessage: message);
            return NotFound(response);

        }
    }
}
