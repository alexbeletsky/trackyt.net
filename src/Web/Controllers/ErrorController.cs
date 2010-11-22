using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace Web.Controllers
{
    // Handling errors in ASP.net MVC
    //http://www.genericerror.com/blog/2009/01/27/ASPNetMVCCustomErrorPages.aspx

    public class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult GenericError(string url)
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View("GenericError");
        }

    }
}
