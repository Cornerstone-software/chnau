using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFXProductMgr.Models;

namespace MvcFXProductMgr.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Company/

        public ActionResult Index()
        {
            List<CompanyModel> list = new List<CompanyModel>();
            CompanyModel model = new CompanyModel();
            list = model.GetAllCompanys();
            return View(list);
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewCompany([Bind(Include = "Name,Address,Url,Tel")]CompanyModel model)
        {
            if(ModelState.IsValid){
                try
                {
                    if (model.Name.Length > 0) {
                        model.AddCompany(model);
                        HttpContext.Response.Write("<script>alert('保存成功！')</script>");
                    }
                    else
                    {
                        HttpContext.Response.Write("<script> $('#TxtCName').siblings('.errMsg').show();</script>");
                    }
                
                }
                catch(Exception ex){

                    HttpContext.Response.Write("<script>alert('保存不成功！请仔细检查。'" + ex.Message+")</script>");
                }
            }
            else
            {
                HttpContext.Response.Write("<script>alert('没有数据要保存！')</script>"); 
            }

            return View("CreateOrUpdateCompany");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateCompany(int id)
        {

            ViewBag.Title = "修改公司信息";
            CompanyModel model = new CompanyModel();
            model = model.GetCompany(id);
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateCompany(CompanyModel model)
        {
            return View();
        } 
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult DedelteCompany()
        {
            ViewBag.Title = "删除公司信息";
            return View();
           
        }

    }
}
