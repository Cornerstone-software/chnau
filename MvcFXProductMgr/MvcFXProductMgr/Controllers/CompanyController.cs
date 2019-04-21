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
        /// <summary>
        /// 所有公司信息显示
        /// </summary>
        /// <returns></returns>

        public ActionResult Index()
        {
            List<CompanyModel> list = new List<CompanyModel>();
            CompanyModel model = new CompanyModel();
            list = model.GetAllCompanys();
            return View(list);
        }

        //
       
        /// <summary>
        /// 创建公司信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CompanyManage(int id = 0)
        {
            CompanyModel model = new CompanyModel();
            if (id == 0)
            {
                model.Id = 0;
                model.Name = "";
                model.Address = "";
                model.Url = "";
                model.Tel = "";
            }
            else
            {                
                model = model.GetCompanyById(id);               
            }
            return View(model);
        }
        /// <summary>
        /// 增加或修改公司信息
        /// id=0时，新增公司信息
        /// id>0时，修改公司信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CompanyManage([Bind(Include = "Id,Name,Address,Url,Tel")]CompanyModel model)
        {

            //创建删除日志
            LogModel logModel = new LogModel();
            logModel.Name = User.Identity.Name;
            logModel.Date = DateTime.Now;
           
           
            if (model.Id ==0)
            {
                model.AddCompany(model);

                logModel.Content = "AddCompany";
                try
                {
                    logModel.AddLog(logModel);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "日志创建失败");
                }
                HttpContext.Response.Write("<script>alert('保存成功！')</script>");
                return View("CompanyManage");
            }
            else
            {
                model.UpdateCompany(model);

                logModel.Content = "UpdateCompany";
                try
                {
                    logModel.AddLog(logModel);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "日志创建失败");
                }
                return RedirectToAction("Index","Company");
            }         
        }

        /// <summary>
        /// 删除指定Id的公司信息
        /// </summary>
        /// <returns></returns>
        
        public ActionResult DeleteCompany(int id)
        {
            CompanyModel model = new CompanyModel();
            model.DeleteCompany(id);


            //创建删除日志
            LogModel logModel = new LogModel();
            logModel.Name = User.Identity.Name;
            logModel.Date = DateTime.Now;
            logModel.Content = "DeleteCompany";
            try
            {
                logModel.AddLog(logModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "日志创建失败");
            }
            return RedirectToAction("Index", "Company");
           
        }

    }
}
