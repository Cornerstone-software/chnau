using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFXProductMgr.Controllers
{
    public class AdvertmentController : Controller
    {
        //
        // GET: /Advertment/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 创建新的广告信息
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateNewAdvertment()
        {
            ViewBag.Title = "新增广告信息";
            return View("CreateOrUpdateAdvertment");
        }
    }
}
