using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Tracky.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Tracky/Dashboard/

        public ActionResult Index()
        {
            return View();
        }

    }
}
