using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFXProductMgr.Models;

namespace MvcFXProductMgr.Controllers
{
    public class TestingOrgController : Controller
    {
        /// <summary>
        /// 获取信息的列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateNewTestingOrg()
        {
            return View("CreateOrUpdateTestingOrg");
        }

        [HttpPost]
        public ActionResult CreateNewTestingOrg(CompanyModel model)
        {
            return View("CreateOrUpdateTestingOrg");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult CreateNewTestingOrg(int id)
        {
            return View("CreateOrUpdateTestingOrg");
        }
  

        public ActionResult UpdateTestingOrg()
        {
            return View();
        }
        public ActionResult DeleteTestingOrg()
        {
            return View();
        }
    }
}
