using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using short_url.BusinessLogic;
using short_url.Models;
using System.Net;

namespace short_url.Controllers
{
    [Route("{*url:regex(^(?!api).*$)}")]
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private readonly RedirectPathBL _businesslogic;

        public RedirectController(RedirectPathContext context)
        {
             _businesslogic = new RedirectPathBL(context);
        }

        [HttpGet]
        public IActionResult  GetRedirect()
        {
            
            
            string path = ControllerContext.HttpContext.Request.Path.ToString();
            RedirectPath redirectPath = _businesslogic.GetRedirectPaths(path);

            if(redirectPath == null){
                return  NotFound("cannot find the requested path");
            }

            return Redirect(redirectPath.destination);
        }


    }
}
