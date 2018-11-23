using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFXProductMgr.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Company/

        public ActionResult Index()
        {
            return View();
        }

        //
       
        /// <summary>
        /// 创建新的公司信息
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateNewCompany()
        {
          
            ViewBag.Title = "新建公司信息";
            return View("CreateOrUpdateCompany");
        }

        public ActionResult UpdateCompany()
        {

            ViewBag.Title = "更新公司信息";
            return View("CreateOrUpdateCompany");
        }
        public ActionResult DedelteCompany()
        {
            ViewBag.Title = "删除公司信息";
            return View();
        }

    }
}
