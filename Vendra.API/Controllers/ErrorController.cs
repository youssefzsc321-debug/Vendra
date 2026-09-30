using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vendra.API.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseController
    {
        public IActionResult HandleError(int statusCode)
        {
            return statusCode switch
            {
                404 => NotFoundResponse("The requested endpoint was not found."),
                401 => BadRequestResponse("You are not authorized to access this resource."),
                _ => BadRequestResponse($"An unexpected error occurred ({statusCode}).")
            };

        }
    }
}


