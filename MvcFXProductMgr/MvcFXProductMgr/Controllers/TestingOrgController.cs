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
        public ActionResult CreateNewTestingOrg([Bind(Include = "Name,Url,Tel")]TestingOrgModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    ModelState.AddModelError("Name", "'Name'是必填字段");
                }
                else
                {
                    model.Status = "N";
                    bool bRes = model.AddTestingOrg(model);
                    if (bRes)
                    {
                        HttpContext.Response.Write("");
                        
                    }
                }


            }
            return View("CreateOrUpdateTestingOrg");
        }


        public ActionResult UpdateTestingOrg(TestingOrgModel model)
        {
            return View("CreateOrUpdateTestingOrg",model);
        }
        public ActionResult DeleteTestingOrg()
        {
            return View();
        }
    }
}
