using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using short_url.BusinessLogic;
using short_url.Consts;
using short_url.Models;


namespace short_url.Controllers
{
    [Route("{*url:regex(^(?!api).*$)}")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private readonly RedirectPathBL _businesslogic;

        public RedirectController(RedirectPathContext context, IConfiguration configuration)
        {
            _businesslogic = new RedirectPathBL(context, configuration);
        }

        [HttpGet]
        public IActionResult GetRedirect()
        {
            try
            {
                string path = ControllerContext.HttpContext.Request.Path.ToString();
                RedirectPath redirectPath = _businesslogic.GetRedirectPaths(path);

                if (redirectPath == null)
                {
                    return NotFound(Messages.redirectNotFound);
                }

                if (_businesslogic.ValidateDestination(redirectPath.destination))
                {
                    return Redirect(redirectPath.destination);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest);
                }
            }
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


    }
}
