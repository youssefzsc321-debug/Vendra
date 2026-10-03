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
            var response = new APIResponse<T>(statusCode: 200, isSuccess: true, data: Data, errorMessage: message);
            return Ok(response);
        }
        

        protected IActionResult Created<T>(string actionName, object value, T Data, string? message = null)
        {
            var response = new APIResponse<T>(statusCode: 201, isSuccess: true, data: Data, errorMessage: message);
            return CreatedAtAction(actionName, value, response);
        }
        protected IActionResult BadRequestResponse(string message, List<string>? errors = null)
        {
            var response = new APIResponse<object>(statusCode: 400, isSuccess: false, errorMessage: message, errors: errors);
            return BadRequest(response);

        }
        protected IActionResult UnauthorizedResponse(string? message=null)
        {
            var response = new APIResponse<object>(statusCode: 401, isSuccess: false, errorMessage: message);
            return Unauthorized(response);
        }
        protected IActionResult NotFoundResponse(string message)
        {
            var response = new APIResponse<object>(statusCode: 404, isSuccess: false, errorMessage: message);
            return NotFound(response);

        }
    }
}
