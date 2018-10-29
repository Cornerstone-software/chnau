using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFXProductMgr.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult GetProduct()
        {
            return View();
        }
        
        public ActionResult AddProducts()
        {
            return View();
        }
        public ActionResult DeleteProducts()
        {
            return View();
        }
        public ActionResult ChangeProducts()
        {
            return View();
        }
       
    }
}
