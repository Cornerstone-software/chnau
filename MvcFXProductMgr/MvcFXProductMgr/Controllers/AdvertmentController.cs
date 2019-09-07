using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFXProductMgr.Models;
using MvcFXProductMgr.ViewModels;
namespace MvcFXProductMgr.Controllers
{
    public class AdvertmentController : Controller
    {
        /// <summary>
        /// 显示所有的广告信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<AdvertmentModel> list = new List<AdvertmentModel>();
            AdvertmentModel model = new AdvertmentModel();
            list = model.GetAllAdvertments();
            return View(list);
        }
        /// <summary>
        /// 创建新的广告信息
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult AdvertmentManage(int id=0)
        {
            AdvertmentModel model = new AdvertmentModel();
            if (id == 0)
            {
                model.Id = 0;
                model.Name = "";
                model.Url = "";
            }
            else
            {
                model = model.GetAdvertmentById(id);
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult AdvertmentManage([Bind(Include = "Id,Name,Url")]AdvertmentModel model)
        {
            //创建删除日志
            LogModel logModel = new LogModel();
            logModel.Name = User.Identity.Name;
            logModel.Date = DateTime.Now;
            
            if (model.Id == 0)
            {
                model.AddAdvertment(model);
                logModel.Content = "AddAdvertment";
                try
                {
                    logModel.AddLog(logModel);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "日志创建失败");
                }
                HttpContext.Response.Write("<script>alert('保存成功！')</script>");
                return View();
            }
            else
            {
                model.UpdateAdvertment(model);

                logModel.Content = "UpdateAdvertment";
                try
                {
                    logModel.AddLog(logModel);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "日志创建失败");
                }

                return RedirectToAction("Index", "Advertment");
            } 
        }
        /// <summary>
        /// 删除指定Id的广告信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteAdvertment(int id)
        {
            AdvertmentModel model = new AdvertmentModel();
            model.DeleteAdvertment(id);

            //创建删除日志
            LogModel logModel = new LogModel();
            logModel.Name = User.Identity.Name;
            logModel.Date = DateTime.Now;
            logModel.Content = "DeleteAdvertment";
            try
            {
                logModel.AddLog(logModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "日志创建失败");
            }

            return RedirectToAction("Index", "Advertment");
        }
        /// <summary>
        /// 获取广告信息
        /// </summary>
        /// <returns></returns>
        public AdvertmentModel GetAdvertment()
        {
            AdvertmentModel model = new AdvertmentModel();
            model = model.GetAdvertment();
            return model;
        }
    }
}
