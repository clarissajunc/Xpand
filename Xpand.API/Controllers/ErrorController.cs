using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Xpand.API.Exceptions;

namespace Xpand.API.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        [Route("/error")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var exception = context.Error;

            if (exception is ValidationException)
            {
                return Problem(
                    statusCode: (int?)HttpStatusCode.BadRequest,
                    title: "Error - Bad Request",
                    detail: exception.Message
                );
            }

            return Problem(
                statusCode: (int?)HttpStatusCode.InternalServerError,
                title: "Error",
                detail: exception.Message
            );
        }
    }
}
