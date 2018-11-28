using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFXProductMgr.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //if (Request.IsAuthenticated)
            //{
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("LogOn", "Account");
            //}
            return View();
        }

    }
}
